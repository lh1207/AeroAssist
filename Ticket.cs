using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AeroAssist
{
    /// <summary>
    /// Represents a ticket in the AeroAssist system.
    /// </summary>
    public class Ticket
    {
        // Parameterless constructor
        public Ticket()
        {
        }

        /// <summary>
        /// Gets or sets the ticket ID.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TicketId { get; set; }

        /// <summary>
        /// Gets or sets the title of the ticket.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the description of the ticket.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the status of the ticket.
        /// </summary>
        public string? Status { get; set; }

        /// <summary>
        /// Gets or sets the priority of the ticket.
        /// </summary>
        public string? Priority { get; set; }

        /// <summary>
        /// Gets or sets the type of the ticket.
        /// </summary>
        public string? Type { get; set; }

        public string? Subtype { get; set; }

        public string? Item { get; set; }

        /// <summary>
        /// Gets or sets the assignee of the ticket.
        /// </summary>
        public string? Assignee { get; set; }

        /// <summary>
        /// Gets or sets the reporter of the ticket.
        /// </summary>
        public string? Reporter { get; set; }

        /// <summary>
        /// Gets or sets the creation date of the ticket.
        /// </summary>
        [Column(TypeName = "DATETIME")]
        public DateTime Created { get; set; }

        [Column(TypeName = "DATETIME")]
        public DateTime Updated { get; set; }

        [Column(TypeName = "DATETIME")]
        public DateTime? Due { get; set; }

        /// <summary>
        /// Gets or sets the resolution of the ticket.
        /// </summary>
        public string? Resolution { get; set; }

        /// <summary>
        /// Gets or sets the comments of the ticket.
        /// </summary>
        public string? Comments { get; set; }

        /// <summary>
        /// Gets or sets the attachments of the ticket.
        /// </summary>
        public string? Attachments { get; set; }

        /// <summary>
        /// Gets or sets the departments associated with the ticket.
        /// </summary>
        public string? Departments { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Ticket"/> class.
        /// </summary>
        /// <param name="ticketId">The ticket ID.</param>
        /// <param name="title">The title of the ticket.</param>
        /// <param name="description">The description of the ticket.</param>
        /// <param name="status">The status of the ticket.</param>
        /// <param name="priority">The priority of the ticket.</param>
        /// <param name="type">The type of the ticket.</param>
        /// <param name="subtype">The subtype of the ticket.</param>
        /// <param name="item">The subtype supplement.</param>
        /// <param name="assignee">The assignee of the ticket.</param>
        /// <param name="reporter">The reporter of the ticket.</param>
        /// <param name="created">The creation date of the ticket.</param>
        /// <param name="updated">The last updated date of the ticket.</param>
        /// <param name="due">The due date of the ticket.</param>
        /// <param name="resolution">The resolution of the ticket.</param>
        /// <param name="comments">The comments of the ticket.</param>
        /// <param name="attachments">The attachments of the ticket.</param>
        /// <param name="departments">The departments associated with the ticket.</param>
        public Ticket(int ticketId, string title, string? description, string? status, string? priority, string? type, string? subtype, string? item,
            string? assignee, string? reporter, DateTime created, DateTime updated, DateTime? due, string? resolution,
            string? comments, string? attachments, string? departments)
        {
            TicketId = ticketId;
            Title = title;
            Description = description;
            Status = status;
            Priority = priority;
            Type = type;
            Subtype = subtype;
            Item = item;
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

        /// <summary>
        /// Creates a new ticket with default values.
        /// </summary>
        public void CreateTicket()
        {
            // TODO: Implementation...
        }

        /// <summary>
        /// Closes the ticket and updates its status and resolution.
        /// </summary>
        public void CloseTicket()
        {
            // TODO: Implementation...
        }
    }
}