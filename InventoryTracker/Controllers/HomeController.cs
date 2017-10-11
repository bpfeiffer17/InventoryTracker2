using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication6.Models;


namespace InventoryTracker.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        //links to the Cleveand Clinic about page after the user put in home/about

        public ActionResult About()
        {
            Response.BufferOutput = true;
            Response.Redirect("https://my.clevelandclinic.org/about");
            return View();
        }

        //links to the cleveland clinic contact page after the user puts in home/contact
        public ActionResult Contact()
        {
            Response.BufferOutput = true;
            Response.Redirect("https://my.clevelandclinic.org/help");
            return View();
        }
    }
}

//namespace MvcApplication6.Controllers
//{
//    public class HomeController : Controller
//    {
//        public ActionResult Index()
//        {
//            LoginModel obj = new LoginModel();
//            return View(obj);
//        }
//        [HttpPost]
//        public ActionResult Index(LoginModel objuserlogin)
//        {
//            var display = Userloginvalues().Where(m => m.UserName == objuserlogin.UserName && m.UserPassword == objuserlogin.UserPassword).FirstOrDefault();
//            if (display != null)
//            {
//                ViewBag.Status = "CORRECT UserNAme and Password";
//            }
//            else
//            {
//                ViewBag.Status = "INCORRECT UserName or Password";
//            }
//            return View(objuserlogin);
//        }
//        public List<LoginModel> Userloginvalues()
//        {
//            List<LoginModel> objModel = new List<LoginModel>();
//            objModel.Add(new LoginModel { UserName = "user1", UserPassword = "password1" });
//            objModel.Add(new LoginModel { UserName = "user2", UserPassword = "password2" });
//            objModel.Add(new LoginModel { UserName = "user3", UserPassword = "password3" });
//            objModel.Add(new LoginModel { UserName = "user4", UserPassword = "password4" });
//            objModel.Add(new LoginModel { UserName = "user5", UserPassword = "password5" });
//            return objModel;
//        }
//    }
//}
