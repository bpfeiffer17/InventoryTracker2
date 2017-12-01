/**
 * @author Emily Hansen <@ehansen18@jcu.dy>
 */
//The following code will enable dropdownlists to work on the create page of the dropdown controller.
function dropDown(){
    var values = ['test1','test2','test3','test4','test5','test6'​];

    ​$.each(values,function(i,val){
        $('<option />').text(val).val(val).appendTo('#drop');
    });​
}