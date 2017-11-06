/**
 * ConfirmModal class that shows and hides a confirm modal for suers to confirm an action
 * 
 * @author - Brandon Pfeiffer <bpfeiffer17@jcu.edu>
 */
class ConfirmModal {
    constructor() {
        this.confirmed = false;
        this.confirmMessage = '';
        this.confirmCallBack = null;
        this.denyCallBack = null;
    }

    /**
     * Show a confirm modal
     */
    show(confirmMessage, confirmCallBack, denyCallBack) {
        this.confirmed = false;
        this.confirmMessage = confirmMessage;
        this.confirmCallBack = confirmCallBack;
        this.denyCallBack = denyCallBack;
        if (!this.modalExists()) {
            this.addModal();
        }
        $('#confirmMessage').html(this.confirmMessage);
        $('#confirmModal').modal('show');
    }

    /**
     * Execute the proper callback and then close the modal
     */
    hide() {
        if (this.modalExists()) {
            // Check to see if the user confirmed the message and execute the correct callback
            if (this.confirmed) {
                if (this.confirmCallBack) {
                    this.confirmCallBack();
                }
            }else {
                if (this.denyCallBack) {
                    this.denyCallBack();
                }
            }
            $('#confirmModal').modal('hide');
        }
    }

    /**
     * Check to see if this modal exists by querying for a confirmModal element
     */
    modalExists() {
        if ($('#confirmModal').length) {
            return true;
        }
        return false;
    }

    /**
     * Add a modal to the pages body
     */
    addModal() {
        $('body').append(`
            <div id="confirmModal" class="modal fade">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Confirm</h5>
                        </div>
                        <div class="modal-body">
                            <p><span id="confirmMessage"></span></p>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-primary" onclick="confirmModal.confirmed = true; confirmModal.hide()">Confirm</button>
                            <button type="button" class="btn btn-secondary" onclick="confirmModal.hide()">Deny</button>
                        </div>
                    </div>
                </div>
            </div>
        `);
    }
}

// Create a new ConfirmModal object to be used where this script is included
confirmModal = new ConfirmModal();