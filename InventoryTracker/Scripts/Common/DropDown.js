/**
 * DropDown class that holds properties and functions of an DropDown object.
 * 
 * @author - Brandon Pfeiffer <bpfeiffer17@jcu.edu>
 */
class DropDown {
    /**
     * Set some properties of the DropDown from the properties of the options object.
     * 
     * @param {any} options - an object with properties to set to our new DropDown
     */
    constructor(options) {
        this.id = options.DropDownID;
        this.name = options.Name;
        this.values = options.Values;
        this.active = options.Active;
    }

    /**
     * Return whether or not the DropDown is new.
     * 
     * This is determined by its id.  If the id contains the string 'new',
     * then it is new and has not yet been saved to the database.
     * 
     * @returns {boolean} - whether or not the DropDown is new
     */
    isNew() {
        let structure= {
            DropDownID: (this.id + '').indexOf('new') >= 0 ? 0:this.id, 
            Name: this.name,
            Value: this.value,
            Active: this.active
           }

    return structure;
    }
 }