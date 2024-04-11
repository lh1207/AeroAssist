$(document).ready(function() {
    $.ajax({
        url: 'https://localhost:7223/api/Ticket/Ticket',
        type: 'GET',
        success: function (data) {
            console.log(data);

            // Initialize an object to hold the counts for each priority
            let priorityCounts = {
                'Low': 0,
                'Medium': 0,
                'High': 0
            };

            // Iterate over the data and increment the count for each priority
            data.forEach(item => {
                if (priorityCounts.hasOwnProperty(item.priority)) {
                    priorityCounts[item.priority]++;
                }
            });

            // Convert the priorityCounts object into an array in the same order as the labels
            let graphData = ['Low', 'Medium', 'High'].map(priority => priorityCounts[priority]);

            // Calculate the maximum value in your data
            let maxValue = Math.max(...graphData);

            const ctx = document.getElementById('ticketPriorityChart').getContext('2d');
            window.ticketPriorityChart = new Chart(ctx, {
                type: 'bar',
                data: {
                    labels: ['Low', 'Medium', 'High'],
                    datasets: [{
                        label: 'Tickets by Priority',
                        data: graphData,
                        backgroundColor: [
                            'rgba(255, 99, 132, 0.2)',
                            'rgba(54, 162, 235, 0.2)',
                            'rgba(255, 206, 86, 0.2)'
                        ],
                        borderColor: [
                            'rgba(255, 99, 132, 1)',
                            'rgba(54, 162, 235, 1)',
                            'rgba(255, 206, 86, 1)'
                        ],
                        borderWidth: 1
                    }]
                },
                options: {
                    scales: {
                        y: {
                            beginAtZero: true,
                            max: maxValue + 1 // Set the maximum value to be higher than the highest value in the dataset
                        }
                    }
                },
            });
        },
        error: function (err) {
            console.error(err);
        }
    });
});