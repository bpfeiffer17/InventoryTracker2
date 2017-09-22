//Globals
var dropDowns = [];

$(document).ready(function () {
    /**
     * Retrieve drop downs from db and then get the asset
     */
    $.get('/Scripts/DummyData/DropDowns.json', function (data, status) {
        for (var dd of data) {
            dropDowns.push(dd);
        }
        //Retrieve asset from db and then add its properties to the page
        $.get('/Scripts/DummyData/AssetTypes/' + assetTypeId + '.json', function (data, status) {
            var assetType = data;
            $('#assetTypeName').val(assetType.name);
            $('#assetTypeDescription').val(assetType.description);
            for (var prop of assetType.properties) {
                addProp(prop);
            }
        });
    });
});

/**
 * Add a new div with elements that edit a property on this asset type
 * 
 * @param {any} prop
 */
function addProp(prop) {
    $('#propertiesDiv').append(`
        <div>
            <div style="display:inline-block">
                <label>Property Type: </label>
                <select class="form-control">
                    <option ${prop.type === 'String' ? 'selected':''}>String</option>
                    <option ${prop.type === 'Number' ? 'selected':''}>Number</option>
                    <option ${prop.type === 'Drop Down' ? 'selected':''}>Drop Down</option>
                </select>
            </div>
            <div style="display:inline-block">
                <label>Property Name: </label>
                <input value="${prop.name}" class="form-control" />
            </div>
            <div style="display:inline-block">
                <label>Property Unit: </label>
                <input value="${prop.unit ? prop.unit:''}" class="form-control" />
            </div>
            <div style="display:inline-block" id="dropDownDiv-${prop.propertyId}"></div>
        </div>
    `);
    if (prop.type === 'Drop Down') {
        addDropDown(prop.propertyId, prop.dropDownId);
    }
}

/**
 *  Add the needed elements to the #dropDownDiv-[propertyId]
 */
function addDropDown(propertyId, dropDownId) {
    var dropDown = findDropDownById(dropDownId);
    var select = `<select class="form-control" onchange="addDropDown('${propertyId}', this.value)" >`;
    for (var dd of dropDowns) {
        select += `<option value="${dd.dropDownId}" ${dd.dropDownId == dropDownId ? 'selected':''}>${dd.name}</option>`;
    }
    select += `</select>`;
    var values = ``;
    dropDown.values.forEach(function(val, i) {
        values += `<input style="width: 100px" value="${val}" class="form-control" onblur="changeDropDownValue('${dropDownId}', ${i}, this.value)" />`;
    });
    var template = `
        <div style="display:inline-block">
            <label>Select Drop Down: </label>
            ${select}
        </div>
        <div style="display:inline-block">
            <label>Drop Down Options: </label><a onClick="addNewVal('${propertyId}', '${dropDownId}')"><label> (New)</label></a>
            ${values}
        </div>
    `;
    $('#dropDownDiv-' + propertyId).html(template);
}

function findDropDownById(dropDownId) {
    for (var d of dropDowns) {
        if (d.dropDownId == dropDownId) {
            return d;
        }
    }
}

function setListeners() {
    /**
     * When a user clicks #addPropButton, add a property with a propertyId of 'new'
     */
    $('#addPropButton').click(function () {
        addProp({
            propertyId: 'new',
            name: '',
            type: 'String',
            unit: '',
            dropDownId: ''
        });
    });
}

function addNewVal(propertyId, dropDownId) {
    var dropDown = findDropDownById(dropDownId);
    dropDown.values.push('');
    addDropDown(propertyId, dropDownId);
}

function changeDropDownValue(dropDownId, valueIndex, value) {
    var dropDown = findDropDownById(dropDownId);
    dropDown.values[valueIndex] = value;
}