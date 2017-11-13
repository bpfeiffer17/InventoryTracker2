/**
 * Asset class that holds properties and functions of an Asset object.
 * 
 * @author - Brandon Pfeiffer <bpfeiffer17@jcu.edu>
 */
class Asset {
    /**
     * Set some properties of the Asset from the properties of the options object.
     * 
     * @param {any} options - an object with properties to set to our new Asset
     */
    constructor(options) {
        this.id = options.AssetID;
        this.AssetType = new AssetType(options.AssetType);
    }

    /**
     * Get a structure that will mach the c# model
     * 
     * @returns - a json structure that resembles the c# Asset model
     */
    getSaveStructure() {
        var structure = {
            AssetID: this.id,
            AssetType: this.AssetType.getSaveStructure()
        }
        return structure;
    }
}