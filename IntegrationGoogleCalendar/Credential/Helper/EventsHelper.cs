using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using IntegrationGoogleCalendar.Models;
using System.Globalization;

namespace IntegrationGoogleCalendar.Credention.Helper
{
    public class EventsHelper
    {
        const string CALENDAR_ID = "primary";

        protected EventsHelper()
        {
        }

        private static async Task<CalendarService> ConnectGoogleAgenda(string[] scopes)
        {
            string applicationName = "Teste-Integracao-Google";
            UserCredential credential;
            using (var stream = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "Credential", "credential.json"), FileMode.Open, FileAccess.Read))
            {
                string credPath = "token.json";
                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.FromStream(stream).Secrets,
                        scopes,
                        "user",
                        CancellationToken.None,
                        new FileDataStore(credPath, true)
                );
            }

            //define services
            var services = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = applicationName
            });

            return services;
        }

        public static async Task<Event> CreateQuickEventGoogleCalendar(string description)
        {
            string[] scopes = { $"https://www.googleapis.com/calendar/v3/calendars/{CALENDAR_ID}/events/quickAdd" };
            var services = await ConnectGoogleAgenda(scopes);

            var requestCreate = services.Events.QuickAdd(CALENDAR_ID, description).Execute();

            return requestCreate;
        }

        public static async Task<Event> CreateGoogleCalendar(GoogleCalendar request)
        {
            string[] scopes = { "https://www.googleapis.com/auth/calendar " };
            var services = await ConnectGoogleAgenda(scopes);

            //define request
            Event eventCalendar = new Event()
            {
                Summary = request.Summary,
                Location = request.Location,
                Start = new EventDateTime
                {
                    DateTime = request.Start,
                    TimeZone = "(UTC-03:00) Brasília"
                },
                End = new EventDateTime
                {
                    DateTime = request.End,
                    TimeZone = "(UTC-03:00) Brasília"
                },
                Description = request.Description,
            };

            var eventRequest = services.Events.Insert(eventCalendar, CALENDAR_ID);
            var requestCreate = await eventRequest.ExecuteAsync();

            return requestCreate;
        }

        public static async Task<IList<Event>> GetEventsGoogleCalendar()
        {
            string[] scopes = { $"https://www.googleapis.com/calendar/v3/calendars/{CALENDAR_ID}/events" };
            var services = await ConnectGoogleAgenda(scopes);

            var events = services.Events.List(CALENDAR_ID).Execute();

            return events.Items;
        }

        public static async Task<Event> GetEventGoogleCalendar(string eventId)
        {
            string[] scopes = { $"https://www.googleapis.com/calendar/v3/calendars/{CALENDAR_ID}/events" };
            var services = await ConnectGoogleAgenda(scopes);

            var events = await services.Events.Get(CALENDAR_ID, eventId).ExecuteAsync();

            return events;
        }

        public static async Task<string> DeleteEventGoogleCalendar(string eventId)
        {
            string[] scopes = { $"https://www.googleapis.com/calendar/v3/calendars/{CALENDAR_ID}/events/{eventId}" };
            var services = await ConnectGoogleAgenda(scopes);

            var events = await services.Events.Delete(CALENDAR_ID, eventId).ExecuteAsync();

            return events;
        }

        public static async Task<Event> UpdateEventGoogleCalendar(string eventId)
        {
            string[] scopes = { $"https://www.googleapis.com/calendar/v3/calendars/{CALENDAR_ID}/events/{eventId}" };
            var services = await ConnectGoogleAgenda(scopes);

            Event eventCalendar = new Event()
            {
                Summary = "Atualizando",
                Location = "São Paulo",
                Start = new EventDateTime
                {
                    DateTime = DateTime.Parse("2023-03-25T00:49:18.227Z"),
                    TimeZone = "(UTC-03:00) Brasília"
                },
                End = new EventDateTime
                {
                    DateTime = DateTime.Parse("2023-03-25T00:49:18.227Z"),
                    TimeZone = "(UTC-03:00) Brasília"
                },
                Description = "Descrição do evento atualizado",

            };

            var events = await services.Events.Update(eventCalendar, CALENDAR_ID, eventId).ExecuteAsync();

            return events;
        }
    }
}
