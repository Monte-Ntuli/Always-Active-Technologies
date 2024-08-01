using Client.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Client.Services
{
    public class EventsService : IEventsService
    {
        [Inject]
        public ISnackbar Snackbar { get; set; }

        private readonly HttpClient _httpClient;
    }
}
