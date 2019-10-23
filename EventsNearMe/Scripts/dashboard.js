window.onload = function () {

    var ctx = document.getElementById('myChart').getContext('2d');

    var myChart = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: ['Red', 'Blue', 'Yellow', 'Green', 'Purple', 'Orange'],
            datasets: [{
                backgroundColor: [
                    'rgba(255, 99, 132, 0.2)',
                    'rgba(54, 162, 235, 0.2)',
                    'rgba(255, 206, 86, 0.2)',
                    'rgba(75, 192, 192, 0.2)',
                    'rgba(153, 102, 255, 0.2)',
                    'rgba(255, 159, 64, 0.2)'
                ],
                borderColor: [
                    'rgba(255, 99, 132, 1)',
                    'rgba(54, 162, 235, 1)',
                    'rgba(255, 206, 86, 1)',
                    'rgba(75, 192, 192, 1)',
                    'rgba(153, 102, 255, 1)',
                    'rgba(255, 159, 64, 1)'
                ],
                borderWidth: 1
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            scales: {
                yAxes: [{
                    ticks: {
                        beginAtZero: true
                    }
                }]
            }
        }
    });


    $.ajax({
        url: '/Innovation/chartData',
        type: 'GET',
        contentType: 'application/json;utf-8',
        datatype: 'json',

    }).done(function (result) {
        if (result.data.length > 0) {
            console.log(result);
            updateChart(result);
        } else {
            console.log("fail");
        }

    });

    function updateChart(result) {
        myChart.data.labels = result.Labels;
        myChart.data.datasets[0].label = "# of Bookings";
        myChart.data.datasets[0].data = result.data;
        myChart.update();
    }
}


