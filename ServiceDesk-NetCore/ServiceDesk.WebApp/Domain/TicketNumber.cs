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


        public string GenerateNewTicketNumber()
        {
            Counter++;

            return Counter.ToString();
        }
    }
}