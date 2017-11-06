/**
 * LoadingModal class that shows and hides a loading modal
 * 
 * @author - Brandon Pfeiffer <bpfeiffer17@jcu.edu>
 */
class LoadingModal {
    /**
     * Show a loading modal
     */
    show() {
        if (!this.modalExists()) {
            this.addModal();
        }
        $('#loadingModal').modal('show');
    }

    /**
     * Hide the loading modal
     */
    hide() {
        if (this.modalExists()) {
            $('#loadingModal').modal('hide');
        }
    }

    /**
     * Check to see if the loading modal exists by querying the loadingModal id
     */
    modalExists() {
        if ($('#loadingModal').length) {
            return true;
        }
        return false;
    }

    /**
     * Add a modal to the body of the page
     */
    addModal() {
        $('body').append(`
            <div id="loadingModal" class="modal fade">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Loading...</h5>
                        </div>
                    </div>
                </div>
            </div>
        `);
    }
}

// Create a LoadingModal object to be used where this script is included
loadingModal = new LoadingModal();