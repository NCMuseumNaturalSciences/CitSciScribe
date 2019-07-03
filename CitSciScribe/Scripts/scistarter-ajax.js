$("#transcription-form").submit(function(event) {
    var key = "",
        var;
    baseurl = "https://scistarter.com/api/record_event?key="; // the script where you handle the form input.
    var posturl = baseurl + key;
    var profile_id = $("#profile_id").val();
    var cdt = converToLocalTime;
    $.ajax({
        type: "POST",
        url: posturl,
        data: {
            profile_id: profile_id,
            project_id: "18106",
            type: "classification",
            when: cdt,
        },
        success: function(data) {
            console.log("Post Submission was successful.");
            console.log(data);
        },
        error: function(textStatus, errorThrown) {
            console.log("A POST error occurred.");
            console.log(textStatus);
            console.log(errorThrown);
        },
    });
});

function converToLocalTime(serverDate) {

    var dt = new Date(Date.parse(serverDate));
    var localDate = dt;

    var gmt = localDate;
    var min = gmt.getTime() / 1000 / 60; // convert gmt date to minutes
    var localNow = new Date().getTimezoneOffset(); // get the timezone
    // offset in minutes
    var localTime = min - localNow; // get the local time

    var dateStr = new Date(localTime * 1000 * 60);
    dateStr = dateStr.toString("yyyy-MM-dd'T'HH:mm:ss");
    return dateStr;
}