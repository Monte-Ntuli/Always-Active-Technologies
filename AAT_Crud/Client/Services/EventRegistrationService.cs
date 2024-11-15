﻿using Client.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using SharedClasses.DTOs;
using System.Net.Http.Json;

namespace Client.Services
{
    public class EventRegistrationService : IEventRegistrationService
    {
        [Inject]
        public ISnackbar Snackbar { get; set; }

        private readonly HttpClient _httpClient;
        public CreateEventRegDTO CreateEventReg { get; set; }
        public IEnumerable<EventRegistrationDTO> UserEventReg { get; set; }
        public EventRegistrationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateEventRegistration(CreateEventRegDTO createEventReg)
        {
            var result = await _httpClient.PostAsJsonAsync("https://localhost:7054/api/EventRegistration/Create", createEventReg);
            if (result.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                var response = await result.Content.ReadFromJsonAsync<CreateEventRegDTO>();
                CreateEventReg = response;
            }
            else if(result.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                Snackbar.Add(result.ToString(), Severity.Error, config => { config.ShowCloseIcon = false; });
                CreateEventReg = await result.Content.ReadFromJsonAsync<CreateEventRegDTO>();
            }
        }

        public async Task<HttpResponseMessage> DeleteEventRegistration(Guid EventId)
        {
            var DeleteEvent = await _httpClient.PostAsJsonAsync<Guid>("https://localhost:7054/api/EventRegistration/Delete/", EventId);
            return DeleteEvent;
        }

        public async Task<IEnumerable<EventRegistrationDTO>> GetAllUserEventRegistrations(string UserId)
        {
            UserEventReg = await _httpClient.GetFromJsonAsync<IEnumerable<EventRegistrationDTO>>("https://localhost:7054/api/EventRegistration/Get-Registered/" + UserId);
            return UserEventReg;
        }
    }
}
