/**
 * @author Brandon Pfeiffer <bpfeiffer17@jcu.edu>
 */

/**
 * $(document).ready waits for the all html content to be loaded
 * and then executes the code inside the callback function.
 */
$(document).ready(function (){
    $('#assetsTable').DataTable();
});

function deleteAsset(assetId) {
    confirmModal.show('Are you sure you want to delete this asset?', () => {
        loadingModal.show();
        $.post('/Asset/DeleteAsset/' + assetId, function (data, status) {
            loadingModal.hide();
            window.location = '/Asset/Browse';
        });
    });
}

function createNewAsset() {
    var assetTypeID = $('#assetType').val();
    if (assetTypeID) {
        window.location = `/Asset/Edit?id=0&assetTypeID=${assetTypeID}`;
    }
}