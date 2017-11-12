using LumenWorks.Framework.IO.Csv;
using InventoryTracker.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

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
                Response.Cookies["UserSettings"]["AssetTypeToBrowseID"] = id.ToString();
                ViewBag.assetTypeToBrowse = db.AssetTypes.Find(id);
            }
            else if (Request.Cookies["UserSettings"]["AssetTypeToBrowseID"] != null)
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

        public ActionResult Edit(int id, int assetTypeID = 0)
		{
            ViewBag.id = id;
            ViewBag.assetTypeID = assetTypeID;
            ViewBag.dropDowns = new JavaScriptSerializer().Serialize(this.getDropDowns());
            return View();
		}

        /**
         * Get an array of DropDowns from the DB
         */
        private DropDownBare[] getDropDowns()
        {
            List<DropDownBare> dropDowns = new List<DropDownBare>();
            List<DropDown> dbDropDowns = db.DropDowns.ToList();
            foreach (DropDown dd in dbDropDowns)
            {
                dropDowns.Add(dd.getDropDownBare());
            }
            return dropDowns.ToArray();
        }

        public ActionResult View(int id)
        {
            Asset asset = db.Assets.Find(id);
            return View(asset);
        }

        public ActionResult BulkImport()
        {

            return View();
        }

        public ActionResult Upload()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {

                if (upload != null && upload.ContentLength > 0)
                {

                    if (upload.FileName.EndsWith(".csv"))
                    {
                        Stream stream = upload.InputStream;
                        DataTable csvTable = new DataTable();
                        using (CsvReader csvReader =
                            new CsvReader(new StreamReader(stream), true))
                        {
                            csvTable.Load(csvReader);
                        }
                        return View(csvTable);
                    }
                    else
                    {
                        ModelState.AddModelError("File", "This file format is not supported");
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError("File", "Please Upload Your file");
                }
            }
            return View();
        }

        public string JSON(int id = 0, int assetTypeID = 0)
        {
            Asset asset;
            if (id != 0)
            {
                asset = db.Assets.Find(id);
            }
            else
            {
                // Create a new asset
                asset = new Asset();
                // Add AssetType info based on the given assetTypeID
                asset.AssetTypeID = assetTypeID;
                asset.AssetType = db.AssetTypes.Find(assetTypeID);
            }
            return ViewBag.assetTypeJSON = new JavaScriptSerializer().Serialize(asset.getAssetBare());
        }

        /**
         *  Save a new asset to the database or update an existing one
         */
        [HttpPost]
        public string SaveAsset(AssetBare asset)
        {
            // Check to see if the asset exists in the db of if this is a new asset
            if (db.Assets.Find(asset.AssetID) == null)
            {
                Asset newDBAsset = new Asset();
                newDBAsset.AssetTypeID = asset.AssetType.AssetTypeID;
                db.Assets.Add(newDBAsset);
                db.SaveChanges();
                asset.AssetID = newDBAsset.AssetID;
            }
            foreach (var prop in asset.AssetType.Properties)
            {
                PropertyValue propVal = db.PropertyValues.Find(asset.AssetID, prop.PropertyID);
                if (propVal != null)
                {
                    if (prop.Value != "" && prop.Value != null)
                    {
                        propVal.Value = prop.Value;
                    }
                    else
                    {
                        db.PropertyValues.Remove(propVal);
                    }
                }
                else
                {
                    if (prop.Value != "" && prop.Value != null)
                    {
                        propVal = new PropertyValue();
                        propVal.AssetID = asset.AssetID;
                        propVal.PropertyID = prop.PropertyID;
                        propVal.Value = prop.Value;
                        db.PropertyValues.Add(propVal);
                    }
                }
                db.SaveChanges();
            }
            return "";
        }

        /**
         *  Delete an asset from the database given its id
         */
        [HttpPost]
        public string DeleteAsset(int id)
        {
            Asset asset = db.Assets.Find(id);
            if (asset != null)
            {
                // Delete the PropertyValues for this asset
                foreach (var propVal in asset.PropertyValues.ToList())
                {
                    db.PropertyValues.Remove(propVal);
                }
                // Delete the asset itself
                db.Assets.Remove(asset);
                db.SaveChanges();
            }
            return "";
        }
    }
}
