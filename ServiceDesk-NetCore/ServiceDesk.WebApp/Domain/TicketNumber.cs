using System;
using System.Text.Json.Serialization;

namespace ServiceDesk.WebApp.Domain
{
    public class TicketNumber : IDomainEntity
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("counter")]
        public int Counter { get; private set; }

        [JsonPropertyName("createdOn")]
        public DateTime CreatedOn { get; set; }

        [JsonPropertyName("modifiedOn")]
        public DateTime ModifiedOn { get; set; }


        private const int _firstNumber = 1;

        public TicketNumber()
        {
            // Create a new ticket number counter.
            Id = Guid.NewGuid();
            Counter = _firstNumber;
            CreatedOn = DateTime.Now;
            ModifiedOn = CreatedOn;
        }

        public TicketNumber(TicketNumber current)
        {
            // Load currnet ticket number counter.
            Id = current.Id;
            Counter = current.Counter;
            CreatedOn = current.CreatedOn;
            ModifiedOn = current.ModifiedOn;
        }

        public void GenerateNewTicketNumber()
        {
            // Step up counter
            Counter++;
            ModifiedOn = DateTime.Now;
        }

        // Ticket number can be formated as needed.
        public string GetTicketNumber() => Counter.ToString();

    }
}