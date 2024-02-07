using System.Reflection;

namespace AeroAssist
{
    // Primary constructor, which accepts parameters for each property
    public class Ticket(
        int ticketId,
        string title,
        string? description,
        string? status,
        string? priority,
        string? type,
        string? assignee,
        string? reporter,
        string? created,
        string? updated,
        string? due,
        string? resolution,
        string? comments,
        string? attachments,
        string? departments)
    {

        // Getters and Setters for Constructor
        int TicketId { get; set; } = ticketId;
        string Title { get; set; } = title;
        public string? Description { get; set; } = description;
        public string? Status { get; set; } = status;
        public string? Priority { get; set; } = priority;
        public string? Type { get; set; } = type;
        public string? Assignee { get; set; } = assignee;
        public string? Reporter { get; set; } = reporter;
        public string? Created { get; set; } = created;
        public string? Updated { get; set; } = updated;
        public string? Due { get; set; } = due;
        public string? Resolution { get; set; } = resolution;
        public string? Comments { get; set; } = comments;
        public string? Attachments { get; set; } = attachments;
        public string? Departments { get; set; } = departments;

        // Ticket Functions
        public void CreateTicket()
        {
            TicketId = 0; // TODO: Generate a unique ticket ID through a database query
            /*
             * TODO: Generate unique variable names through function parameters
             */
            Title = "New Ticket";
            Description = "New Ticket Description";
            Status = "Open";
            Priority = "Low";
            Type = "Task";
            Assignee = "Unassigned";
            Reporter = "Unassigned";
            Created = "Now";
            Updated = "Now";
            Due = "None";
            Resolution = "None";
            Comments = "None";
            Attachments = "None";
            Departments = "None";
        }

        public void CloseTicket()
        {
            Status = "Closed";
            Resolution = "Resolved";
        }

    }
}
