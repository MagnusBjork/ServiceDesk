using System;

namespace ServiceDesk.WebApp.Domain
{
    public class Ticket : IDomainEntity
    {
        public Guid Id { get; set; }
        public string TicketNumber { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Severity { get; set; }
        public string From { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}