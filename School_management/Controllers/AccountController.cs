using School_management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace School_management.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(UserModel model)
        {
            using ( Web_nâng_caoEntities context = new Web_nâng_caoEntities())
            {
                bool IsValidUser = context.Users.Any(user => user.Username.ToLower() == model.UserName.ToLower() && user.Password == model.UserPassword);
                if ( IsValidUser)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false);
                    return RedirectToAction("Index", "Students");
                }
                ModelState.AddModelError("", "Invalid Username or Password");
                return View();
            }
        }

        public ActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Signup(User model)
        {
            using (Web_nâng_caoEntities context = new Web_nâng_caoEntities())
            {
                context.Users.Add(model);
                context.SaveChanges();
            }
            return RedirectToAction("Login");
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}