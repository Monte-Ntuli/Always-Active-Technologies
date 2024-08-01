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

        public UserDTO userInfo { get; set; }
        public CreateEventRegDTO BookEvent {  get; set; } = new CreateEventRegDTO();
        public IEnumerable<EventsDTO> Events { get; set; } = new List<EventsDTO>();
        public string email;
        protected override async Task OnInitializedAsync()
        {
            email = await localStorage.GetItemAsync<string>("UserName");
            Events = await EventsService.GetAllEvents();
            if(!string.IsNullOrEmpty(email))
            {
                userInfo = await AccountService.GetUserByEmail(email);
            }
        }

        protected override Task OnParametersSetAsync()
        {
            return base.OnParametersSetAsync();
        }

        public async Task BookTicket(Guid EventId)
        {
            BookEvent.EventId = EventId;
            BookEvent.UserId = email;

            if(email == null) 
            {
                Snackbar.Add("Please Signin to book tickets", Severity.Info, config => { config.ShowCloseIcon = false; });
            }
            else
            {
                await EventRegistrationService.CreateEventRegistration(BookEvent);
            }
        }

        public async Task ViewEvent(Guid EventId)
        {

        }
    }
}
