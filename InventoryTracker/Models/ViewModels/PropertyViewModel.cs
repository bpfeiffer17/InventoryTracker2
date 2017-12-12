using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryTracker.Models.ViewModels
{
    public class PropertyViewModel
    {
        public int PropertyID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Unit { get; set; }
        public Nullable<int> DropDownID { get; set; }
        public string Value { get; set; }
        public Boolean Active { get; set; }
        public Boolean Required { get; set; }
        public Boolean Tracked { get; set; }
        public Boolean NonTracked { get; set; }
        public Nullable<int> Order { get; set; }

        public PropertyViewModel() { }

        public PropertyViewModel(Property entityProperty)
        {
            this.PropertyID = entityProperty.PropertyID;
            this.Name = entityProperty.Name;
            this.Type = entityProperty.Type;
            this.Unit = entityProperty.Unit;
            this.DropDownID = entityProperty.DropDownID;
            this.Value = "";
            this.Active = Convert.ToBoolean(entityProperty.Active);
            this.Required = Convert.ToBoolean(entityProperty.Required);
            this.Tracked = Convert.ToBoolean(entityProperty.Tracked);
            this.NonTracked = Convert.ToBoolean(entityProperty.NonTracked);
            this.Order = entityProperty.Order;
        }

        public Property getEntityProperty()
        {
            return new Property
            {
                PropertyID = this.PropertyID,
                Name = this.Name,
                Type = this.Type,
                Unit = this.Unit,
                DropDownID = this.DropDownID,
                Active = Convert.ToByte(this.Active),
                Required = Convert.ToByte(this.Required),
                Tracked = Convert.ToByte(this.Tracked),
                NonTracked = Convert.ToByte(this.NonTracked),
                Order = this.Order
            };
        }
    }
}