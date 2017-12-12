using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using InventoryTracker.Models;
using InventoryTracker.Models.ViewModels;

namespace InventoryTracker.Controllers
{
    public class DropDownController : Controller
    {
        private InventoryTrackerEntities db = new InventoryTrackerEntities();

        // GET: DropDown
        public ActionResult Index()
        {
            return View("Browse");
        }

        // GET: DropDown/Browse
        public ActionResult Browse()
        {
            IEnumerable<DropDown> dbDropDowns = db.DropDowns.ToList();
            DropDownViewModel[] dropDowns = new DropDownViewModel[dbDropDowns.Count()];
            for (var i = 0; i < dbDropDowns.Count(); i++)
            {
                dropDowns[i] = new DropDownViewModel(dbDropDowns.ElementAt(i));
            }
            return View(dropDowns);
        }

        // GET: DropDown/JSON/{id}
        public string JSON(int id = 0)
        {
            return new JavaScriptSerializer().Serialize(this.getDropDownViewModel(id));
        }

        // GET: DropDown/Edit/{id}
        public ActionResult Edit(int id = 0)
        {
            return View(this.getDropDownViewModel(id));
        }

        // GET: DropDown/View/{id}
        public ActionResult View(int id = 0)
        {
            return View(this.getDropDownViewModel(id));
        }

        private DropDownViewModel getDropDownViewModel(int id = 0)
        {
            DropDownViewModel dropDown;

            if (id == 0)
            {
                dropDown = new DropDownViewModel(new DropDown());
            }
            else
            {
                dropDown = new DropDownViewModel(db.DropDowns.Find(id));
            }

            return dropDown;
        }

        public string saveDropDown(DropDownViewModel postDropDown)
        {
            DropDown dbDropDown;
            if (postDropDown.DropDownID != 0)
            {
                dbDropDown = db.DropDowns.Find(postDropDown.DropDownID);
                dbDropDown.Name = postDropDown.Name;
            }
            else
            {
                dbDropDown = new DropDown();
                dbDropDown.Name = postDropDown.Name;
                db.DropDowns.Add(dbDropDown);
            }
            // Delete all of the dbDropDown's values
            foreach (var value in dbDropDown.DropDownValues.ToList())
            {
                db.DropDownValues.Remove(value);
            }
            // Add all of the values from postDropDown
            foreach (var value in postDropDown.Values)
            {
                var newDropDownValue = new DropDownValue();
                newDropDownValue.Value = value;
                dbDropDown.DropDownValues.Add(newDropDownValue);
            }
            db.SaveChanges();
            return "";
        }

        public string DeleteDropDown(int id)
        {
            DropDown dbDropDown = db.DropDowns.Find(id);
            if (dbDropDown != null)
            {
                // Remove all of the DropDownValues in dbDropDown
                foreach (var value in dbDropDown.DropDownValues.ToList())
                {
                    db.DropDownValues.Remove(value);
                }
                // Remove the DropDown itself
                db.DropDowns.Remove(dbDropDown);
                // Go through all of the properties that have this DropDownID, and change them to string properties
                foreach (var prop in db.Properties.Where(prop => prop.DropDownID == id))
                {
                    prop.DropDownID = null;
                    prop.Type = "String";
                }
                db.SaveChanges();
            }
            return "";
        }
    }
}