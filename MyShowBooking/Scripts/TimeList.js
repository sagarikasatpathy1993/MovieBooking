$(document).ready(function () {
    $("#TimeList").prop("disabled", true);
    
    $("#TheaterList").change(function () {
        if ($("#TheaterList").val() != "Select Your Theater") {
            var options = {};
            options.url = "/Login/TimeDropDown";
            options.type = "POST";
            options.data = JSON.stringify({ TheaterList: $("#TheaterList").val() });
            options.dataType = "json";
            options.contentType = "application/json";
            options.success = function (movies) {

                $("#TimeList").empty();
                console.log(movies.TimeList.length);
                for (var i = 0; i < movies.TimeList.length; i++) {
                    $("#TimeList").append("<option>" + ToJavaScriptDate(movies.TimeList[i].StartTime) + "</option>");


                }
                $("#TimeList").prop("disabled", false);


            };
            options.error = function () { alert("Error retrieving Time!"); };
            $.ajax(options);
        }

        else {
            $("#TimeList").empty();
            $("#TimeList").prop("disabled", true);
        }
    });
    function ToJavaScriptDate(value) {
        var pattern = /Date\(([^)]+)\)/;
        var results = pattern.exec(value);
        var date = new Date(parseInt(results[1]));
        return (date.getMonth() + 1) + "/" + date.getDate() + "/" + date.getFullYear()+" "+date.getHours()+":"+date.getMinutes();
    }
    
   

});
