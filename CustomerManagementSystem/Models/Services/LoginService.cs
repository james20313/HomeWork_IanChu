using CustomerManagementSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace CustomerManagementSystem.Models.Services
{
    public class LoginService
    {
        private I客戶資料Repository CustomerRepo;
        public LoginService()
        {
            this.CustomerRepo = RepositoryHelper.Get客戶資料Repository();
        }

        public string CheckLogin(CustomerLoginViewModel userInput)
        {
            /*
             * 1.尋找有此帳號
             * 2.取出鹽
             * 3.輸入密碼與鹽合併後SHA256且HASH 與DB中的HASH相符 即驗證成功
             */
            if(userInput.Account.Equals("admin") && userInput.Account.Equals("admin"))
            {
                return "admin";
            }

            var result = false;
            var existAcc = this.CustomerRepo.All().FirstOrDefault(x => x.Account.Equals(userInput));
            if (existAcc == null)
                result = false;
            else
            {
                string salt = existAcc.Salt;
                byte[] pwdAndSalt = Encoding.UTF8.GetBytes(userInput.Password + salt);
                byte[] hashBytes = new SHA256Managed().ComputeHash(pwdAndSalt);
                string hash = Convert.ToBase64String(hashBytes);
                if (hash.Equals(existAcc.Password))
                {
                    result = true;
                }
            }
            return result?existAcc.Account:string.Empty;
        }
    }
}