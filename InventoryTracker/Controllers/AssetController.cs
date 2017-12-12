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
using InventoryTracker.Models.ViewModels;

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
        
        public ActionResult CreateFile(int id = 0)
        {
            //Create CSV File based upon user asset selection

            //https://stackoverflow.com/questions/1375486/how-to-create-file-and-return-it-via-fileresult-in-asp-net-mvc
            //https://www.codeproject.com/Articles/325103/MVC-Grid-to-Excel-file-download


            //Convert the rendering of the gridview to a string representation 
            StringWriter sw = new StringWriter();

            AssetType findTheAssetType = db.AssetTypes.Find(id);
            //******** building a comma seperated string by going thru properties
            foreach (Property prop in findTheAssetType.Properties)
            {
                sw.Write(prop.Name + ",");
            }
     
            //Create a response stream to create and write the Excel file

            this.HttpContext.Response.Clear();
            this.HttpContext.Response.AddHeader("content-disposition", "attachment;filename=" + findTheAssetType.Name +".csv");
            this.HttpContext.Response.Charset = "";
            this.HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            this.HttpContext.Response.ContentType = "application/vnd.ms-excel";

            //Open a memory stream that you can use to write back to the response
            byte[] byteArray = System.Text.Encoding.ASCII.GetBytes(sw.ToString());
            MemoryStream s = new MemoryStream(byteArray);
            StreamReader sr = new StreamReader(s, System.Text.Encoding.ASCII);

            //Write the stream back to the response
            this.HttpContext.Response.Write(sr.ReadToEnd());
            this.HttpContext.Response.End();

            return View("");
        }

        // GET: Asset/Browse
        public ActionResult Browse(int id = 0)
        {
            // Gather a list of AssetTypes to fill in the drop down of AssetTypes to browse
            ViewBag.assetTypes = db.AssetTypes.Where(assetType => assetType.Active == 1).ToList();

            // If an AssetTypeID was provided, gather the AssetType and Assets associated with it
            if (id != 0)
            {
                ViewBag.assetTypeToBrowse = db.AssetTypes.Find(id);
                IEnumerable<Asset> dbAssets = db.Assets.Where(asset => asset.AssetTypeID == id).ToList();
                AssetViewModel[] assets = new AssetViewModel[dbAssets.Count()];
                for (var i = 0; i < dbAssets.Count(); i++)
                {
                    assets[i] = new AssetViewModel(dbAssets.ElementAt(i));
                }
                ViewBag.assets = assets;
            }
            else
            {
                ViewBag.assetTypeToBrowse = null;
                ViewBag.assets = null;
            }

            return View();
        }

        // GET: Asset/Edit
        public ActionResult Edit(int id, int assetTypeID = 0)
		{
            ViewBag.assetID = id;
            ViewBag.assetTypeID = assetTypeID;
            return View();
		}

        // Get an array of DropDowns from the DB
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
            AssetViewModel asset = new AssetViewModel(db.Assets.Find(id));
            return View(asset);
        }

        public ActionResult BulkImport()
        {
            return View();
        }

        public ActionResult Upload(int id)
        {
            //This gets called from the javascript.
            //https://stackoverflow.com/questions/34223736/maintaining-viewbag-values-while-posting-data
            
            //put assetID in viewbag so it can be passed onto the csv upload.
            ViewBag.AssetIdForUpload = id;
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        //The assetID comes from the hidden field on the upload.cshtml
        public ActionResult Upload(HttpPostedFileBase upload, int assetId=0)
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
                           
                            if (assetId == 0)
                                {

                                ViewBag.ErrorMessage = "Error... Asset id not specified";
                                return View("");
                                                                
                            }

                            ViewBag.ErrorMessage = "";
                            //loop thru table
                            foreach (DataRow row in csvTable.Rows)
                            {
                                // Get The asset type id
                                int assetTypeId = assetId;

                                // Find the asset type according to the id ****
                                
                                AssetType findTheAssetType = db.AssetTypes.Find(assetTypeId);

                                // Create a new asset and give it the assetTypeId you just found
                                Asset newAsset = new Asset()
                                {
                                    AssetTypeID = findTheAssetType.AssetTypeID,
                                    DateAdded = DateTime.Now,
                                    DateLastModified = DateTime.Now
                            };

                                //Add the asset, save the changes so we can get the id to use for saving property values*****
                                Asset addedAsset = db.Assets.Add(newAsset);                               
                                db.SaveChanges();

                                // loop thru the rest of the columns to get the values
                                for (int col=0; col < csvTable.Columns.Count; col++)
                                {
                                    //get the column name and value
                                    string columnName = csvTable.Columns[col].ToString();
                                    string columnValue = row[col].ToString();

                                    // Find the corresponding Property by looping thru all until we find it ****
                                    //grab assetType and iterte through to find name.
                                    foreach (Property propertyItem in findTheAssetType.Properties)
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

        // Gather a JSON representation of the Asset with the given id
        public string JSON(int id = 0, int assetTypeID = 0)
        {
            AssetViewModel asset;
            if (id != 0)
            {
                asset = new AssetViewModel(db.Assets.Find(id));
            }
            else
            {
                // Create a new asset
                Asset newAsset = new Asset();
                // Add AssetType info based on the given assetTypeID
                newAsset.AssetTypeID = assetTypeID;
                newAsset.AssetType = db.AssetTypes.Find(assetTypeID);
                // Convert the Asset to an AssetViewModel
                asset = new AssetViewModel(newAsset);
            }
            return ViewBag.assetTypeJSON = new JavaScriptSerializer().Serialize(asset);
        }

        /**
         *  Save a new asset to the database or update an existing one
         */
        [HttpPost]
        public string SaveAsset(AssetViewModel postAsset)
        {
            // Try to find the postAsset in the db
            Asset dbAsset = db.Assets.Find(postAsset.AssetID);
            // If dbAsset is null, this is a new asset, add it to the db
            if (dbAsset == null)
            {
                // Create a new asset, add the AssetTypeID and DateAdded, add it to db
                dbAsset = new Asset();
                dbAsset.AssetTypeID = postAsset.AssetType.AssetTypeID;
                dbAsset.AssetType = db.AssetTypes.Find(postAsset.AssetType.AssetTypeID);
                dbAsset.DateAdded = DateTime.Now;
                dbAsset.DateLastModified = DateTime.Now;
                db.Assets.Add(dbAsset);
            }
            // Save the properties on the asset from the post data
            foreach (var postProp in postAsset.AssetType.Properties)
            {
                // Find the PropertyValue from the dbAsset
                PropertyValue dbProp = db.PropertyValues.Find(postAsset.AssetID, postProp.PropertyID);
                // If the PropertyValue exists in the DB, edit it
                if (dbProp != null)
                {
                    // If the postProp is not an empty string, is not null, 
                    // and is different than the dbProp, change it
                    if (postProp.Value != "" && postProp.Value != null)
                    {
                        if (dbProp.Value != postProp.Value)
                        {
                            dbProp.Value = postProp.Value;
                            dbAsset.DateLastModified = DateTime.Now;
                        }
                    }
                    // If the postProp is null or an empty string, remove it from the db
                    else
                    {
                        db.PropertyValues.Remove(dbProp);
                        dbAsset.DateLastModified = DateTime.Now;
                    }
                }
                // If the postProp does not exist in the db, and is not an empty string or null, add it
                else
                {
                    if (postProp.Value != "" && postProp.Value != null)
                    {
                        PropertyValue newProp = new PropertyValue();
                        // newProp.AssetID = postAsset.AssetID;
                        newProp.PropertyID = postProp.PropertyID;
                        newProp.Value = postProp.Value;
                        dbAsset.PropertyValues.Add(newProp);
                        dbAsset.DateLastModified = DateTime.Now;
                    }
                }
            }
            db.SaveChanges();
            // If this is a non tracked asset, check its on hand value
            if (dbAsset.AssetType.Tracked == 0)
            {
                checkTide(dbAsset.AssetID);
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

        public void checkTide(int id)
        {
            AssetViewModel asset = new AssetViewModel(db.Assets.Find(id));
            if (asset != null)
            {
                int onHand = 0;
                int lowTide = 0;
                int highTide = 0;
                String assetName = "";
                String[] emails = null;
                foreach (var prop in asset.AssetType.Properties)
                {
                    //21 is on hand, 22 is low tide, 23 is high tide, 41 is assetName 
                    if (prop.Name == "On Hand")
                    {
                        onHand = int.Parse(prop.Value);
                    }
                    else if (prop.Name == "Low Tide")
                    {
                        lowTide = int.Parse(prop.Value);
                    }
                    else if (prop.Name == "High Tide")
                    {
                        highTide = int.Parse(prop.Value);
                    }
                    else if (prop.Name == "Name")
                    {
                        assetName = prop.Value;
                    }
                    else if (prop.Name == "Emails to Notify")
                    {
                        emails = prop.Value.Split(';');
                    }
                }
                if (onHand < lowTide)
                {
                    SendMail("Low Tide", assetName, emails);
                }
                if (onHand > highTide)
                {
                    SendMail("High Tide", assetName, emails);
                }

            }
        }

        [HttpPost]
        public void checkAllTides()
        {
        }

        public void SendMail(String tide, String assetName, String[] emails)
        {
            if (emails != null)
            {
                //Mail Notification 
                MailMessage alert = new MailMessage();
                foreach (var email in emails)
                {
                    alert.To.Add(new MailAddress(email));
                }
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
}
