var chart = Highcharts.chart('container', {
    data: {
        table: 'datatable'
    },
    chart: {
        type: 'column'
    },
    title: {
        text: 'The course and rate chart'
    },
    yAxis: {
        allowDecimals: false,
        title: {
            text: 'rate',
            rotation: 0
        }
    },
    tooltip: {
        formatter: function () {
            return '<b>' + this.point.name + '</b><br/>' +
                this.point.y;
        }
    }
});

