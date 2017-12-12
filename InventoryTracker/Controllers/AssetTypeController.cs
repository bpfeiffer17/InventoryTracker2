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
using InventoryTracker.Models.ViewModels;

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

        // Gather the AssetTypes from the database and generate an
        // array of AssetTypeViewModels for the view
        public ActionResult Browse()
        {
            IEnumerable<AssetType> dbAssetTypes = db.AssetTypes.ToList();
            AssetTypeViewModel[] assetTypes = new AssetTypeViewModel[dbAssetTypes.Count()];
            for (var i = 0; i < dbAssetTypes.Count(); i++)
            {
                assetTypes[i] = new AssetTypeViewModel(dbAssetTypes.ElementAt(i));
            }
            return View(assetTypes);
        }

        // Return a JSON representation of the AssetTypeViewModel with the given id
        // If the id is 0, return an empty AssetTypeViewModel
        public string JSON(int id = 0)
        {
            return new JavaScriptSerializer().Serialize(this.getAssetTypeViewModel(id));
        }

        // GET: AssetType/Edit/{id}
        public ActionResult Edit(int id = 0)
        {
            return View(this.getAssetTypeViewModel(id));
        }

        // GET: AssetType/View/{id}
        public ActionResult View(int id = 0)
        {
            return View(this.getAssetTypeViewModel(id));
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
                dbAssetType.Active = 1;
                db.AssetTypes.Add(dbAssetType);
                //Add tracked and non tracked dproperties
                IQueryable<Property> defaults;
                if (assetType.Tracked == 1)
                {
                    defaults = db.Properties.Where(prop => prop.Tracked == 1);
                }else
                {
                    defaults = db.Properties.Where(prop => prop.NonTracked == 1);
                }
                foreach (Property prop in defaults)
                {
                    dbAssetType.Properties.Add(prop);
                }
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
                    dbProperty.Active = prop.Active;
                    dbProperty.Required = prop.Required;
                }
                else
                {
                    if (prop.Active != 0)
                    {
                        dbAssetType.Properties.Add(prop);
                    }
                }
            }
        }

        public string DropDownHelper()
        {
            return new JavaScriptSerializer().Serialize(this.getDropDowns());
        }

        /**
         *  Make the asset type with the given id inactive
         */
        [HttpPost]
        public string DeleteAssetType(int id)
        {
            AssetType assetType = db.AssetTypes.Find(id);
            if (assetType != null)
            {
                assetType.Active = 0;
                db.SaveChanges();
            }
            return "";
        }

        private AssetTypeViewModel getAssetTypeViewModel(int id = 0)
        {
            AssetTypeViewModel assetType;

            if (id == 0)
            {
                assetType = new AssetTypeViewModel(new AssetType());
            }
            else
            {
                assetType = new AssetTypeViewModel(db.AssetTypes.Find(id));
            }

            return assetType;
        }
    }
}