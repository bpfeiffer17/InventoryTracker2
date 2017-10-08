/**
 * DropDownHelper class that holds properties and functions of a DropDownHelper object.
 * 
 * @author - Brandon Pfeiffer <bpfeiffer17@jcu.edu>
 */
class DropDownHelper {
    /**
     * Set some properties of the DropDownHelper from the properties of the options object.
     * 
     * @param {any} options - an object with properties to set to our new DropDownHelper
     */
    constructor(options) {
        this.setDropDowns(options);
    }

    /**
     * Return the DropDown that has the given id
     * 
     * @param {any} id - the id of the DropDownHelper to return
     * @returns {DropDown} - the DropDown with the given id
     */
    findDD(id) {
        for (var dd of this.dropDowns) {
            if (dd.id == id) {
                return dd;
            }
        }
    }

    /**
     * Set a value for the DropDown with the given id
     * 
     * @param {any} ddId - id of the DropDown to modify a value on
     * @param {number} valIndex - the index of the value to modify
     * @param {string} val - the string to modify the value to
     */
    setDDVal(ddId, valIndex, val) {
        this.findDD(ddId).values[valIndex] = val;
    }

    /**
     * Add a new blank value to the DropDown with the given id
     * 
     * @param {any} ddId - id of the DropDown to add a blank value to
     */
    addNewDDVal(ddId) {
        this.findDD(ddId).values.push('');
    }

    /**
     * Add a new blank DropDown object to the dropDowns array
     * 
     * @returns {string} - the id of the new DropDown 
     */
    addNewDD() {
        var newId = 'new' + Date.now();
        this.dropDowns.push(new DropDown({
            DropDownID: newId,
            Name: "New",
            Values: ['']
        }));
        return newId;
    }

    /**
     * Remove the DropDown with the given id from the DropDowns array
     * 
     * @param {any} id - the id of the DropDown to remove 
     */
    removeDD(id) {
        this.dropDowns.forEach((dd, i) => {
            if (dd.id == id) {
                this.dropDowns.splice(i, 1);
            }
        });
    }

    /**
     * Initialize the dropDowns array and add DropDown objects to it from the given dropDowns array.
     * 
     * @param {any[]} dropDowns - an array of JSON objects with properties needed to create a DropDown
     */
    setDropDowns(dropDowns) {
        this.dropDowns = [];
        if (dropDowns) {
            for (var dd of dropDowns) {
                this.dropDowns.push(new DropDown(dd));
            }
        }
    }
}