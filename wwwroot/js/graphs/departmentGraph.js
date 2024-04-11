$(document).ready(function() {
    $.ajax({
        url: 'https://localhost:7223/api/Ticket/Ticket',
        type: 'GET',
        success: function (data) {
            console.log(data);

            // Initialize an object to hold the counts for each department
            let departmentCounts = {
                "IT Support": 0,
                "Network Operations": 0,
                "Systems Administration": 0,
                "Software Development": 0,
                "Quality Assurance": 0,
                "Project Management": 0,
                "Database Administration": 0,
                "Security Operations": 0,
                "Help Desk": 0,
                "Infrastructure Management": 0,
                "User Experience (UX)": 0,
                "Business Analysis": 0,
                "Training and Development": 0,
                "Vendor Management": 0,
                "Finance and Billing": 0,
                "Procurement": 0,
                "Human Resources": 0,
                "Legal and Compliance": 0,
                "Marketing and Communications": 0,
                "Sales and Customer Support": 0
            };

            // Iterate over the data and increment the count for each department
            data.forEach(item => {
                let department = item.departments;
                if (departmentCounts.hasOwnProperty(department)) {
                    departmentCounts[department]++;
                }
            });

            // Convert the departmentCounts object into arrays for labels and data
            let labels = Object.keys(departmentCounts);
            let graphData = Object.values(departmentCounts);

            const ctx = document.getElementById('ticketDepartmentChart').getContext('2d');
            window.ticketDepartmentChart = new Chart(ctx, {
                type: 'doughnut',
                data: {
                    labels: labels,
                    datasets: [{
                        label: 'Tickets by Department',
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
                    responsive: false,
                    maintainAspectRatio: false
                },
            });
        },
        error: function (err) {
            console.error(err);
        }
    });
});