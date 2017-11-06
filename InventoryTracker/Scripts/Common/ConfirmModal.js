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
     * Show a loading modal
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

    hide() {
        if (this.modalExists()) {
            // Check to see if the user confirmed the message
            if (this.confirmed) {
                this.confirmCallBack();
            }else {
                this.denyCallBack();
            }
            $('#confirmModal').modal('hide');
        }
    }

    modalExists() {
        if ($('#confirmModal').length) {
            return true;
        }
        return false;
    }

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

confirmModal = new ConfirmModal();