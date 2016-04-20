$(document).ready(function () {
    $("#DateList").prop("disabled", true);
    $("#MovieNameList").change(function () {
        if ($("#MovieNameList").val() != "Select Your Movie")
        {
            var options = {};
            options.url = "/Login/DateDropDown";
            options.type = "POST";
            options.data = JSON.stringify({ MovieNameList: $("#MovieNameList").val() });
            options.dataType = "json";
            options.contentType = "application/json";
            options.success = function (movies) {

                $("#DateList").empty();
                console.log(movies.DateList.length);
                for (var i = 0; i < movies.DateList.length; i++) {
                   
                  
                    $("#DateList").append("<option>" + ToJavaScriptDate(movies.DateList[i].Date)+ "</option>");
                  
                    
                }

                $("#DateList").prop("disabled", false);

            };
            options.error = function () { alert("Error retrieving Dates!"); };
            $.ajax(options);
        }
        else {
            $("#DateList").empty();
            $("#DateList").prop("disabled", true);
        }
    });
    function ToJavaScriptDate(value)
    {
        var pattern = /Date\(([^)]+)\)/;
        var results = pattern.exec(value);
        var date = new Date(parseInt(results[1]));
        return (date.getMonth() + 1) + "/" + date.getDate() + "/" + date.getFullYear();
    }
});
