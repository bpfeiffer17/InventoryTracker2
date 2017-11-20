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
    
    public partial class DropDown
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DropDown()
        {
            this.DropDownValues = new HashSet<DropDownValue>();
            this.Properties = new HashSet<Property>();
        }
    
        public int DropDownID { get; set; }
        public string Name { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DropDownValue> DropDownValues { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Property> Properties { get; set; }

        public DropDownBare getDropDownBare()
        {
            DropDownBare dropDownBare = new DropDownBare();
            dropDownBare.DropDownID = this.DropDownID;
            dropDownBare.Name = this.Name;
            dropDownBare.Values = new string[this.DropDownValues.Count];
            var i = 0;
            foreach (DropDownValue val in this.DropDownValues)
            {
                dropDownBare.Values[i] = val.Value;
                i++;
            }
            return dropDownBare;
        }
    }

    public class DropDownBare
    {
        public int DropDownID { get; set; }
        public string Name { get; set; }
        public string[] Values { get; set; }
    }
}
