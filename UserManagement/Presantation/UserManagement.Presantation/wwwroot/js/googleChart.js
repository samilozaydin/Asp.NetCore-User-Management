$(document).ready(function () {
    $.ajax({
        type: "GET",
        dataType: "json",
        contentType: "application/json",
        url: 'CreateGraph',
        success: function (result) {
            google.charts.load('current', { 'packages': ['line'] });
            google.charts.setOnLoadCallback(function () {
                drawChart(result);
            });
        }
    });
});

function drawChart(result) {

    var data = new google.visualization.DataTable();
    data.addColumn('string', 'Departments Name');
    data.addColumn('number', 'Employees on the same department');
    var dataArray = [];

    $.each(result, function (i, obj) {
        dataArray.push([obj.departmentName, obj.departmentId]);
        console.log(i);
        console.log(obj.departmentName);
    });

    data.addRows(dataArray);

    var options = {

        width: 750,
        height: 220,
        axes: {
            x: {
                0: { side: 'top' }
            }
        }
    };

    var chart = new google.charts.Line(document.getElementById('line_top_x'));

    chart.draw(data, google.charts.Line.convertOptions(options));
}