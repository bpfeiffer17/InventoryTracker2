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
        this.id = options.propertyId;
        this.name = options.name;
        this.type = options.type;
        this.unit = options.unit;
        this.dropDownId = options.dropDownId;
    }
}