using System;
using System.ComponentModel.DataAnnotations;

namespace ServiceDesk.WebApp.Pages.Tickets
{
    public class TicketViewModel
    {
        public int TicketId { get; set; }
        [Required]
        [Display(Name = "Subject")]
        public string Subject { get; set; }
        public string Category { get; set; }
        public string Severity { get; set; }
        public string From { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}