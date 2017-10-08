/**
 * Javascript file for front end functionality of the Edit view of the AssetType controller
 * 
 * @author - Brandon Pfeiffer <bpfeiffer17@jcu.edu>
 */

var dropDownHelper;
var assetType;

//Code to be executed when the page is done loading.
$(document).ready(function () {
    /**
     * Retrieve DropDowns JSON and AssetType JSON from the data base
     * so that we can create DropDownHelper and AssetType objects.
     */
    $.get('/Scripts/DummyData/DropDowns.json', function (data, status) {
        //Create a DropDownHelper object with the returned JSON data
        dropDownHelper = new DropDownHelper(dropDowns);
        //Retrieve an AssetType from the database with the given assetTypeId
        $.get('/Scripts/DummyData/AssetTypes/' + assetTypeId + '.json', function (data, status) {
            //Create an AssetType object with the returned JSON data
            //assetType = new AssetType(data);
            assetType = new AssetType(assetTypeJSON);
            //Add the properties of the new AssetType to the view
            setPage();
        });
    });
    //Set our event listeners on the page
    setListeners();
});

/**
 * Add a new div to #propertiesDiv with elements that edit a property of the AssetType
 * 
 * @param {Property} prop - a Property of the Asset Type to be added to the view 
 */
function addProp(prop) {
    $('#propertiesDiv').append(`
        <div class="top-align">
            <div style="display:inline-block">
                <label>Property Type: </label>
                <select class="form-control" onchange="setType('${prop.id}', this.value)">
                    <option ${prop.type === 'String' ? 'selected':''}>String</option>
                    <option ${prop.type === 'Number' ? 'selected':''}>Number</option>
                    <option ${prop.type === 'Drop Down' ? 'selected':''}>Drop Down</option>
                </select>
            </div>
            <div style="display:inline-block">
                <label>Property Name: </label>
                <input value="${prop.name}" class="form-control" onblur="assetType.setPropertyProperty('${prop.id}', 'name', this.value)" />
            </div>
            <div style="display:inline-block">
                <label>Property Unit: </label>
                <input value="${prop.unit ? prop.unit:''}" class="form-control" onblur="assetType.setPropertyProperty('${prop.id}', 'unit', this.value)" />
            </div>
            <div class="top-align" style="display:inline-block" id="dropDownDiv-${prop.id}"></div>
        </div>
    `);
    //If the Property is of type 'Drop Down', append DropDown specific html to the Property div
    if (prop.type === 'Drop Down') {
        addDropDown(prop.id, prop.dropDownId);
    }
}

/**
 * Append DropDown specific html to the Property div
 * 
 * @param {any} propertyId - id of the Property to add 
 * @param {any} dropDownId - id of the DropDown to add to the Property div
 */
function addDropDown(propertyId, dropDownId) {
    //Retrieve the DropDown that we are going to add from the DropDownHelper
    var dropDown = dropDownHelper.findDD(dropDownId);
    //Begin creating the select element where the user will select which DropDown to attribute to the Property
    var select = `<select class="form-control" onchange="addDropDown('${propertyId}', this.value)" >`;
    //For each DropDown in the DropDownHelper, add an option to the select element we are creating
    for (var dd of dropDownHelper.dropDowns) {
        select += `<option value="${dd.id}" ${dd.id == dropDownId ? 'selected':''}>${dd.name}</option>`;
    }
    //Finish creating the select element
    select += `</select>`;
    //Begin creating a values template where we will show the values that are part of a DropDown
    var values = ``;
    //For each value in our DropDown, add an input to the values template we are creating
    dropDown.values.forEach(function(val, i) {
        values += `<input style="width: 100px" value="${val}" class="form-control" onblur="dropDownHelper.setDDVal('${dropDownId}', ${i}, this.value)" />`;
    });
    //Create a template with html for editing the DropDown
    var template = `
        <div style="display:inline-block">
            ${!dropDown.isNew() ?
                `<label>Select Drop Down: </label><a onClick="addNewDD('${propertyId}')"><label> (New)</label></a> ${select}`
                :
                `<label>Drop Down Name:</label><a onClick="cancelNewDD('${propertyId}', '${dropDownId}')"><label>(Cancel)</label></a><input class="form-control" value="${dropDown.name}" />`
            }
        </div>
        <div style="display:inline-block">
            <label>Drop Down Options: </label>${dropDown.isNew() ? `<a onClick="addNewDDVal('${dropDownId}')"><label> (New)</label></a>`:``}
            ${values}
        </div>
    `;
    //Add the created template to the #dropDownDiv-[PropertyID]
    $('#dropDownDiv-' + propertyId).html(template);
}

/**
 * Set JQuery listeners
 */
function setListeners() {
    /**
     * When a user clicks #addPropButton, add a Property with a 
     * propertyId of 'new' plus the current timestamp for uniqueness
     */
    $('#addPropButton').click(function () {
        var newID = assetType.addNewProperty();
        var newProperty = assetType.findProperty(newID);
        addProp(newProperty);
    });
}

/**
 * Add a new value to the DropDown with the given id and reset all of the Property divs
 * 
 * @param {any} dropDownId - id of the DropDown to add a value to
 */
function addNewDDVal(dropDownId) {
    dropDownHelper.addNewDDVal(dropDownId);
    setPage();
}

/**
 * Add a new DropDown to the DropDownHelper and reset all of the Property divs
 * 
 * @param propertyId - id of the Property change the DropDown value on
 */
function addNewDD(propertyId) {
    var newDDId = dropDownHelper.addNewDD();
    assetType.setPropertyProperty(propertyId, 'dropDownId', newDDId);
    setPage();
}

/**
 * Cancel the addition of the new DropDown and reset all of the Property divs
 * 
 * @param propertyId - id of the Property change the DropDown value on
 * @param {any} dropDownId - id of the DropDown to cancel
 */
function cancelNewDD(propertyId, dropDownId) {
    dropDownHelper.removeDD(dropDownId);
    assetType.setPropertyProperty(propertyId, 'dropDownId', dropDownHelper.dropDowns[0].id);
    setPage();
}

/**
 * Clear out the #propertiesDiv and for each Property on the AssetType, add a html to edit to Property to #propertiesDiv
 */
function setPage() {
    $('#propertiesDiv').html('');
    $('#assetTypeName').val(assetType.name);
    $('#assetTypeDescription').val(assetType.description);
    $('#propertiesDiv').append(`
        <div>Default Properties: ${assetType.tracked ? `Name, Serial Number, Deployed, Location`:`High Tide, Low Tide, Count`}<div>
    `);
    for (var prop of assetType.properties) {
        addProp(prop);
    }
}

/**
 * Set the type of the Property with the given propertyId
 * 
 * Also check to see what type the Property is being set to.  If it is 'Drop Down',
 * give the Property a dropDownId, if now make the dropDownId null.
 */
function setType(propertyId, type) {
    var prop = assetType.findProperty(propertyId);
    prop.type = type;
    if (prop.type === 'Drop Down') {
        assetType.setPropertyProperty(propertyId, 'dropDownId', dropDownHelper.dropDowns[0].id);
    }else {
        assetType.setPropertyProperty(propertyId, 'dropDownId', null);
    }
    setPage();
}

function save() {
    var data = {
        assetType: JSON.stringify(assetType)
    }
    $.post('/AssetType/Edit', data, function (data, status) {
        console.log(response);
    });
}