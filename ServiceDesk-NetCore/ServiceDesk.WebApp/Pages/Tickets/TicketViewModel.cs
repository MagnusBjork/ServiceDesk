using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceDesk.WebApp.Domain;

namespace ServiceDesk.WebApp.Pages.Tickets
{
    public class TicketViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Ticket No")]
        public string TicketNumber { get; set; }

        [Required]
        [Display(Name = "Subject")]
        public string Subject { get; set; }

        [Display(Name = "Description")]
        [MaxLength(300)]
        public string Description { get; set; }

        [Display(Name = "Category")]
        public string Category { get; set; }

        [Display(Name = "Category")]
        public string CategoryText { get; set; }

        [Display(Name = "Severity")]
        public string Severity { get; set; }

        [Display(Name = "Severity")]
        public string SeverityText { get; set; }

        [Display(Name = "From")]
        public string From { get; set; }

        [Display(Name = "Assigned To")]
        public string AssignedTo { get; set; }

        [Display(Name = "Assigned To")]
        public string AssignedToText { get; set; }

        [Display(Name = "Registration Date")]
        public DateTime CreatedOn { get; set; }

        [Display(Name = "Last Updated")]
        public DateTime ModifiedOn { get; set; }

        public TicketViewModel() { }

        public TicketViewModel(Ticket ticket)
        {
            Id = ticket.Id;
            TicketNumber = ticket.TicketNumber;
            Subject = ticket.Subject;
            Description = ticket.Description;
            Category = ticket.Category;
            CategoryText = ticket.CategoryText;
            Severity = ticket.Severity;
            SeverityText = ticket.SeverityText;
            From = ticket.From;
            AssignedTo = ticket.AssignedTo;
            CreatedOn = ticket.CreatedOn;
            ModifiedOn = ticket.ModifiedOn;
        }
    }
}