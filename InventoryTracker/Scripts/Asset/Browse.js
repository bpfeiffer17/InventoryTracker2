

$(document).ready(function (){

    $.get('/Scripts/DummyData/Assets.json', function (data, status){

    for(var assets of data){
        //create a row
        var row = $('<tr></tr>');
        //colmuns
        var assetTypeCol = $('<td>' + assets.assetType+ '</td>');
        var nameCol = $('<td> <a href="/Asset/View/' + assets.assetTypeId + '">' + assets.name + '</a></td>');
        var descCol = $('<td>' + assets.description + '</td>');
        var editCol = $('<a href="/Asset/Edit/' + assets.assetId + '">Edit</a>');

        //Append the columns to the row
        row.append(assetTypeCol);
        row.append(nameCol);
        row.append(descCol);
        row.append(editCol);
        //Append the row the the tables body
          $('#assetsTable tbody').append(row);

         }
         $('#assetsTable').DataTable();
    });

});