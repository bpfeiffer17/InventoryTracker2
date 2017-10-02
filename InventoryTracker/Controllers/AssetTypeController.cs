using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using InventoryTracker.Models;
using System.Web.Script.Serialization;

namespace InventoryTracker.Controllers
{
    public class AssetTypeController : Controller
    {
        private InventoryTrackerEntities db = new InventoryTrackerEntities();

        // GET: AssetType
        public ActionResult Index()
        {
            return View("Browse");
        }

        public ActionResult Browse()
        {
            //Gather a list of AssetTypes from the database 
            ViewBag.assetTypes = db.AssetTypes.ToList();
            return View();
        }

        public ActionResult Edit(int id)
        {
            ViewBag.id = id;
            return View();
        }

        public ActionResult View(int id)
        {
            ViewBag.id = id;
            return View();
        }
    }
}