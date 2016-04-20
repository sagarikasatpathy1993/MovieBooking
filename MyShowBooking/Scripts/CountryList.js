$(document).ready(function () {
    //Dropdownlist Selectedchange event
    $("#CityNameList").change(function () {
        $("#MovieNameList").empty();
        $.ajax({
            type: 'POST',
            url: '@Url.Action("GetStates")', // we are calling json method
 
            dataType: 'json',
 
            data: { id: $("#CityNameList").val() }, 
            
            success: function (states) {
                // states contains the JSON formatted list
                // of states passed from the controller
 
                $.each(states, function (i, state) {
                    $("#MovieNameList").append('<option value="' + state.Value + '">' +  
                         state.Text + '</option>');                                                                                                
                    // here we are adding option for States
 
                });
            },
        error: function (ex) {
            alert('Failed to retrieve states.' + ex);
        }
    });
    return false;
})
});