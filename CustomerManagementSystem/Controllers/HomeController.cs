using CustomerManagementSystem.Models;
using CustomerManagementSystem.Models.Attribute;
using CustomerManagementSystem.Models.Services;
using CustomerManagementSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CustomerManagementSystem.Controllers
{
    [OutputActionSpendTimeAttribute]
    public class HomeController : Controller
    {
        private LoginService _loginService = new LoginService();

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [AllowAnonymous]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login()
        {
            ViewBag.LogOutMsg = TempData["LogOutMsg"];
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(CustomerLoginViewModel login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }
            string accountName = _loginService.CheckLogin(login);

            if (!string.IsNullOrEmpty(accountName))
            {
                string userRole = "";
                if (accountName.Equals("admin"))
                {
                    userRole = "Manager";
                }
                else
                {
                    userRole = "Customer";
                }
                var authTicket = new FormsAuthenticationTicket(
                    1, // version
                    accountName, // user name
                    DateTime.Now, // created
                    DateTime.Now.AddMinutes(20), // expires
                    false, // persistent?
                    userRole // can be used to store roles
                    );

                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                HttpContext.Response.Cookies.Add(authCookie);


                //FormsAuthentication.SetAuthCookie(accountName, false);   //設置cookies
                if (Request.QueryString["ReturnUrl"] != null)
                {
                    if (Request.QueryString["ReturnUrl"].Contains("LogOut"))
                    {

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return Redirect(Request.QueryString["ReturnUrl"]);
                    }

                }
                else
                    return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("error", "帳號不存在或者密碼輸入錯誤");
                return View(login);
            }
        }

        [Authorize]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            TempData["LogOutMsg"] = "已成功登出";
            return RedirectToAction("Login", "Home");
        }
    }
}