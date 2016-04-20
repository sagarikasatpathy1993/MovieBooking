$(document).ready(function () {
    $("#MovieNameList").prop("disabled", true);
    $("#DateList").prop("disabled", true);
    $("#CityNameList").change(function ()
    {
        if ($("#CityNameList").val() != "Select Your City")
        {
            var options = {};
            options.url = "/Login/MovieDropDown";
            options.type = "POST";
            options.data = JSON.stringify({ CityNameList: $("#CityNameList").val() });
            options.dataType = "json";
            options.contentType = "application/json";
            options.success = function (movies) {

                $("#MovieNameList").empty();
                console.log(movies.MovieNameList.length);
                for (var i = 0; i < movies.MovieNameList.length; i++) {
                    $("#MovieNameList").append("<option>" + movies.MovieNameList[i].MovieName + "</option>");


                }
                $("#MovieNameList").prop("disabled", false);
               

                };
                options.error = function () { alert("Error retrieving Movies!"); };
                $.ajax(options);
            }
        
        else {
            $("#MovieNameList").empty();
            $("#MovieNameList").prop("disabled", true);
        }
    });
});
