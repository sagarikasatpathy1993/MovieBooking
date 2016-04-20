$(document).ready(function () {
    $("#SeatList").prop("disabled", true);

    $("#TimeList").change(function () {
        if ($("#TimeList").val() != "Select Time") {
            var options = {};
            options.url = "/Login/SeatDropDown";
            options.type = "POST";
            options.data = JSON.stringify({ TimeList: $("#TimeList").val() });
            options.dataType = "json";
            options.contentType = "application/json";
            options.success = function (movies) {

                $("#SeatList").empty();
                console.log(movies.SeatList.length);
                for (var i = 0; i < movies.SeatList.length; i++) {
                    $("#SeatList").append("<option>" + (movies.SeatList[i].SeatName) + "</option>");


                }
                $("#SeatList").prop("disabled", false);


            };
            options.error = function () { alert("Error retrieving Seat!"); };
            $.ajax(options);
        }

        else {
            $("#SeatList").empty();
            $("#SeatList").prop("disabled", true);
        }
    });
    


});
