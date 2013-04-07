using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Ninject;
using rpavelko.Data.Core;
using rpavelko.Data.Entities;
using rpavelko.Data.Repositories.Interfaces;
using rpavelko.Data.Utils;
using rpavelko.Models;

namespace rpavelko.Controllers
{
    public class AccountController : Controller
    {
        [Inject]
        public IAccountRepository AccountRepository { get; set; }
        [Inject]
        public IUnitOfWork UnitOfWork { get; set; }

        public ActionResult LogOn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (AccountRepository.ValidateUser(model.Email, model.Password))
                {
                    var account = AccountRepository.GetAccountByEmail(model.Email);

                    FormsAuthentication.SetAuthCookie(string.Format("{0} {1}", account.FirstName, account.LastName), model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            return View(model);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var salt = Security.GenerateSalt();
                var account = new Account
                                  {
                                      FirstName = model.FirstName,
                                      LastName = model.LastName,
                                      Email = model.Email,
                                      PwdSalt = salt,
                                      PwdHash = Security.HashPassword(model.Password, salt)
                                  };
                AccountRepository.Add(account);
                UnitOfWork.Commit();  
                
                FormsAuthentication.SetAuthCookie(string.Format("{0} {1}", account.FirstName, account.LastName), true);
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid && AccountRepository.ValidateUser(model.Email, model.OldPassword))
            {
                var account = AccountRepository.GetAccountByEmail(model.Email);
                account.PwdHash = Security.HashPassword(model.NewPassword, account.PwdSalt);

                return RedirectToAction("ChangePasswordSuccess");
            }

            return View(model);
        }

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }
    }
}
