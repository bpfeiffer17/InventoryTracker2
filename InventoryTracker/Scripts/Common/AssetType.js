/**
 * AssetType class that holds properties and functions of an AssetType object.
 * 
 * @author - Brandon Pfeiffer <bpfeiffer17@jcu.edu>
 */
class AssetType {
    /**
     * Set some properties of the AssetType from the properties of the options object.
     * 
     * @param {any} options - an object with properties to set to our new AssetType
     */
    constructor(options) {
        this.id = options.assetTypeId;
        this.name = options.name;
        this.description = options.description;
        this.setProperties(options.properties);
    }

    /**
     * Initialize the properties array on the AssetType and add property objects to it for every object in the given properties.
     * 
     * @param {any[]} properties 
     */
    setProperties(properties) {
        this.properties = [];
        if (properties) {
            for (var prop of properties) {
                this.properties.push(new Property(prop))
            }
        }
    }

    /**
     * Return the property with the given id
     * 
     * @param {any} id - the id of the property to return
     * @returns {Property} - the property with the given id
     */
    findProperty(id) {
        for (var prop of this.properties) {
            if (prop.id == id) {
                return prop;
            }
        }
    }

    /**
     * Set a property on the property with the given id to the given value.
     * 
     * Properties have properties themselves.  Given the property id, we find that property
     * and set the given property to the value that is also given.
     * 
     * @param {any} id - id of the property to modify
     * @param {string} property - the name of the property to modify on the property.
     * @param {any} value - the value to set the property's property to.
     */
    setPropertyProperty(id, property, value) {
        this.findProperty(id)[property] = value;
    }
}