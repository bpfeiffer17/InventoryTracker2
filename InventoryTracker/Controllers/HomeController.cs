using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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