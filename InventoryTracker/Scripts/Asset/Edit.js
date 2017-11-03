

var dropDownHelper;
var asset;

$(document).ready(function(){
/*
*Retreive asset from db and then add its properties to the page
*/
    $.get('/AssetType/DropDownHelper', function (data,status){
        dropDownHelper = new DropDownHelper(JSON.parse(data));
        $.get('/Asset/JSON/' + assetTypeId, function (data,status){
            asset = new Asset(JSON.parse(data));
            setPage();
        });
    });
});

function setPage() {
    setProperties();
}

function setProperties() {
    $('#propertiesDiv').html('');
    var propTemplate = ``;
    for (var prop of asset.AssetType.properties) {
        if (prop.type === 'String'  || prop.type === 'Number') {
            propTemplate = `
            <div>
                <label>${prop.name}</label>
                <input value="${prop.value}" onblur="setPropertyValue('${prop.id}', this.value)" />
            </div>`;
        }else if (prop.type === 'Drop Down') {
            propTemplate = `
            <div>
                <label>${prop.name}</label>
                <select onchange="setPropertyValue('${prop.id}', this.value)">
                    <option ${prop.value === value ? 'selected' : ''}></option>`;
            for (var value of dropDownHelper.findDD(prop.dropDownId).values) {
                propTemplate += `<option ${prop.value === value ? 'selected' : ''}>${value}</option>`;
            }
            propTemplate += `
                </select>
            </div>
            `;
        }
        $('#propertiesDiv').append(propTemplate);
    }
}

function setPropertyValue(propId, value) {
    asset.AssetType.setPropertyProperty(propId, 'value', value);
    setProperties();
}

function save() {
    var data = {
        asset: asset.getSaveStructure()
    }
    $.post('/Asset/SaveAsset/' + assetTypeId, data, function (data, status) {
        console.log(data);
    });
}