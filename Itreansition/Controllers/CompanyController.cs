using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Itreansition.Models;
using Itreansition.Services.CompanyService;
using static Itreansition.Services.HomeSevice.FunctionHelpers;
using Itreansition.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;

namespace Itreansition.Controllers
{
    public class CompanyController : Controller
    {


        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        private AppDbContext db;




        public CompanyController(AppDbContext context, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;

            db = context;
        }
        //-----------------------------------Company by id--------------------------------------------//
        public ActionResult Index(int id)
        {
            CompanyModel modelRequest = CreaterModelCompany.GetCompanyById(id, db);
            if (modelRequest == null) return RedirectToAction("Index", "Home");
            return View(modelRequest);
        }

        //------Функция отправки коментариев

        //-----------------------------------Create Company GET--------------------------------------------//
        [Authorize]
        public ActionResult Create(string user)
        {
            if(User.Identity.Name == user || User.IsInRole("admin"))
            {
                ViewBag.Name = user;
            return View();
            }
            return RedirectToAction("Index", "Home");
        }

        //-----------------------------------Create Company POST--------------------------------------------//
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RegisterCompanyModel registerCompanyModel, IFormFile LoadedFile)
        {
            if (ModelState.IsValid)
            {
                Company company = new Company();
                company.Name = registerCompanyModel.Name;
                company.Logo = registerCompanyModel.Logo;
                company.BrieflyAbout = registerCompanyModel.BrieflyAbout;
                company.About = registerCompanyModel.About;
                company.NeedMoney= registerCompanyModel.NeedMoney;
                company.OwnerName = registerCompanyModel.OwnerName;
                company.DateCreate = DateTime.Now;

                if (await SaveImgAsync(LoadedFile, "Logo", registerCompanyModel.OwnerName))
                {
                    company.Logo = GetPathImg(LoadedFile.FileName, "Logo", registerCompanyModel.OwnerName);
                }
                company.NeedMoney *= 100;
                db.Companies.Add(company);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Name = registerCompanyModel.OwnerName;
            return View(registerCompanyModel);
        }

        [Authorize]

        public ActionResult Edit(int id)
        {
            Company company = db.Companies.FirstOrDefault(i => i.Id == id);
            if (User.Identity.Name == company.OwnerName || User.IsInRole("admin"))
            { 
                EditCompanyViewModel editCompanyViewModel = EditCompanyViewModel.ParseCompanyInViewModel(company);

                return View(editCompanyViewModel);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditCompanyViewModel editCompanyViewModel, IFormFile LoadedFile)
        {
            ViewData["CurrentTitle"] = "Index";
            if (ModelState.IsValid)
            {
                Company company = await db.Companies.FirstOrDefaultAsync(i => i.Id == editCompanyViewModel.Id);
                company.Name = editCompanyViewModel.Name;
                company.BrieflyAbout = editCompanyViewModel.BrieflyAbout;
                company.About = editCompanyViewModel.About;
                if (editCompanyViewModel.Video!="") company.Video = editCompanyViewModel.Video;

                if (await SaveImgAsync(LoadedFile, "Logo", editCompanyViewModel.OwnerName))
                {
                    company.Logo = GetPathImg(LoadedFile.FileName, "Logo", editCompanyViewModel.OwnerName);
                }
                db.Companies.Update(company);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return Redirect("Edit?id=" + editCompanyViewModel.Id);
            //return View(editCompanyViewModel);
        }

        [Authorize]
        public async Task<ActionResult> Dell(int id)
        {

           Company company= await db.Companies.FirstOrDefaultAsync(i => i.Id == id);
            if (company != null)
            {
                if (User.Identity.Name == company.OwnerName || User.IsInRole("admin"))
                {
                    return View(company);
                }
                else return RedirectToAction("Index", "Home");
            }
            else return RedirectToAction("Index", "Home");
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Dell(Company company)
        {
            Company companyDell = await db.Companies.FirstOrDefaultAsync(i => i.Id == company.Id);
            User user = await db.Users.FirstOrDefaultAsync(n => n.UserName == companyDell.OwnerName);
            db.Comments.Where(i => i.CompanyId == companyDell.Id);

            DellService.RemoveRelations(db, company.Id);
            string ownerName = companyDell.OwnerName;
            user.Money += companyDell.CurrentMoney;
            db.Users.Update(user);
            db.Companies.Remove(companyDell);
            await db.SaveChangesAsync();
            return Redirect("~/Account?=" + ownerName);
        }


        //-----------------------------------LIKE--------------------------------------------//
        [HttpPost]
        [Authorize]
        public async Task<bool> SetLike(int idComment, string userName, bool stateLike)
        {
            Comment comment = db.Comments.FirstOrDefault(i => i.Id == idComment);
            if (!(db.LikesComments.FirstOrDefault(u => u.OwnerLikeName == userName && u.CommentId == idComment && u.Like == stateLike) == null)) return false;
            else
            {

                LikesComment likeForDell = db.LikesComments.FirstOrDefault(u => u.OwnerLikeName == userName && u.CommentId == idComment);
                if (!(likeForDell == null))
                {
                    db.LikesComments.Remove(likeForDell);
                    if (!stateLike) comment.Like -= 1;
                    else comment.Dislike -= 1;
                }
            }
            LikesComment like = new LikesComment();
            like.CommentId = idComment;
            like.OwnerLikeName = userName;
            like.Like = stateLike;

            if (stateLike) comment.Like += 1;
            else comment.Dislike += 1;
            db.Comments.Update(comment);
            db.LikesComments.Add(like);
            await db.SaveChangesAsync();
            return true;

        }

        //-----------------------------------Send Comment--------------------------------------------//
        [Authorize]
        [HttpPost]
        public async Task<bool> _SendComment(int idCompany, string userName, string userComment)
        {
            StreamReader st = new StreamReader(Request.Body);
            string data = st.ReadToEnd();
            Comment comment = new Comment();
            comment.CompanyId = idCompany;
            comment.OwnerName = userName;
            comment.Text = userComment;
            comment.Date = DateTime.Now;
            db.Comments.Add(comment);
            await db.SaveChangesAsync();
            return false;
        }


        //-------Функция обновляющая коментарии через AJAX
        [HttpPost]
        public IActionResult _Comments(int idCompany)
        {
            var commentList = db.Comments.Where(i => i.CompanyId == idCompany);
            return PartialView(commentList);
        }
        //-----------------------------------Bonuses View and Add--------------------------------------------//
        [HttpPost]
        public IActionResult _Bonuses(int idCompany)
        {

            ViewBag.ListBonuses = db.Bonuses.Where(i => i.CompanyId == idCompany);
            Company company = db.Companies.FirstOrDefault(i => i.Id == idCompany);
            ViewBag.OwnerName = company.OwnerName;
            ViewBag.idCompany = idCompany;
            Bonus bonus = new Bonus();
            return PartialView(bonus);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddBonus(IFormFile LoadedFile,Bonus bonus)
        {

            
            if (await SaveImgAsync(LoadedFile, "BonusImages", User.Identity.Name))
            {
                bonus.Image = GetPathImg(LoadedFile.FileName, "BonusImages", User.Identity.Name);
            }
            db.Bonuses.Add(bonus);
            await db.SaveChangesAsync();
            return Redirect("~/Company?id=" + bonus.CompanyId);
        }

        //-----------------------------------About--------------------------------------------//
        [HttpPost]
        public IActionResult _AboutCompany(int idCompany)
        {
            Company company = db.Companies.FirstOrDefault(i => i.Id == idCompany);
            return PartialView(company);
        }

        //-----------------------------------Gallery Company--------------------------------------------//
        [HttpPost]
        public async Task<IActionResult> _Gallery(int idCompany)
        {
            ViewBag.Company = await  db.Companies.FirstOrDefaultAsync(i => i.Id == idCompany);
            Company company= await db.Companies.FirstOrDefaultAsync(i => i.Id == idCompany);
            ViewBag.OwnerName = company.OwnerName;
            IEnumerable<CompanyImage> imagesCompany = db.CompanyImages.Where(i => i.CompanyId == idCompany);
            return PartialView(imagesCompany);
        }

        //-----------------------------------Add Image to gallery--------------------------------------------//
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddImage(int idCompany, IFormFile LoadedFile)
        {
            CompanyImage companyImage = new CompanyImage();
            if(await SaveImgAsync(LoadedFile, "CompanyImages", User.Identity.Name))
            {
                companyImage.Img = GetPathImg(LoadedFile.FileName, "CompanyImages", User.Identity.Name);
            }
            companyImage.CompanyId = idCompany;
            db.CompanyImages.Add(companyImage);
            await db.SaveChangesAsync();
            return Redirect("~/Company?id=" + idCompany);
        }

        //-----------------------------------Dell Image from gallery--------------------------------------------//
        [HttpPost]
        [Authorize]
        public async Task  DellImage(int idImage)
        {
            db.CompanyImages.Remove(db.CompanyImages.FirstOrDefault(i => i.Id == idImage));
            await db.SaveChangesAsync();


        }
        //-----------------------------------Create Donate--------------------------------------------//

        [Authorize]
        public IActionResult Donnate(int idCompany)
        {
            DonnateViewModel donnateModel = new DonnateViewModel();
            donnateModel.BonusList = db.Bonuses.Where(i => i.CompanyId == idCompany);
            donnateModel.User = db.Users.FirstOrDefault(n => n.UserName == User.Identity.Name);
            donnateModel.Company = db.Companies.FirstOrDefault(i => i.Id == idCompany);
            return View(donnateModel);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Donnate(double sum, string userName, int idCompany,int idBonnus)
        {
            int sumInt = Convert.ToInt32(sum *= 100);

            User user = db.Users.FirstOrDefault(n => n.UserName == userName);
            Company company = db.Companies.FirstOrDefault(i => i.Id == idCompany);
            TransferService answerTransfer = await TransferService.MoneyTransfer(user, company, sumInt, db);
            if ((answerTransfer.transfer) && (idBonnus != -1))
            {
                 await TransferBonus(db, idBonnus, User.Identity.Name);
            }


            //return Redirect("~/Company?id=" + company.Id);
            return Redirect("~/Home/Message?message=" + answerTransfer.message);
        }

        [HttpPost]
       public IActionResult ChangeLinkYoutube(string linkYoutube,int idCompany)
        {
            if(linkYoutube!=null)
            { 

                Company company = db.Companies.FirstOrDefault(i => i.Id == idCompany);
                company.Video = linkYoutube;
                db.Companies.Update(company);
                db.SaveChanges();
            }
            return Redirect("~/Company?id=" + idCompany);
        }




    }
}