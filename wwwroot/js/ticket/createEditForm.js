$(document).ready(function() {
    $('.select2').select2();

    var priority = {
        "Low": "bg-success",
        "Medium": "bg-warning",
        "High": "bg-danger"
    };

    var items = {
        "Network": ["Connectivity Outage", "Performance", "VPN", "WIFI", "Firewall", "Router", "Switch"],
        "Hardware": ["Hardware Failure", "Peripheral Issues", "Operating System", "Application Software", "Slow Performance", "Freezing/Crashing"],
        "Server": ["Hardware Failure", "Storage", "Operating System", "Application Software", "High CPU/Memory Usage", "Server Slowdown"],
        "Printer/Scanner": ["Printing Issues", "Scanning Issues", "Network Connectivity", "USB Connectivity", "Print Quality", "Scanned Document Quality"],
        "Software": ["Installation Issues", "Activation/Registration", "Bugs/Error Messages", "Feature Requests", "Compatibility Issues", "Integration Issues"]
    };

    var departments = [
        "IT Support",
        "Network Operations",
        "Systems Administration",
        "Software Development",
        "Quality Assurance",
        "Project Management",
        "Database Administration",
        "Security Operations",
        "Help Desk",
        "Infrastructure Management",
        "User Experience (UX)",
        "Business Analysis",
        "Training and Development",
        "Vendor Management",
        "Finance and Billing",
        "Procurement",
        "Human Resources",
        "Legal and Compliance",
        "Marketing and Communications",
        "Sales and Customer Support"
    ];

    // Populate the departments dropdown
    $.each(departments, function(index, value) {
        $('#departments').append(new Option(value, value));
    });

    $('#subtype').change(function() {
        var subtype = $(this).val();
        var options = items[subtype];

        $('#item').empty();
        $.each(options, function(index, value) {
            $('#item').append(new Option(value, value));
        });
    }).change(); // Trigger the change event manually

    // Populate the priority dropdown
    $.each(priority, function(key, value) {
        $('#priority').append(new Option(key, key));
    });

    // Apply Select2 to the priority dropdown
    $('#priority').select2();
});