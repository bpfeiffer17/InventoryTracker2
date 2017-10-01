

var dropDownHelper;
var assetType;

$(document).ready(function(){
/*
*Retreive asset from db and then add its properties to the page
*/


    $.get('/Scripts/DummyData/Assets/' + assetId + '.json', function (data,status) {
        var assets = data;
        $('#assetType').val(assets.assetType);
        $('#assetName').val(assets.name);
        $('#assetDescription').val(assets.description);
        for (var prop of assets.properties){
            addProp(prop);
        }
    });

    $('addPropButton').click(function (){
        addProp({
            propertyId:'new',
            name:'',
            type: 'String',
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
                <select class="form-control" onChange()>
                    <option> ${prop.type === 'String' ? 'selected':''}String</option>
                    <option> ${prop.type === 'Number' ? 'selected':''}Number</option>
                    <option> ${prop.type === 'Drop Down' ? 'selected':''}Drop Down</option>
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