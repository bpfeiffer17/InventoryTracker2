using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryTracker.Models.ViewModels
{
    public class AssetViewModel
    {
        public int AssetID { get; set; }
        public AssetTypeViewModel AssetType { get; set; }
        public int AssetTypeID { get; set; }
        public string DateAdded { get; set; }
        public string DateLastModified { get; set; }

        public AssetViewModel() { }

        public AssetViewModel(Asset entityAsset)
        {
            this.AssetID = entityAsset.AssetID;
            this.AssetType = new AssetTypeViewModel(entityAsset.AssetType);
            this.AssetTypeID = entityAsset.AssetTypeID;
            this.DateAdded = entityAsset.DateAdded.ToString("MM/dd/yy H:mm");
            this.DateLastModified = entityAsset.DateLastModified.ToString("MM/dd/yy H:mm");
            // Set the values for the properties
            var i = 0;
            foreach (PropertyViewModel prop in this.AssetType.Properties)
            {
                this.AssetType.Properties[i].Value = this.getPropertyValue(prop, entityAsset);
                i++;
            }
        }

        private string getPropertyValue(PropertyViewModel prop, Asset entityAsset)
        {
            foreach (PropertyValue propValue in entityAsset.PropertyValues)
            {
                if (propValue.PropertyID == prop.PropertyID)
                {
                    return propValue.Value;
                }
            }
            return "";
        }
    }
}