namespace AeroAssist
{
    public class Ticket
    {
        // Properties
        public int TicketId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
        public string? Priority { get; set; }
        public string? Type { get; set; }
        public string? Assignee { get; set; }
        public string? Reporter { get; set; }
        public string? Created { get; set; }
        public string? Updated { get; set; }
        public string? Due { get; set; }
        public string? Resolution { get; set; }
        public string? Comments { get; set; }
        public string? Attachments { get; set; }
        public string? Departments { get; set; }

        // Constructor
        public Ticket(
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
            // Initialize properties
            TicketId = ticketId;
            Title = title;
            Description = description;
            Status = status;
            Priority = priority;
            Type = type;
            Assignee = assignee;
            Reporter = reporter;
            Created = created;
            Updated = updated;
            Due = due;
            Resolution = resolution;
            Comments = comments;
            Attachments = attachments;
            Departments = departments;
        }

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
