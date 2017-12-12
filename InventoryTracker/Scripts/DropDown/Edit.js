/**
 * Javascript file for front end functionality of the Edit view of the DropDown controller
 * 
 * @author - Brandon Pfeiffer <bpfeiffer17@jcu.edu>
 */

var dropDown;

//Code to be executed when the page is done loading.
$(document).ready(function () {
    loadingModal.show();
    $.get(`/DropDown/JSON/${dropDownID || 0}`, function (data) {
        dropDown = new DropDown(JSON.parse(data));
        setPage();
        setListeners();
        loadingModal.hide();
    });
});

/**
 * Set JQuery listeners
 */
function setListeners() {
    /**
     * When a user clicks #addValueButton, add a Value with a 
     * propertyId of 'new' plus the current timestamp for uniqueness
     */
    $('#addValueButton').click(function () {
        dropDown.values.push('');
        setPage();
    });
}

/**
 * Clear out the #valuesDiv and for each Value on the DropDown, add html to edit the Value to #valuesDiv
 */
function setPage() {
    $('#valuesDiv').html('');
    $('#dropDownName').val(dropDown.name);
    $('#valuesDiv').append(`
        <div class="row">
            <div class="col-sm-4">
                <h4>Value: </h4>
            </div>
            <div class="col-sm-4"></div>
            <div class="col-sm-4"></div>
        </div>
    `);
    // Add inputs with the value of each of the DropDown's values
    for (var i = 0; i < dropDown.values.length; i++) {
        var value = dropDown.values[i];
        $('#valuesDiv').append(`
            <div class="row">
                <div class="col-sm-4">
                    <input class="form-control" value="${value}" onblur="setValue(${i}, this.value)">
                </div>
                <div class="col-sm-4">
                    <button class="btn" onclick="deleteValue(${i})">DELETE</button>
                </div>
                <div class="col-sm-4"></div>
            </div>
        `);
    }
}

function setValue(valueIndex, value) {
    dropDown.values[valueIndex] = value;
    var valueCount = 0;
    for (let val of dropDown.values) {
        if (val.toLowerCase() === value.toLowerCase()) {
            valueCount++;
        }
    }
    if (valueCount > 1) {
        dropDown.values[valueIndex] = '';
        alertModal.show(`You have already created the value "${value}"`);
        setPage();
    }
}

function deleteValue(valueIndex) {
    confirmModal.show('Are you sure you want to delete this value?', () => {
        dropDown.values.splice(valueIndex, 1);
        setPage();
    });
}

function save() {
    loadingModal.show();
    var data = {
        postDropDown: dropDown.getSaveStructure()
    }
    $.post('/DropDown/SaveDropDown', data, function (data, status) {
        loadingModal.hide();
        window.location = '/DropDown/Browse';
    });
}