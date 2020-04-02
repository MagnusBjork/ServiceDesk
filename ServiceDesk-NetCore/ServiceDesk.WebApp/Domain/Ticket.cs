using System;
using System.Text.Json.Serialization;

namespace ServiceDesk.WebApp.Domain
{
    public class Ticket : IDomainEntity
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("ticketNumber")]
        public string TicketNumber { get; set; }

        [JsonPropertyName("subject")]
        public string Subject { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("category")]
        public string Category { get; set; }

        [JsonPropertyName("severity")]
        public string Severity { get; set; }

        [JsonPropertyName("from")]
        public string From { get; set; }

        [JsonPropertyName("createdOn")]
        public DateTime CreatedOn { get; set; }

        [JsonPropertyName("modifiedOn")]
        public DateTime ModifiedOn { get; set; }


        // public Ticket() { }
        // public Ticket(string subject, string description, string category, string severity, string from)
        // {
        //     Subject = subject;
        //     Description = description;
        //     Category = category;
        //     Severity = severity;
        //     From = from;
        // }



        // public void Create(string ticketNumber)
        // {
        //     Id = Guid.NewGuid();
        //     TicketNumber = ticketNumber;
        //     CreatedOn = DateTime.Now;
        //     ModifiedOn = DateTime.Now;
        // }
        // public void Update()
        // {
        //     ModifiedOn = DateTime.Now;
        // }


    }
}