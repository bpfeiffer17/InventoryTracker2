/**
 * Javascript file for front end functionality of the Edit view of the AssetType controller
 * 
 * @author - Brandon Pfeiffer <bpfeiffer17@jcu.edu>
 */

var dropDownHelper;
var assetType;

//Code to be executed when the page is done loading.
$(document).ready(function () {
    loadingModal.show();
    $.get('/AssetType/DropDownHelper', function (data) {
        dropDownHelper = new DropDownHelper(JSON.parse(data));
        $.get(`/AssetType/JSON/${assetTypeID ? assetTypeID : 0}`, function (data) {
            assetType = new AssetType(JSON.parse(data));
            setPage();
            setListeners();
            loadingModal.hide();
        });
    });
});

/**
 * Add a new div to #propertiesDiv with elements that edit a property of the AssetType
 * 
 * @param {Property} prop - a Property of the Asset Type to be added to the view 
 */
function addProp(prop) {
    if (prop.active && !prop.tracked && !prop.nonTracked) {
        $('#propertiesDiv').append(`
            <div class="row">
                <div class="col-sm-3">
                    <input value="${prop.name}" class="form-control" onblur="setPropName('${prop.id}', 'name', this.value)" />
                </div>
                <div class="col-sm-3">
                    <select class="form-control" onchange="setType('${prop.id}', this.value)">
                        <option ${prop.type === 'String' ? 'selected':''}>String</option>
                        <option ${prop.type === 'Number' ? 'selected':''}>Number</option>
                        <option ${prop.type === 'Drop Down' ? 'selected':''}>Drop Down</option>
                    </select>
                </div>
                <div class="col-sm-1">
                    <input value="${prop.unit ? prop.unit:''}" class="form-control" onblur="assetType.setPropertyProperty('${prop.id}', 'unit', this.value)" />
                </div>
                <div class="col-sm-3 center" id="dropDownDiv-${prop.id}">${prop.type !== 'Drop Down' ? `--` : ``}</div>
                <div class="col-sm-1 center"><input type="checkbox" ${prop.required ? 'checked' : ''} onclick="assetType.setPropertyProperty('${prop.id}', 'required', this.checked)"></div>
                <div class="col-sm-1" onclick="deleteProp('${prop.id}')"><button>DELETE</button></div>
            </div>
        `);
        //If the Property is of type 'Drop Down', append DropDown specific html to the Property div
        if (prop.type === 'Drop Down') {
            addDropDown(prop.id, prop.dropDownId);
        }
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
    var select = `<select class="form-control" onchange="assetType.setPropertyProperty('${propertyId}', 'dropDownId', this.value)" >`;
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
            ${select}
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
        <div class="row">
            <div class="col-sm-3">
                <h4>Name: </h4>
            </div>
            <div class="col-sm-3">
                <h4>Type: </h4>
            </div>
            <div class="col-sm-1">
                <h4>Unit: </h4>
            </div>
            <div class="col-sm-3">
                <h4>Drop Down: </h4>
            </div>
            <div class="col-sm-1">
                <h4>Required: </h4>
            </div>
            <div class="col-sm-1">
                <h4>Delete: </h4>
            </div>
        </div>
    `);
    for (var prop of assetType.properties) {
        addProp(prop);
    }
    if (assetType.tracked) {
        $('#tracked').prop('checked', true);
    } else {
        $('#nontracked').prop('checked', true);
    }
    if (assetType.id) {
        $('#tracked').prop('disabled', true);
        $('#nontracked').prop('disabled', true);
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
    loadingModal.show();
    var data = {
        assetType: JSON.stringify(assetType.getSaveStructure())
    }
    $.post('/AssetType/SaveAsset', data, function (data, status) {
        loadingModal.hide();
        window.location = '/AssetType/Browse';
    });
}

function deleteProp(propertID) {
    confirmModal.show('Are you sure you want to delete this property?', () => {
        assetType.findProperty(propertID).active = false;
        setPage();
    });
}

function setPropName(propId, property, value) {
    assetType.setPropertyProperty(propId, property, value);
    var nameCount = 0;
    for (let prop of assetType.properties) {
        if (prop.name.toLowerCase() === value.toLowerCase() && prop.active) {
            nameCount++;
        }
    }
    if (nameCount > 1) {
        assetType.setPropertyProperty(propId, property, '');
        alertModal.show(`There is already a property with the name "${value}"`);
        setPage();
    }
}