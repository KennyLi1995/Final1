var schools = [];
$(".school").each(function () {
    var name = $('.name', this).text().trim();
    var address = $('.address', this).text().trim();
    var longitude = $('.longitude', this).text().trim();
    var latitude = $('.latitude', this).text().trim();
    var point = {
        "longitude": longitude,
        "latitude": latitude,
        "name": name,
        "address": address
    };
    schools.push(point);
});


// Initial Map
mapboxgl.accessToken = 'pk.eyJ1Ijoiem9lbGkxMTE4IiwiYSI6ImNrZ3VuaDNiajAwc2wycm4ycm1kYjNobXgifQ.RPDCH5DxBkKhaQ4ihIMlsg';
var map = new mapboxgl.Map({
    container: 'map',
    style: 'mapbox://styles/mapbox/streets-v11',
    center: [schools[0].longitude, schools[0].latitude],
    zoom: 10
});


map.addControl(
    new MapboxGeocoder({
        accessToken: mapboxgl.accessToken,
        mapboxgl: mapboxgl
    })
);



for (i = 0; i < schools.length; i++) {

    new mapboxgl.Marker()
        .setLngLat([schools[i].longitude, schools[i].latitude])
        .setPopup(new mapboxgl.Popup({ offset: 25 }) // add popups
            .setHTML('<h5>' + schools[i].name + '</h5><p>' + schools[i].address + '</p>'))
        .addTo(map);
};





















