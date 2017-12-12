/**
 * @author Brandon Pfeiffer <bpfeiffer17@jcu.edu>
 */

/**
 * $(document).ready waits for the all html content to be loaded
 * and then executes the code inside the callback function.
 */
$(document).ready(function () {
    $('#dropDownsTable').DataTable();
});

function deleteDropDown(dropDownID) {
    confirmModal.show('WARNING! All properties that use this drop down will be changed to String properties and be disassociated with this drop down.  Values of properties on asset will be unaffected. Are you sure you want to delete this drop down?', () => {
        loadingModal.show();
        $.post('/DropDown/DeleteDropDown/' + dropDownID, function (data, status) {
            loadingModal.hide();
            window.location = '/DropDown/Browse';
        });
    });
}