using System;
using System.Collections.Generic;
using System.Linq;

namespace ServiceDesk.WebApp.Services
{
    public enum EnumOptionSet
    {
        TicketCategory = 100,
        TicketServerity = 101,
        TicketStatus = 102
    }

    public class OptionSet
    {
        public EnumOptionSet Id { get; set; }
        public string Name { get; set; }
        public IList<OptionSetValue> Values { get; set; }
    }

    public class OptionSetValue
    {
        public string Key { get; set; }
        public string Text { get; set; }
        public bool Enabled { get; set; }
    }

    public interface IOptionSetService
    {
        IList<OptionSetValue> GetOptionSetValues(EnumOptionSet optionSet);
        string GetOptionSetValueText(EnumOptionSet optionSet, string key);
    }

    public class OptionSetService : IOptionSetService
    {
        // OptionSet values are hard coded for now. Later on an admin view will be developed to let admin 
        // create and edit optionsets in the app.

        private readonly IList<OptionSet> _optionSets = new List<OptionSet>() {
            new OptionSet() {
                Id = EnumOptionSet.TicketCategory,
                Name = "Ticket Categories",
                Values = new List<OptionSetValue>() {
                    new OptionSetValue() { Key = "1000", Text = "Order", Enabled = true},
                    new OptionSetValue() { Key = "1001", Text = "Bug", Enabled = true},
                    new OptionSetValue() { Key = "1002", Text = "User Admin", Enabled = true},
                    new OptionSetValue() { Key = "1003", Text = "Requirement", Enabled = true}}

            },
            { new OptionSet() {
                Id = EnumOptionSet.TicketServerity,
                Name = "Ticket Serverity",
                Values = new List<OptionSetValue>() {
                    new OptionSetValue() { Key = "1100", Text = "Low", Enabled = true},
                    new OptionSetValue() { Key = "1101", Text = "Medium", Enabled = true},
                    new OptionSetValue() { Key = "1102", Text = "High", Enabled = true}}

            }}
          };

        public IList<OptionSetValue> GetOptionSetValues(EnumOptionSet optionSet)
        {
            return _optionSets.Where(o => o.Id.Equals(optionSet)).SingleOrDefault().Values;
        }

        public string GetOptionSetValueText(EnumOptionSet optionSet, string key)
        {
            return GetOptionSetValues(optionSet).Where(o => o.Key.Equals(key)).Select(o => o.Text).SingleOrDefault();
        }
    }
}