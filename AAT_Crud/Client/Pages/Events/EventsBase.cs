using Client.Services.Interfaces;
using Microsoft.AspNetCore.Components;
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
        public IJSRuntime JSRuntime { get; set; }

        [Inject]
        public NavigationManager NavMan { get; set; }

        [Inject]
        public ISnackbar Snackbar { get; set; }

        public IEnumerable<EventsDTO> Events { get; set; } = new List<EventsDTO>();
        protected override async Task OnInitializedAsync()
        {
            Events = await EventsService.GetAllEvents();
        }

        protected override Task OnParametersSetAsync()
        {
            return base.OnParametersSetAsync();
        }
    }
}
