/**
 * AlertModal class that shows and hides an alert modal with a custom message
 * 
 * @author - Brandon Pfeiffer <bpfeiffer17@jcu.edu>
 */
class AlertModal {
    constructor() {
        this.alertMessage = '';
    }

    /**
     * Show an alert modal
     */
    show(alertMessage) {
        if (!this.modalExists()) {
            this.addModal();
        }
        $('#alertMessage').html(alertMessage);
        $('#alertModal').modal('show');
    }

    /**
     * Hide the alert modal
     */
    hide() {
        if (this.modalExists()) {
            $('#alertModal').modal('hide');
        }
    }

    /**
     * Check to see if the alert modal exists by querying the alertModal id
     */
    modalExists() {
        if ($('#alertModal').length) {
            return true;
        }
        return false;
    }

    /**
     * Add a modal to the body of the page
     */
    addModal() {
        $('body').append(`
            <div id="alertModal" class="modal fade">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 id="alertMessage" class="modal-title"></h5>
                        </div>
                    </div>
                </div>
            </div>
        `);
    }
}

// Create a LoadingModal object to be used where this script is included
alertModal = new AlertModal();