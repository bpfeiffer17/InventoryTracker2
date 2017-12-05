/**
 * @author Emily Hansen <@ehansen18@jcu.dy>
 */
//The following code will enable dropdownlists to work on the edit page of the dropdown controller.


var dropDown;

$(document).ready(function () {
   
};


function addValue(value) {
    if ()
};

function setPage() {
    $('#valuesDiv').html('');
    $('#dropDownName').val(dropDown.name);
    $('#valuesDiv').append(`
        <div class="row">
            <div class="col-sm-3">
                <h4> Value: </h4>
            </div>
            <div class= "col-sm-3">
                <h4> Delete:
            </div>
        `);
}


function save() {
    LoadingModal.show();
    var data = {
        Dropdown: JSON.stringify()// need to insert stringify
    }
    $.post('/DropDown/SaveDropDown', data, function (data, status) {
        LoadingModal.hide();
        window.location = '/Dropdown/Browse';
    });
}

function deleteValue(valueID) {
    ConfirmModal.show('Are you sure you want to delete this Value from the DropDown?', () => {
        dropDown.findValue(valueID).active = false;
        setPage();
    });
}

function dropDown(){
    var values = ['test1','test2','test3','test4','test5','test6'​];

    ​$.each(values,function(i,val){
        $('<option />').text(val).val(val).appendTo('#drop');
    });​
}