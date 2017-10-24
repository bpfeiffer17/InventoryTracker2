using InventoryTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryTracker.Controllers
{
    public class AssetController : Controller
    {
        private InventoryTrackerEntities db = new InventoryTrackerEntities();

        // GET: Asset
        public ActionResult Index()
        {
            return View("Browse");
        }

        public ActionResult Browse(int id = 0)
        {
            ViewBag.assetTypes = db.AssetTypes.ToList();
            if (id != 0)
            {
                Request.Cookies["UserSettings"]["AssetTypeToBrowseID"] = id.ToString();
                ViewBag.assetTypeToBrowse = db.AssetTypes.Find(id);
            }
            if (Request.Cookies["UserSettings"]["AssetTypeToBrowseID"] != null)
            {
                ViewBag.assetTypeToBrowse = db.AssetTypes.Find((Int64.Parse(Request.Cookies["UserSettings"]["AssetTypeToBrowseID"])));
            }
            else
            {
                ViewBag.assetTypeToBrowse = ViewBag.assetTypes[0];
            }
            //Gather a list of Assets from the database
            ViewBag.assets = db.Assets.ToList();
            return View();
        }

		public ActionResult Edit(int id)
		{
            ViewBag.id = id;
            return View();
		}
        public ActionResult View(int id)
        {
            Asset asset = db.Assets.Find(id);
            return View(asset);
        }
    }
}
