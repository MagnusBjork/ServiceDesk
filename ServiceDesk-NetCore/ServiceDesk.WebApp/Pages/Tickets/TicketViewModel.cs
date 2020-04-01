using System;
using System.ComponentModel.DataAnnotations;
using ServiceDesk.WebApp.Domain;

namespace ServiceDesk.WebApp.Pages.Tickets
{
    public class TicketViewModel
    {
        public Guid Id { get; set; }
        public string TicketNumber { get; set; }
        [Required]
        [Display(Name = "Subject")]
        public string Subject { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Severity { get; set; }
        public string From { get; set; }
        public DateTime CreatedOn { get; set; }

        public TicketViewModel() { }

        public TicketViewModel(Ticket ticket)
        {
            Id = ticket.Id;
            TicketNumber = ticket.TicketNumber;
            Subject = ticket.Subject;
            Description = ticket.Description;
            Category = ticket.Category;
            Severity = ticket.Subject;
            From = ticket.From;
            CreatedOn = ticket.CreatedOn;

        }

    }


}