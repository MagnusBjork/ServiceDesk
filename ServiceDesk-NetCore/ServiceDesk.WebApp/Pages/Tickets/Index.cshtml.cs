using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using ServiceDesk.WebApp.Repositories;

namespace ServiceDesk.WebApp.Pages.Tickets
{
    public class IndexModel : PageModel
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly ILogger<EditModel> _logger;

        public IEnumerable<TicketViewModel> Tickets { get; private set; }


        public IndexModel(ITicketRepository ticketRepository, ILogger<EditModel> logger)
        {
            _ticketRepository = ticketRepository;
            _logger = logger;
        }

        public async Task OnGetAsync()
        {
            var tickets = await _ticketRepository.GetAllAsync();

            Tickets = tickets.Select(t => new TicketViewModel(t));

            // Tickets = new List<TicketViewModel>() {
            //     new TicketViewModel() {
            //         Id = Guid.Empty,
            //         TicketNumber = "1001",
            //         Subject = "Request for new user",
            //         Category = "Order",
            //         Severity = "Low",
            //         From = "kalle.kula@test.com",
            //         CreatedOn = DateTime.Parse ("2020-03-25 12:01")
            //     },
            //     new TicketViewModel() {
            //         Id = Guid.Empty,
            //         TicketNumber = "1002",
            //         Subject = "Bug on web",
            //         Category = "Bug",
            //         Severity = "Medium",
            //         From = "nisse.nilsson@test.com",
            //         CreatedOn = DateTime.Parse ("2020-03-05 07:14")
            //     },
            //     new TicketViewModel() {
            //         Id = Guid.Empty,
            //         TicketNumber = "1003",
            //         Subject = "Need for additional telephone field",
            //         Category = "Requirement",
            //         Severity = "Low",
            //         From = "nisse.nilsson@test.com",
            //         CreatedOn = DateTime.Parse ("2020-03-25 15:11")
            //     }};
        }
    }
}
