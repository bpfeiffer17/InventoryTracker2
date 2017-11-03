/**
 * AssetType controller that takes care of requests for AssetType views of Edit, View, and Browse
 * 
 * @author Brandon Pfeiffer <bpfeiffer17@jcu.edu>
 */
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
using Newtonsoft.Json;

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
            AssetType assetType;
            if (id != 0)
            {
                assetType = db.AssetTypes.Find(id);
            }
            else
            {
                assetType = new AssetType();
            }
            ViewBag.assetTypeJSON = new JavaScriptSerializer().Serialize(assetType.getAssetTypeBare());
            ViewBag.dropDowns = new JavaScriptSerializer().Serialize(this.getDropDowns());
            return View();
        }

        public ActionResult View(int id)
        {
            AssetType assetType = db.AssetTypes.Find(id);
            ViewBag.assetTypeJSON = new JavaScriptSerializer().Serialize(assetType.getAssetTypeBare());
            return View(assetType);
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

        /**
         *  Save a new asset to the database or update an existing one
         */
        [HttpPost]
        public string SaveAsset()
        {
            //Deserialize the JSON object 'assetType' into an AssetType object
            AssetType assetType = JsonConvert.DeserializeObject<AssetType>(Request["assetType"]);

            //If assetType has an id, get the existing AssetType from the DB, else create a new AssetType
            AssetType dbAssetType;
            if (assetType.AssetTypeID != 0)
            {
                dbAssetType = db.AssetTypes.Find(assetType.AssetTypeID);
            }
            else
            {
                dbAssetType = new AssetType();
            }

            //Edit the properties of dbAssetType to match those coming in from the request
            dbAssetType.Name = assetType.Name;
            dbAssetType.Description = assetType.Description;
            dbAssetType.Tracked = assetType.Tracked;

            //If assetType's id is 0, add it to the DB
            if (assetType.AssetTypeID == 0)
            {
                db.AssetTypes.Add(dbAssetType);
            }

            //Save the properties coming in from the request
            this.saveProperties(assetType, dbAssetType);

            //Save the changes to the DB
            db.SaveChanges();

            //Return a blank string to recognize there were no errors
            return "";
        }

        /**
         * Save the properties the are coming in from the request
         * 
         * @param {AssetType} assetType - the AssetType coming in from the request
         * @param {AssetType} dbAssetType - the AssetType from the DB to save the new/edited properties on
         */
        private void saveProperties(AssetType assetType, AssetType dbAssetType)
        {
            //Edit or add each Property on assetType
            foreach (var prop in assetType.Properties)
            {
                //If the Property's id is not 0, then Find it in the DB and update its properties,
                //else add a new Property to the DB with the object from the request
                if (prop.PropertyID != 0)
                {
                    Property dbProperty = db.Properties.Find(prop.PropertyID);
                    dbProperty.Name = prop.Name;
                    dbProperty.Type = prop.Type;
                    dbProperty.Unit = prop.Unit;
                    dbProperty.DropDownID = prop.DropDownID;
                }
                else
                {
                    dbAssetType.Properties.Add(prop);
                }
            }
        }

        public string DropDownHelper()
        {
            return new JavaScriptSerializer().Serialize(this.getDropDowns());
        }
    }
}