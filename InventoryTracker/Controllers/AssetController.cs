using LumenWorks.Framework.IO.Csv;
using InventoryTracker.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
                            // loads the csv file in a datatable
                            csvTable.Load(csvReader);

                            // Make sure that the csv has the right columns in it this needs to be dynamic based on the asset type
                            if (!csvTable.Columns.Contains("name") ||
                                !csvTable.Columns.Contains("description") ||
                                !csvTable.Columns.Contains("tracked"))
                                {

                                ViewBag.ErrorMessage = "CSV file must contail these three columns name, description, tracked";
                                return View("");
                                                                
                            }

                            ViewBag.ErrorMessage = "";
                            //loop thru table
                            foreach (DataRow row in csvTable.Rows)
                            {
                                

                                var name = row["name"].ToString();
                                var description = row["description"].ToString();
                                var tracked = row["tracked"].ToString();

                                //new AssetType model is created
                                var newAsset = new InventoryTracker.Models.AssetType();
                                newAsset.Name = name;
                                newAsset.Description = description;
                                newAsset.Tracked = byte.Parse(tracked);

                                //adds the new AssetType model to the database
                                db.AssetTypes.Add(newAsset);                                
                            }
                            //saves data into database
                            db.SaveChanges();
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
    }
}
