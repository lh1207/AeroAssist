$(document).ready(function() {
    $.ajax({
        url: 'https://localhost:7223/api/Ticket/Ticket',
        type: 'GET',
        success: function(data) {
            console.log(data);
            // Initialize an object to hold the counts for each status
            let statusCounts = {
                'Open': 0,
                'In-Progress': 0,
                'Resolved': 0,
                'Closed': 0,
                'Canceled': 0
            };

            // Iterate over the data and increment the count for each status
            data.forEach(item => {
                if (statusCounts.hasOwnProperty(item.status)) {
                    statusCounts[item.status]++;
                }
            });

            // Convert the statusCounts object into an array in the same order as the labels
            let graphData = ['Open', 'In-Progress', 'Resolved', 'Closed', 'Canceled'].map(status => statusCounts[status]);

            // Calculate the maximum value in your data
            let maxValue = Math.max(...graphData);

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
                            beginAtZero: true,
                            max: maxValue + 10 // Set the maximum value to be 10 units higher than the highest value in the dataset
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