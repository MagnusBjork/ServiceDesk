using System;
using System.Collections.Generic;
using System.Linq;
using ServiceDesk.WebApp.Domain;

namespace ServiceDesk.WebApp.Services
{

    public enum EnumOptionSets
    {
        TicketCategory = 100,
        TicketServerity = 101,
        TicketStatus = 102
    }

    public class OptionSet
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public List<OptionSetValue> Values { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }
    }


    public class OptionSetValue
    {
        public string Value { get; set; }

        public string Text { get; set; }

    }

    public class OptionSetService
    {

        private readonly IList<OptionSet> _optionSets = new List<OptionSet>() {
            new OptionSet() {
                Id = EnumOptionSets.TicketCategory.ToString(),
                Name = "Ticket Categories",
                Values = new List<OptionSetValue>() {
                    new OptionSetValue() { Value = "1000", Text = "Order"},
                    new OptionSetValue() { Value = "1001", Text = "Bug"},
                    new OptionSetValue() { Value = "1002", Text = "User Admin"},
                    new OptionSetValue() { Value = "1003", Text = "Requirement"}},
                CreatedOn = DateTime.Now,
                ModifiedOn= DateTime.Now
            },
            { new OptionSet() {
                Id = EnumOptionSets.TicketCategory.ToString(),
                Name = "Ticket Serverity",
                Values = new List<OptionSetValue>() {
                    new OptionSetValue() { Value = "1100", Text = "Low"},
                    new OptionSetValue() { Value = "1101", Text = "Medium"},
                    new OptionSetValue() { Value = "1102", Text = "High"}},
                CreatedOn = DateTime.Now,
                ModifiedOn= DateTime.Now
            }}
          };

        public OptionSetService() { }

        public OptionSet GetOptionSet(EnumOptionSets optionSet)
        {
            return _optionSets.Where(o => o.Id.Equals(optionSet)).SingleOrDefault();
        }



    }
}

