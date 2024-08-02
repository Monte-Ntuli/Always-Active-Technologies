using Blazored.LocalStorage;
using Client.Services;
using Client.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.JSInterop;
using MudBlazor;
using SharedClasses.DTOs;

namespace Client.Pages.Events
{
    public class EventsBase : ComponentBase
    {
        [Inject]
        public IEventsService EventsService { get; set; }

        [Inject]
        public IAccountService AccountService { get; set; }

        [Inject]
        public IEventRegistrationService EventRegistrationService { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Inject]
        public NavigationManager NavMan { get; set; }

        [Inject]
        public ISnackbar Snackbar { get; set; }

        [Inject]
        public ILocalStorageService localStorage { get; set; }

        [Parameter]
        public Guid EventId { get; set; }
        public UserDTO userInfo { get; set; }
        public CreateEventRegDTO BookEvent {  get; set; } = new CreateEventRegDTO();
        public List<EventsDTO> Events { get; set; } = new List<EventsDTO>();
        public EventsDTO Event { get; set; } = new EventsDTO();
        public UpdateEventDTO updateEvent { get; set; } = new UpdateEventDTO();
        public IEnumerable<EventRegistrationDTO> EventRegistrations { get; set; } = new List<EventRegistrationDTO>();
        
        public string email;
        public CreateEventDTO CreateEventDTO { get; set; } = new CreateEventDTO();

        public DateTime? _date = DateTime.UtcNow;
        protected override async Task OnInitializedAsync()
        {
            email = await localStorage.GetItemAsync<string>("UserName");
            Events = await EventsService.GetAllEvents();
            if(!string.IsNullOrEmpty(email))
            {
                userInfo = await AccountService.GetUserByEmail(email);
            }
        }

        public async Task BookTicket(Guid EventId)
        {
            BookEvent.EventId = EventId;
            BookEvent.UserId = email;

            if(email == null) 
            {
                Snackbar.Add("Please Sign in to book tickets", Severity.Info, config => { config.ShowCloseIcon = false; });
            }
            else
            {
                await EventRegistrationService.CreateEventRegistration(BookEvent);
                Snackbar.Add("Successful bookings will appear in my booked events");
            }
        }

        public async Task ViewEvent(Guid EventId)
        {
            EventId.ToString();
            NavMan.NavigateTo($"ViewEvent/{EventId}");
        }

        public async Task DeleteEvent(Guid EventId)
        {
            await EventsService.DeleteEvent(EventId);
            NavMan.Refresh();
        }

        public async Task UpdateEvent(Guid EventId)
        {
            updateEvent.Id = EventId;
            if (string.IsNullOrEmpty(updateEvent.Description))
            {
                Snackbar.Add("Event Description can not be empty", Severity.Warning, config => { config.ShowCloseIcon = false; });
            }
            if (string.IsNullOrEmpty(updateEvent.Name))
            {
                Snackbar.Add("Event name can not be empty", Severity.Warning, config => { config.ShowCloseIcon = false; });
            }
            if (updateEvent.Seats <= 0)
            {
                Snackbar.Add("Event can not have 0 seats", Severity.Warning, config => { config.ShowCloseIcon = false; });
            }
            else
            {
                await EventsService.Update(updateEvent);
            }
        }

        public async Task CalculateAvailableSeats()
        {
            
        }
    }
}
