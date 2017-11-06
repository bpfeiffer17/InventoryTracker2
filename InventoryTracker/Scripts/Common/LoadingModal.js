/**
 * LoadingModal class that shows and hides a laoding modal
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

    hide() {
        if (this.modalExists()) {
            $('#loadingModal').modal('hide');
        }
    }

    modalExists() {
        if ($('#loadingModal').length) {
            return true;
        }
        return false;
    }

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

loadingModal = new LoadingModal();