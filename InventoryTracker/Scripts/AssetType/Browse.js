/**
 * $(document).ready waits for the all html content to be loaded
 * and then executes the code inside the callback function.
 */
$(document).ready(function () {
    //What do we want to do when the page is loaded?
    //We want to gather asset types from the db
    $.get('/Scripts/DummyData/AssetTypes.json', function (data, status) {
        for (var assetType of data) {
            var row = $('<tr></tr>');
            var nameCol = $('<td>' + assetType.name + '</td>');
            var descCol = $('<td>' + assetType.description + '</td>');
            row.append(nameCol);
            row.append(descCol);
            $('#assetTypesTable tbody').append(row);
        }
        $('#assetTypesTable').DataTable();
    });
    //Display those assets types in a table
    //Give the user options for those asset types
});