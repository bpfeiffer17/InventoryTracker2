

var dropDownHelper;
var assetType;

$(document).ready(function(){
/*
*Retreive asset from db and then add its properties to the page
*/
    $.get('/Scripts/DummyData/DropDowns.json', function (data,status){
        //create a dropdownhelper object with the returned json data
        dropDownHelper = new dropDownHelper(data);
        $.get('/Scripts/DummyData/AssetTypes/' + assetTypeId + '.json', function (data,status){
            assetType = new assetType(data);
            setPage();
        });
    });
    setListners();
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
   if (prop.type == 'Drop Down') {
       addDropDown(prop.id, prop.addDropDownId)
   }
}

