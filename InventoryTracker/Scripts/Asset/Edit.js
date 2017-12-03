

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
    var propIndex = 1;
    var propTemplate = ``;
    for (var prop of asset.AssetType.properties) {
        if (prop.active) {
            if (prop.type === 'String' || prop.type === 'Number') {
                propTemplate = `
            <div class="row">
                <div class="col-sm-3">${prop.name}</div>
                <div class="col-sm-9">
                    <input
                        class="form-control ${prop.required ? 'required' : ''} ${prop.type === 'Number' ? 'number' : ''}" 
                        value="${prop.value}" 
                        onblur="setPropertyValue('${prop.id}', this.value)" 
                        tabindex="${propIndex}"
                    />
                    <span style="color:red"></span>
                </div>
            </div>`;
            } else if (prop.type === 'Drop Down') {
                propTemplate = `
                    <div class="row">
                        <div class="col-sm-3">${prop.name}</div>
                        <div class="col-sm-9">
                            <select class="form-control ${prop.required ? 'required' : ''}" onchange="setPropertyValue('${prop.id}', this.value)" tabindex="${propIndex}">
                                <option ${prop.value === value ? 'selected' : ''}></option>`;
                for (var value of dropDownHelper.findDD(prop.dropDownId).values) {
                    propTemplate += `<option ${prop.value === value ? 'selected' : ''}>${value}</option>`;
                }
                propTemplate += `
                            </select>
                            <span></span>
                        </div>
                    </div>
                `;
            }
            $('#propertiesDiv').append(propTemplate);
            propIndex++;
        }
    }
    // Add the type to the UI
    $('#type').html(asset.AssetType.name);
    $('#dateAdded').html(asset.dateAdded);
    $('#dateLastModified').html(asset.dateLastModified);
}

function setPropertyValue(propId, value) {
    asset.AssetType.setPropertyProperty(propId, 'value', value);
    //setProperties();
}

function save() {
    loadingModal.show();
    if (checkFields()) {
        var data = {
            postAsset: asset.getSaveStructure()
        }
        $.post('/Asset/SaveAsset/' + assetTypeId, data, function (data, status) {
            loadingModal.hide();
            window.location = '/Asset/Browse/' + asset.AssetType.id;
        });
    } else {
        loadingModal.hide();
    }
}

function checkFields() {
    var valid = true;
    $('.required').each(function () {
        if ($(this).val() === '') {
            $(this).next().html('This field is required');
            valid = false;
        } else {
            $(this).next().html('');
        }
    });
    $('.number').each(function () {
        if ($(this).val() === '' && !$(this).hasClass('required')) {
            $(this).next().html('');
        }
        else if (!/^\d+$/.test($(this).val())) {
            $(this).next().html('This field should be a number');
            valid = false;
        }else {
            $(this).next().html('');
        }
    });
    return valid;
}