$(document).ready(function () {
    /**
     * Retrieve asset from db and then add its properties to the page
     */
    $.get('/Scripts/DummyData/AssetTypes/' + assetTypeId + '.json', function (data, status) {
        var assetType = data;
        $('#assetTypeName').val(assetType.name);
        $('#assetTypeDescription').val(assetType.description);
        for (var prop of assetType.properties) {
            addProp(prop);
        }
    });

    $('#addPropButton').click(function () {
        addProp({
            propertyId: 'new',
            name: '',
            type: String,
            unit: '',
            dropDownId: ''
        });
    });
});

function addProp(prop) {
    $('#propertiesDiv').append(`
        <div>
            <div style="display:inline-block">
                <label>Property Type: </label>
                <select value="${prop.type}" class="form-control">
                    <option>String</option>
                    <option>Number</option>
                    <option>Drop Down</option>
                </select>
            </div>
            <div style="display:inline-block">
                <label>Property Name: </label>
                <input value="${prop.name}" class="form-control" />
            </div>
            <div style="display:inline-block">
                <label>Property Unit: </label>
                <input value="${prop.unit ? prop.unit:'' }" class="form-control" />
            </div>
        </div>
    `);
}