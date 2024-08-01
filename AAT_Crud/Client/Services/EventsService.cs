﻿using Client.Services.Interfaces;
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
        public IEnumerable<EventsDTO> Events { get; set; } = new List<EventsDTO>();
        public IEnumerable<EventsDTO> UserEvents { get; set; } = new List<EventsDTO>();
        public EventsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateEvent(CreateEventDTO createEvent)
        {
            var result = await _httpClient.PostAsJsonAsync("Event/Create", createEvent);
            if (result.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                var response = await result.Content.ReadFromJsonAsync<CreateEventDTO>();
                createEvents = response;
            }
            else
            {
                Snackbar.Add(result.ToString(), Severity.Error, config => { config.ShowCloseIcon = false; });
            }
        }
        public async Task<IEnumerable<EventsDTO>> GetAllEvents()
        {
            Events = await _httpClient.GetFromJsonAsync<IEnumerable<EventsDTO>>("Event/Get-All-Events");
            return Events;
        }

        public async Task<IEnumerable<EventsDTO>> GetAllUserEvents(Guid UserId)
        {
            UserEvents = await _httpClient.GetFromJsonAsync<IEnumerable<EventsDTO>>("Event/Get-All-Events-By-UserId" + UserId);
            return UserEvents;
        }

        public async Task<HttpResponseMessage> DeleteEvent(Guid EventId)
        {
            var DeleteEvent = await _httpClient.PostAsJsonAsync<Guid>("Event/Delete/" , EventId);
            return DeleteEvent;
        }

        public async Task Update(UpdateEventDTO updateEvent)
        {
            var result = await _httpClient.PostAsJsonAsync("Event/Update", updateEvent);
            if(result.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                var response = await result.Content.ReadFromJsonAsync<EventsDTO>();
                Snackbar.Add("Event updated successfully", Severity.Success, config => { config.ShowCloseIcon = false; });
                Event = response;
            }
            else
            {
                Snackbar.Add(result.ToString(), Severity.Error, config => { config.ShowCloseIcon = false; });
            }
        }
    }
}
