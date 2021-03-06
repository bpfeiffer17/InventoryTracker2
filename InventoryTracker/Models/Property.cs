//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InventoryTracker.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Property
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Property()
        {
            this.PropertyValues = new HashSet<PropertyValue>();
            this.AssetTypes = new HashSet<AssetType>();
        }
    
        public int PropertyID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Unit { get; set; }
        public Nullable<int> DropDownID { get; set; }
        public byte Active { get; set; }
        public byte Required { get; set; }
        public byte Tracked { get; set; }
        public byte NonTracked { get; set; }
        public Nullable<int> Order { get; set; }
    
        public virtual DropDown DropDown { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PropertyValue> PropertyValues { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AssetType> AssetTypes { get; set; }
    }
}
