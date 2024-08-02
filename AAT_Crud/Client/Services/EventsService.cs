using Client.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using SharedClasses.DTOs;
using System.Net.Http.Json;

namespace Client.Services
{
    public class EventsService : IEventsService
    {
        [Inject]
        public ISnackbar Snackbar { get; set; }

        private readonly HttpClient _httpClient;
        public CreateEventDTO createEvents {  get; set; }
        public EventsDTO Event {  get; set; }
        public List<EventsDTO> Events { get; set; } = new List<EventsDTO>();
        public List<EventsDTO> UserEvents { get; set; } = new List<EventsDTO>();
        public EventsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateEvent(CreateEventDTO createEvent)
        {
            var result = await _httpClient.PostAsJsonAsync("https://localhost:7054/api/Event/Create", createEvent);
            if (result.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                var response = await result.Content.ReadFromJsonAsync<CreateEventDTO>();
                createEvents = response;
            }
            else
            {
                Snackbar.Add(result.ToString(), Severity.Error, config => { config.ShowCloseIcon = false; });
                createEvents = await result.Content.ReadFromJsonAsync<CreateEventDTO>();
            }
        }
        public async Task<List<EventsDTO>> GetAllEvents()
        {
            Events = await _httpClient.GetFromJsonAsync<List<EventsDTO>>("https://localhost:7054/api/Event/Get-All-Events");
            return Events;
        }

        public async Task<List<EventsDTO>> GetAllUserEvents(string UserId)
        {
            UserEvents = await _httpClient.GetFromJsonAsync<List<EventsDTO>>("https://localhost:7054/api/Event/Get-All-Events-By-UserId/" + UserId);
            return UserEvents;
        }

        public async Task<EventsDTO> DeleteEvent(Guid EventId)
        {
           var deletedEvent = await _httpClient.DeleteFromJsonAsync<EventsDTO>("https://localhost:7054/api/Event/Delete/" + EventId);
            return deletedEvent;
        }

        public async Task Update(UpdateEventDTO updateEvent)
        {
            var result = await _httpClient.PostAsJsonAsync("https://localhost:7054/api/Event/Update", updateEvent);
            if(result.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                var response = await result.Content.ReadFromJsonAsync<EventsDTO>();
                Snackbar.Add("Event updated successfully", Severity.Success, config => { config.ShowCloseIcon = false; });
                Event = response;
            }
            else
            {
                Snackbar.Add(result.ToString(), Severity.Error, config => { config.ShowCloseIcon = false; });
                Event = await result.Content.ReadFromJsonAsync<EventsDTO>();
            }
        }

        public async Task<EventsDTO> GetEventById(int EventId)
        {
            Event = await _httpClient.GetFromJsonAsync<EventsDTO>("https://localhost:7054/api/Event/Get-Event-By-Id/" + EventId);
            return Event;
        }

        public async Task<EventsDTO> GetEventByGuid(Guid EventId)
        {
            Event = await _httpClient.GetFromJsonAsync<EventsDTO>("https://localhost:7054/api/Event/Get-Event-By-Guid/" + EventId);
            return Event;
        }
    }
}
