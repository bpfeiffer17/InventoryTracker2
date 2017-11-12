/**
 * @author Brandon Pfeiffer <bpfeiffer17@jcu.edu>
 */

/**
 * $(document).ready waits for the all html content to be loaded
 * and then executes the code inside the callback function.
 */
$(document).ready(function () {
    $('#assetTypesTable').DataTable();
});

function deleteAssetType(assetTypeId) {
    confirmModal.show('Are you sure you want to delete this asset type?', () => {
        loadingModal.show();
        $.post('/AssetType/DeleteAssetType/' + assetTypeId, function (data, status) {
            loadingModal.hide();
            window.location = '/AssetType/Browse';
        });
    });
}