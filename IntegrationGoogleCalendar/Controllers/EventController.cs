using IntegrationGoogleCalendar.Credention.Helper;
using IntegrationGoogleCalendar.Models;
using Microsoft.AspNetCore.Mvc;

namespace IntegrationGoogleCalendar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        [HttpPost("Create")]
        public async Task<IActionResult> CreateEventGoogleCalendar([FromBody] GoogleCalendar request)
        {
            return Ok(await EventsHelper.CreateGoogleCalendar(request));
        }

        [HttpPost("CreateQuick")]
        public async Task<IActionResult> CreateQuickEventGooleCalendar([FromBody] GoogleQuickCalendar request)
        {
            var eventCreated = await EventsHelper.CreateQuickEventGoogleCalendar(request.Description);

            return Ok(eventCreated);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetEventsGooleCalendar()
        {
            return Ok(await EventsHelper.GetEventsGoogleCalendar());
        }

        [HttpGet("Get/{eventId}")]
        public async Task<IActionResult> GetEventGooleCalendar(string eventId)
        {
            return Ok(await EventsHelper.GetEventGoogleCalendar(eventId));
        }

        [HttpDelete("Delete/{eventId}")]
        public async Task<IActionResult> DeleteEventGoogleCalendar(string eventId)
        {
            return Ok(await EventsHelper.DeleteEventGoogleCalendar(eventId));
        }

        [HttpPost("Update/{eventId}")]
        public async Task<IActionResult> UpdateEventGoogleCalendar(string eventId)
        {
            return Ok(await EventsHelper.UpdateEventGoogleCalendar(eventId));
        }
    }
}
