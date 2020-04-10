using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using ServiceDesk.WebApp.Domain;
using ServiceDesk.WebApp.Services;
using ServiceDesk.WebApp.Services.DomainRepositories;

namespace ServiceDesk.WebApp.Pages.Tickets
{
    public class EditModel : PageModel
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IUserRepository _userRepository;
        private readonly IOptionSetService _optionSetService;
        private readonly ILogger<EditModel> _logger;

        public EditModel(ITicketRepository ticketRepository, IUserRepository userRepository, IOptionSetService optionSetService, ILogger<EditModel> logger)
        {
            _ticketRepository = ticketRepository;
            _userRepository = userRepository;
            _optionSetService = optionSetService;
            _logger = logger;
        }

        [TempData]
        public string Message { get; set; }

        [BindProperty]
        public TicketViewModel Ticket { get; set; }

        public List<SelectListItem> CategoryList { get; set; }

        public List<SelectListItem> SeverityList { get; set; }

        public List<SelectListItem> UserList { get; set; }

        public async Task OnGetAsync(Guid id)
        {
            CategoryList = _optionSetService.GetOptionSetValues(EnumOptionSet.TicketCategory).ToSelectedItems();
            SeverityList = _optionSetService.GetOptionSetValues(EnumOptionSet.TicketServerity).ToSelectedItems();
            UserList = _userRepository.GetAllUsers().Select(u => new SelectListItem() { Text = u.Name, Value = u.Id.ToString() }).ToList();

            if (id.Equals(Guid.Empty))
                Ticket = new TicketViewModel();
            else
            {
                var ticketEntity = await _ticketRepository.GetTicketAsync(id);
                Ticket = new TicketViewModel(ticketEntity);
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            // Map fields that can be created/updated from UI
            var ticketEntity = new Ticket();
            ticketEntity.Subject = Ticket.Subject;
            ticketEntity.Description = Ticket.Description;
            ticketEntity.Category = Ticket.Category;
            ticketEntity.CategoryText = _optionSetService.GetOptionSetValueText(EnumOptionSet.TicketCategory, Ticket.Category);
            ticketEntity.Severity = Ticket.Severity;
            ticketEntity.SeverityText = _optionSetService.GetOptionSetValueText(EnumOptionSet.TicketServerity, Ticket.Severity);
            ticketEntity.AssignedTo = Ticket.AssignedTo;
            ticketEntity.From = Ticket.From;

            if (Ticket.Id.Equals(Guid.Empty))
            {
                Ticket.Id = await _ticketRepository.CreateTicketAsync(ticketEntity);
                Message = "Ticket created successfully!";
            }
            else
            {
                await _ticketRepository.UpdateTicketAsync(Ticket.Id, ticketEntity);
                Message = "Ticket updated successfully!";
            }

            return RedirectToPage("/tickets/index");
        }
    }
}
