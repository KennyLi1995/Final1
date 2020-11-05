var events = [];
$(".events").each(function () {
    var id = $(".id", this).text().trim();
    var title = $(".title", this).text().trim();
    var start = $(".start", this).text().trim();
    var courseId = $(".courseId", this).text().trim();
    var event = {
        "id": id,
        "title": title,
        "start": start,
        "courseId": courseId
    };
    events.push(event);
});


$("#calendar").fullCalendar({
    locale: 'au',
    events: events,
    displayEventTime: false,
    eventClick: function (calEvent, jsEvent, view) {  
        var d = calEvent.start;
        var courseId = calEvent.courseId;
        var m = moment(d).format("YYYY-MM-DD");
        var uri = "/Enrolments/Create?Id=" + courseId + "&date=" + m;
        $(location).attr('href', uri);  
    } 
});


