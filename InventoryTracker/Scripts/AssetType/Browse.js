/**
 * $(document).ready waits for the all html content to be loaded
 * and then executes the code inside the callback function.
 */
$(document).ready(function () {
    /**
     * Retrieve assets from db and add them to the asset types table
     */
    //$.get('/Scripts/DummyData/AssetTypes.json', function (data, status) {
    //    //For every asset in the returned data
    //    for (var assetType of data) {
    //        //Create a row
    //        var row = $('<tr></tr>');
    //        //Create a name and description column
    //        var nameCol = $('<td><a href="/AssetType/View/' + assetType.assetTypeId + '">' + assetType.name + '</a></td>');
    //        var descCol = $('<td>' + assetType.description + '</td>');
    //        var editCol = $('<a href="/AssetType/Edit/' + assetType.assetTypeId + '">Edit</a>');
    //        //Append those columns to the row
    //        row.append(nameCol);
    //        row.append(descCol);
    //        row.append(editCol);
    //        //Append the row to the tables body
    //        $('#assetTypesTable tbody').append(row);
    //    }
    //    $('#assetTypesTable').DataTable();
    //});
    $('#assetTypesTable').DataTable();
});