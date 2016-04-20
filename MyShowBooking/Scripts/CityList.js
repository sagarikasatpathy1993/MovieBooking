$(document).ready(function () {
    $("#CityNameList").prop("disabled", true);
    $("#MovieNameList").prop("disabled", true);
    $("#DateList").prop("disabled", true);
    $("#StateNameList").change(function () {
        if ($("#StateNameList").val() != "Select Your State") {
            var options = {};
            options.url = "/Login/CityDropDown";
            options.type = "POST";
            options.data = JSON.stringify({ StateNameList: $("#StateNameList").val() });
            options.dataType = "json";
            options.contentType = "application/json";
            options.success = function (movies) {

                $("#CityNameList").empty();
               
                for (var i = 0; i < movies.CityNameList.length; i++) {
                    $("#CityNameList").append("<option>" + movies.CityNameList[i].CityName + "</option>");


                }
                $("#CityNameList").prop("disabled", false);


            };
            options.error = function () { alert("Error retrieving Cities!"); };
            $.ajax(options);
        }

        else {
            $("#CityNameList").empty();
            $("#CityNameList").prop("disabled", true);
        }
    });
});
