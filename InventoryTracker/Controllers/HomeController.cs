﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication6.Models;
using InventoryTracker.Models;

namespace MvcApplication6.Controllers
{
    public class HomeController : Controller
    {
        private InventoryTrackerEntities db = new InventoryTrackerEntities();

        public ActionResult Index()
        {
            LoginModel obj = new LoginModel();
            return View(obj);
        }
        [HttpPost]
        public ActionResult Index(LoginModel objuserlogin)
        {
            var display = Userloginvalues().Where(m => m.UserName == objuserlogin.UserName && m.UserPassword == objuserlogin.UserPassword).FirstOrDefault();
            if (display != null)
            {
                ViewBag.Status = "CORRECT UserName and Password";
                Request.Cookies["Tracking"]["Username"] = objuserlogin.UserName.ToString();
                //ViewBag.assetTypeToBrowse = db.AssetTypes.Find(id);
            }
            else
            {
                ViewBag.Status = "INCORRECT UserName or Password";
            }
            return View(objuserlogin);
        }
        public List<LoginModel> Userloginvalues()
        {
            List<LoginModel> objModel = new List<LoginModel>();
            objModel.Add(new LoginModel { UserName = "user1", UserPassword = "password1" });
            objModel.Add(new LoginModel { UserName = "user2", UserPassword = "password2" });
            objModel.Add(new LoginModel { UserName = "user3", UserPassword = "password3" });
            objModel.Add(new LoginModel { UserName = "user4", UserPassword = "password4" });
            objModel.Add(new LoginModel { UserName = "user5", UserPassword = "password5" });
            return objModel;
        }
      
    }
}

