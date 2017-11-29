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
using System.Net.Mail;

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
                            // loads the csv file in a datatable
                            csvTable.Load(csvReader);

                            // Make sure that the csv has the right columns in it this needs to be dynamic based on the asset type
                            // First three columns are fixed, assettypeid, name, description... variable fields after
                            if (!csvTable.Columns.Contains("AssetTypeID") )
                                {

                                ViewBag.ErrorMessage = "CSV file must contail Atlest the AssetTypeID and any other properies";
                                return View("");
                                                                
                            }

                            ViewBag.ErrorMessage = "";
                            //loop thru table
                            foreach (DataRow row in csvTable.Rows)
                            {
                                // Get The asset type id
                                int assetTypeId = int.Parse(row["AssetTypeID"].ToString());

                                // Find the asset type according to the id
                                AssetType findTheAssetType = db.AssetTypes.Find(assetTypeId);

                                // Create a new asset and give it the assetTypeId you just found
                                Asset newAsset = new Asset()
                                {
                                    AssetTypeID = findTheAssetType.AssetTypeID
                                };

                                //Add the asset, save the changes so we can get the id to use for saving property values
                                Asset addedAsset = db.Assets.Add(newAsset);                               
                                db.SaveChanges();

                                // loop thru the rest of the columns to get the values
                                for (int col=1; col < csvTable.Columns.Count; col++)
                                {
                                    //get the column name and value
                                    string columnName = csvTable.Columns[col].ToString();
                                    string columnValue = row[col].ToString();

                                    //gets all of the properties
                                    var properties = db.Properties;

                                    // Find the corresponding Property by looping thru all until we find it
                                    foreach (Property propertyItem in db.Properties)
                                    {
                                        //System.Diagnostics.Debugger.Break();

                                        //if the column name matches the property name then create a new property value.
                                        if (propertyItem.Name.ToLower() == columnName.Trim().ToLower())
                                        {
                                            // found the matching property, now create a new value
                                            PropertyValue newPropertyValue = new PropertyValue();

                                            //give the new property the propertyID and the assetID from above
                                            newPropertyValue.PropertyID = propertyItem.PropertyID;
                                            newPropertyValue.AssetID = addedAsset.AssetID;
                                            newPropertyValue.Value = columnValue;

                                            //add the new property to the properties
                                            db.PropertyValues.Add(newPropertyValue);
                                            break; // exit the loop
                                        }
                                    }

                                    // Save the newly added property value
                                    db.SaveChanges();
                                }
                            }
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
        [HttpPost]
        public void checkTide(int id)
        {
            Asset asset = db.Assets.Find(id);
            if (asset != null)
            {
                int onHand = 0;
                int lowTide = 0;
                int highTide = 0;
                String assetName = "";
                foreach (var prop in asset.getAssetBare().AssetType.Properties)
                {
                    //21 is on hand, 22 is low tide, 23 is high tide, 41 is assetName 
                    if (prop.PropertyID == 48)
                    {
                        onHand = int.Parse(prop.Value);
                    }
                    else if (prop.PropertyID == 47)
                    {
                        lowTide = int.Parse(prop.Value);
                    }
                    else if (prop.PropertyID == 46)
                    {
                        highTide = int.Parse(prop.Value);
                    }
                    else if (prop.PropertyID == 41)
                    {
                        assetName = prop.Value;
                    }


                }
                if (onHand < lowTide)
                {
                    SendMail("Low Tide", assetName);
                }
                if (onHand > highTide)
                {
                    SendMail("High Tide", assetName);
                }

            }
        }

        [HttpPost]
        public void checkAllTides()
        {
            for (int id = 0; id < 100; id++)
            {
                checkTide(id);
            }
        }

        public void SendMail(String tide, String assetName)
        {
            //Mail Notification 
            MailMessage alert = new MailMessage();
            alert.To.Add(new MailAddress("inventorytrackerJCU@gmail.com"));
            alert.Subject = tide;
            alert.Body = "You have reached " + tide + " for the " + assetName + " Asset";
            alert.From = new MailAddress("inventorytrackerjcu@gmail.com");

            //Email Address from there you send the mail 
            var fromAddress = "inventorytrackerjcu@gmail.com";
            //Password of your mail address 
            const string fromPassword = "a11002233";

            //stmp settings 
            var smtp = new System.Net.Mail.SmtpClient();
            {
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                smtp.Credentials = new System.Net.NetworkCredential(fromAddress, fromPassword);
                smtp.Timeout = 20000;
            }
            // Passing values to smtp object 
            smtp.Send(alert);
        }
    }
}
