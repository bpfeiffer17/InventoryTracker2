/**
 * Property class that holds properties and functions of a Property object.
 * 
 * @author - Brandon Pfeiffer <bpfeiffer17@jcu.edu>
 */
class Property {
    /**
     * Set some properties of the Property from the properties of the options object.
     * 
     * @param {any} options - an object with properties to set to our new AssetType
     */
    constructor(options) {
        this.id = options.PropertyID;
        this.name = options.Name;
        this.type = options.Type;
        this.unit = options.Unit;
        this.dropDownId = options.DropDownID;
        this.value = options.Value;
        this.active = options.Active;
        this.required = options.Required;
        this.tracked = options.Tracked;
        this.nonTracked = options.NonTracked;
        this.order = options.Order;
    }

     /**
     * Get a structure that will mach the c# Property model
     * 
     * @returns - a json structure that resembles the c# Property model
     */
    getSaveStructure() {
        let structure = {
            PropertyID: (this.id + '').indexOf('new') >= 0 ? 0:this.id,
            Name: this.name,
            Type: this.type,
            Unit: this.unit,
            DropDownID: this.dropDownId,
            Value: this.value,
            Active: this.active,
            Required: this.required ? 1 : 0,
            Order: this.order
        }
        return structure;
    }
}