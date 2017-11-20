

var dropDownHelper;
var asset;

$(document).ready(function(){
    /*
    *Retreive asset from db and then add its properties to the page
    */
    $.get('/AssetType/DropDownHelper', function (data,status) {
        dropDownHelper = new DropDownHelper(JSON.parse(data));
        $.get(`/Asset/JSON/${assetId}${assetTypeId ? `?assetTypeID=${assetTypeId}`:''}`, function (data,status){
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
        if (prop.active) {
            if (prop.type === 'String' || prop.type === 'Number') {
                propTemplate = `
            <div class="row">
                <div class="col-sm-3">${prop.name}</div>
                <div class="col-sm-9">
                    <input class="form-control" value="${prop.value}" onblur="setPropertyValue('${prop.id}', this.value)" />
                </div>
            </div>`;
            } else if (prop.type === 'Drop Down') {
                propTemplate = `
            <div class="row">
                <div class="col-sm-3">${prop.name}</div>
                <div class="col-sm-9">
                    <select class="form-control" onchange="setPropertyValue('${prop.id}', this.value)">
                        <option ${prop.value === value ? 'selected' : ''}></option>`;
                for (var value of dropDownHelper.findDD(prop.dropDownId).values) {
                    propTemplate += `<option ${prop.value === value ? 'selected' : ''}>${value}</option>`;
                }
                propTemplate += `
                    </select>
                </div>
            </div>
            `;
            }
            $('#propertiesDiv').append(propTemplate);
        }
    }
    // Add the type to the UI
    $('#type').html(asset.AssetType.name);
}

function setPropertyValue(propId, value) {
    asset.AssetType.setPropertyProperty(propId, 'value', value);
    setProperties();
}

function save() {
    loadingModal.show();
    var data = {
        asset: asset.getSaveStructure()
    }
    $.post('/Asset/SaveAsset/' + assetTypeId, data, function (data, status) {
        loadingModal.hide();
        window.location = '/Asset/Browse';
    });
}