using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryTracker.Models.ViewModels
{
    public class AssetTypeViewModel
    {
        public int AssetTypeID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Boolean Tracked { get; set; }
        public PropertyViewModel[] Properties { get; set; }
        public Boolean Active { get; set; }

        public AssetTypeViewModel(AssetType entityAssetType)
        {
            this.AssetTypeID = entityAssetType.AssetTypeID;
            this.Name = entityAssetType.Name;
            this.Description = entityAssetType.Description;
            this.Tracked = Convert.ToBoolean(entityAssetType.Tracked);
            this.Active = Convert.ToBoolean(entityAssetType.Active);
            this.Properties = new PropertyViewModel[entityAssetType.Properties.Count];
            for (var i = 0; i < entityAssetType.Properties.Count(); i++)
            {
                this.Properties[i] = new PropertyViewModel(entityAssetType.Properties.ElementAt(i));
            }
        }
    }
}