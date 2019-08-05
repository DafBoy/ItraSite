using Itreansition.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Http;
using Dropbox.Api;
using System.Text;
using Dropbox.Api.Files;

namespace Itreansition.Services.HomeSevice
{
    public class FunctionHelpers
    {
        public static AppDbContext db;
        public FunctionHelpers(AppDbContext context)
        {
            db = context;
        }
        public static string ConvertToUSD(int cent)
         {

            return  string.Format(new CultureInfo("En-us"),"{0:C}", cent / 100.0);
         }
        public static void AddAchievement(string userName)
        {
            Achievement achievement = new Achievement();
            achievement.OwnerName = userName;
            db.Achievements.Add(achievement);
            db.SaveChangesAsync();
        }


        public static async Task<bool> SaveImgAsync(IFormFile LoadedFile,string typeImg, string userName)
        {
            if (LoadedFile != null)
            {
                string path = Directory.GetCurrentDirectory();
                path += "\\wwwroot\\img\\UsersImages\\" + userName + "\\"+typeImg;
                Directory.CreateDirectory(path);
                path += "\\" + LoadedFile.FileName;
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await LoadedFile.CopyToAsync(fileStream);
                }
                return true;
            }
            return false;
        }

        public static string GetPathImg(string nameFile,string typeImg, string userName)
        {
            return "/img/UsersImages/" + userName + "/" + typeImg + "/"+ nameFile;

        }

        public static async Task <bool> TransferBonus(AppDbContext context,int bonusId,string userName)
        {
            Bonus bonus= context.Bonuses.FirstOrDefault(i => i.Id == bonusId);
            if (bonus.Amount > 0)
            {
                UserBonus userBonus = new UserBonus();
                userBonus.Description = bonus.Description;
                userBonus.OwnerName = userName;
                userBonus.Sum = bonus.Sum;
                userBonus.BonusName = bonus.BonusName;
                userBonus.Image = bonus.Image;
                bonus.Amount -= 1;
                if (bonus.Amount <= 0)
                {
                    context.Bonuses.Remove(bonus);
                    await context.SaveChangesAsync();
                }
                context.UserBonuses.Add(userBonus);
                await context.SaveChangesAsync();
                return true;
            }
            else return false;

        }





        //DropBox

        //public static async Task Upload(DropboxClient dbx, string folder, string file, string content)
        //{
        //    using (var mem = new MemoryStream(Encoding.UTF8.GetBytes(content)))
        //    {
        //        var updated = await dbx.Files.UploadAsync(
        //            folder + "/" + file,
        //            WriteMode.Overwrite.Instance,
        //            body: mem);
        //        Console.WriteLine("Saved {0}/{1} rev {2}", folder, file, updated.Rev);
        //    }
        //}

        //public async Task Download(DropboxClient dbx, string folder, string file)
        //{
        //    using (var response = await dbx.Files.DownloadAsync(folder + "/" + file))
        //    {
        //        Console.WriteLine(await response.GetContentAsStringAsync());
        //    }
        //}



    }
}
