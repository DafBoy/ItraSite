using Itreansition.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Itreansition.Services.CompanyService
{
    public class TransferService
    {
        public bool transfer { get; set; }
        public string message { get; set; }


        private static bool CheckUserMoney(User user,int sum)
        {
            if ((user.Money - sum) < 0) return false;
            else return true;
        
        }

        private static int CheckCompanyAccount(Company company, int sum)
        {
            if ((company.CurrentMoney + sum) > company.NeedMoney) return (company.CurrentMoney + sum) - company.NeedMoney;
            else return 0;

        }

        private static async Task< bool> Debiting(User user,Company company,int sum, AppDbContext context)
        {
            user.Money -= sum;
            company.CurrentMoney+= sum;
            context.Users.Update(user);
            context.Companies.Update(company);
            await context.SaveChangesAsync();
            return true;

        }

        public static async Task <TransferService> MoneyTransfer(User user, Company company,int sum, AppDbContext context)
        {
            TransferService state = new TransferService();
            if (sum == 0)
            {
                state.transfer = false;
                state.message = "Well, keep this amount for yourself";
                return state;
            }
            int money=0;
            if (CheckUserMoney(user, sum))
            { 
                money = sum-CheckCompanyAccount(company, sum);
                if (money > 0)
                {

                    await Debiting(user, company, money, context);
                    state.transfer = true;
                    if (company.CurrentMoney == company.NeedMoney)
                    {
                        Achievement achievement = context.Achievements.FirstOrDefault(i => i.OwnerName == user.UserName);
                        User userCompany = context.Users.FirstOrDefault(n => n.UserName == company.OwnerName);
                        Achievement achievementCompany = context.Achievements.FirstOrDefault(i => i.OwnerName == userCompany.UserName);
                        if (achievementCompany.Shot) {
                            achievementCompany.Closer = true;
                        }
                        else achievementCompany.Shot = true;

                        achievement.Сheckmate = true;
                        context.Achievements.Update(achievementCompany);
                        await context.SaveChangesAsync();
                        context.Achievements.Update(achievement);
                        await context.SaveChangesAsync();

                    }
                    state.message = "The operation was successful.Charged amount:" + money + "$";
                    return state;

                }
                else
                {

                    state.transfer = false;
                    state.message= "The company raised enough funds";
                    return state;
                 }
            }
            else
            {
                state.transfer = false;
                state.message = "Not enough funds in account";
                return state;
            }
        }
    }
}
