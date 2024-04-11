$(document).ready(function() {
    $.ajax({
        url: 'https://localhost:7223/api/Ticket/Ticket',
        type: 'GET',
        success: function(data) {
            console.log(data);
            let graphData = data;

            const ctx = document.getElementById('ticketCountChart').getContext('2d');
            window.ticketCountChart = new Chart(ctx, {
                type: 'bar',
                data: {
                    labels: ['Open', 'In-Progress', 'Resolved', 'Closed', 'Canceled'],
                    datasets: [{
                        label: 'Ticket Count',
                        data: graphData,
                        backgroundColor: [
                            'rgba(255, 99, 132, 0.2)',
                            'rgba(54, 162, 235, 0.2)',
                            'rgba(255, 206, 86, 0.2)',
                            'rgba(75, 192, 192, 0.2)',
                            'rgba(153, 102, 255, 0.2)'
                        ],
                        borderColor: [
                            'rgba(255, 99, 132, 1)',
                            'rgba(54, 162, 235, 1)',
                            'rgba(255, 206, 86, 1)',
                            'rgba(75, 192, 192, 1)',
                            'rgba(153, 102, 255, 1)'
                        ],
                        borderWidth: 1
                    }]
                },
                options: {
                    scales: {
                        y: {
                            beginAtZero: true
                        }
                    }
                }
            });
        },
        error: function(jqXHR, textStatus, errorThrown) {
            console.log('Error loading ticket count data:', textStatus, errorThrown);
        }
    });
});