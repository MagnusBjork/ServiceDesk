using System;
using System.Text.Json.Serialization;

namespace ServiceDesk.WebApp.Domain
{
    public class TicketNumber : IDomainEntity
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("counter")]
        public int Counter { get; set; }

        [JsonPropertyName("createdOn")]
        public DateTime CreatedOn { get; set; }

        [JsonPropertyName("modifiedOn")]
        public DateTime ModifiedOn { get; set; }


        public TicketNumber CreateNewTicketNumberCounter()
        {
            const int initialCounterValue = 1;

            Id = Guid.NewGuid();
            Counter = initialCounterValue;
            CreatedOn = DateTime.Now;
            ModifiedOn = CreatedOn;

            return this;
        }

        public TicketNumber CreateNextTicketNumber()
        {
            // Step up ticket counter
            Counter++;
            ModifiedOn = DateTime.Now;

            return this;
        }

        // Ticket number can be formated as needed.
        public string GetTicketNumberString() => Counter.ToString();

    }
}