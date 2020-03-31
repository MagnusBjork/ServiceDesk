using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ServiceDesk.WebApp.Pages.Tickets
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IEnumerable<TicketViewModel> Tickets { get; private set; }


        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            Tickets = new List<TicketViewModel>() {
                new TicketViewModel() {
                    TicketId = 1,
                    Subject = "Request for new user",
                    Category = "Order",
                    Severity = "Low",
                    From = "kalle.kula@test.com",
                    CreatedOn = DateTime.Parse ("2020-03-25 12:01")
                },
                new TicketViewModel() {
                    TicketId = 2,
                    Subject = "Bug on web",
                    Category = "Bug",
                    Severity = "Medium",
                    From = "nisse.nilsson@test.com",
                    CreatedOn = DateTime.Parse ("2020-03-05 07:14")
                },
                new TicketViewModel() {
                    TicketId = 3,
                    Subject = "Need for additional telephone field",
                    Category = "Requirement",
                    Severity = "Low",
                    From = "nisse.nilsson@test.com",
                    CreatedOn = DateTime.Parse ("2020-03-25 15:11")
                }};
        }
    }
}
