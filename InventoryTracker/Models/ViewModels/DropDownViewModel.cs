using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryTracker.Models.ViewModels
{
    public class DropDownViewModel
    {
        public int DropDownID { get; set; }
        public string Name { get; set; }
        public string[] Values { get; set; }

        public DropDownViewModel() { }

        public DropDownViewModel(DropDown entityDropDown)
        {
            this.DropDownID = entityDropDown.DropDownID;
            this.Name = entityDropDown.Name;
            this.Values = new string[entityDropDown.DropDownValues.Count];
            for (var i = 0; i < entityDropDown.DropDownValues.Count(); i++)
            {
                this.Values[i] = entityDropDown.DropDownValues.ElementAt(i).Value;
            }
            // Order this.Values alphabetically
            this.Values = this.Values.OrderBy(value => value).ToArray();
        }
    }
}