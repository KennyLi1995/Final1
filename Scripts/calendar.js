var events = [];
$(".events").each(function () {
    var id = $(".id", this).text().trim();
    var title = $(".title", this).text().trim();
    var start = $(".start", this).text().trim();
    var event = {
        "id": id,
        "title": title,
        "start": start
    };
    events.push(event);
});


$("#calendar").fullCalendar({
    locale: 'au',
    events: events,
    displayEventTime: false,
    dayClick: function (date, allDay, jsEvent, view) {
        var d = new Date(date);
        var m = moment(d).format("YYYY-MM-DD");
        m = encodeURIComponent(m);
        var uri = "/Enrolments/Create?Id=1" + "&date=" + m;
        $(location).attr('href', uri);
    },

    eventClick: function (calEvent, jsEvent, view) {
        var uri = "/Enrolments/Delete/" + calEvent.id;
        $(location).attr('href', uri);     
    } 
});




