using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using rpavelko.Data.Core;
using rpavelko.Data.Repositories;
using rpavelko.Data.Repositories.Interfaces;

namespace rpavelko.Controllers
{
    public class HomeController : Controller
    {
        [Inject]
        public IAccountRepository AccountRepository { get; set; }

        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to Roma Pavelko app!";
            
            var accounts = AccountRepository.GetAll().ToList();
            ViewBag.Accounts = accounts;
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
