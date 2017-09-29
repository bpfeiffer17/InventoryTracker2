/**
 * Javascript file for front end functionality of the View view of the AssetType controller
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
        dropDownHelper = new DropDownHelper(data);
        //Retrieve an AssetType from the database with the given assetTypeId
        $.get('/Scripts/DummyData/AssetTypes/' + assetTypeId + '.json', function (data, status) {
            //Create an AssetType object with the returned JSON data
            assetType = new AssetType(data);
            //Add the properties of the new AssetType to the view
            addProperties();
        });
    });
});

/**
 * For each Property on the AssetType, add html to view to Property to #propertiesDiv
 */
function addProperties() {
    $('#assetTypeName').html(assetType.name);
    $('#assetTypeDescription').html(assetType.description);
    for (var prop of assetType.properties) {
        $('#propertiesDiv').append(`
            <div>
                <label>Name:</label><label>${prop.name}</label>
                <label>Type:</label><label>${prop.type}</label>
            </div>
        `);
    }
}