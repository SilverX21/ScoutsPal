using EventsPal.Services.EventsManagerAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScoutsPal.Services.EventsManagerAPI.Models;
using Serilog;
using System.Text.RegularExpressions;

namespace ScoutsPal.Services.EventsManagerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class EventController : ControllerBase
    {
        private readonly IEventRepository _eventRepository;
        private readonly Serilog.ILogger _serilogLogger;

        public EventController(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
            _serilogLogger = Log.ForContext<EventController>();
        }

        /// <summary>
        /// Gets the events by a given group
        /// </summary>
        /// <param name="groupId">The group that you pretend to check the events</param>
        /// <returns>list of events by group</returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<Event>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetEventsByGroup(int groupId)
        {
            if (groupId <= 0)
            {
                _serilogLogger.Warning($"EventController: The user inputed an invalid groupId");
                return BadRequest(new { statusCode = StatusCodes.Status400BadRequest, message = "There was a problem getting the events you've requested" });
            }

            try
            {
                var response = _eventRepository.GetEventsByGroup(groupId);

                if (response == null || !response.Any())
                {
                    _serilogLogger.Error($"EventController: There wasn't any data found");
                    return BadRequest(new { statusCode = StatusCodes.Status404NotFound, message = "There was a problem getting the data you've requested, please try again." });
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                _serilogLogger.Error(ex, "EventController: There was a problem getting the events data you've requested!");
                return BadRequest(new { statusCode = StatusCodes.Status400BadRequest, message = "There was a problem getting the data you've requested, please try again." });
            }
        }

        /// <summary>
        /// Gets all of the events
        /// </summary>
        /// <returns>list of all of the events</returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<Event>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetAllEvents()
        {
            try
            {
                var response = _eventRepository.GetAllEvents();

                if (response == null || !response.Any())
                {
                    _serilogLogger.Error($"EventController: There wasn't any data found!");
                    return BadRequest(new { statusCode = StatusCodes.Status404NotFound, message = "There was a problem getting the data you've requested, please try again!" });
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                _serilogLogger.Error(ex, "EventController: There was a problem getting the data for the requested event!");
                return BadRequest(new { statusCode = StatusCodes.Status400BadRequest, message = "There was a problem getting the data you've requested, please try again!" });
            }
        }

        /// <summary>
        /// Gets the details of a given event
        /// </summary>
        /// <param name="eventId">event that you wnat to check the info</param>
        /// <returns>event details</returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(Event))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetEventDetails(long eventId)
        {
            if (eventId < 0)
            {
                _serilogLogger.Warning($"EventController: The user inputed an invalid eventId");
                return BadRequest(new { statusCode = StatusCodes.Status400BadRequest, message = "There was a problem getting the event details you've requested" });
            }

            try
            {
                var response = _eventRepository.GetEventDetails(eventId);

                if (response == null)
                {
                    _serilogLogger.Error($"EventController: The requested event wasn't found ");
                    return BadRequest(new { statusCode = StatusCodes.Status404NotFound, message = "There was a problem getting the event you've requested" });
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                _serilogLogger.Error(ex, "EventController: There was a problem getting the data for the requested event!");
                return BadRequest(new { statusCode = StatusCodes.Status400BadRequest, message = "There was a problem getting the event details you've requested" });
            }
        }

        /// <summary>
        /// Creates a certain event
        /// </summary>
        /// <param name="eventDetails">event to create</param>
        /// <returns>satus code with the result</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ProducesResponseType(200, Type = typeof(Event))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult CreateEvent(Event eventDetails)
        {
            if (eventDetails == null)
            {
                _serilogLogger.Warning($"EventController: The user inputed some invalid data!");
                return BadRequest(new { statusCode = StatusCodes.Status400BadRequest, message = "There was a problem creating the event you've requested. Please input valid data!" });
            }

            try
            {
                var response = _eventRepository.CreateEvent(eventDetails);

                if (!response)
                {
                    _serilogLogger.Error($"EventController: The user inputed an invalid data!");
                    return BadRequest(new { statusCode = StatusCodes.Status400BadRequest, message = "There was a problem creating the event. Please try again!" });
                }

                return Ok(new { statusCode = StatusCodes.Status200OK, message = "The Event was created!" });
            }
            catch (Exception ex)
            {
                _serilogLogger.Error(ex, "EventController: There was a problem getting the data for getting the event required!");
                return BadRequest(new { statusCode = StatusCodes.Status400BadRequest, message = "There was a problem creating the event. Please try again" });
            }
        }

        /// <summary>
        /// Updates a given event
        /// </summary>
        /// <param name="eventDetails">event that you want to update</param>
        /// <returns>satus code with the result</returns>
        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult EditEvent(Event eventDetails)
        {
            if (eventDetails == null)
            {
                _serilogLogger.Warning($"EventController: The user inputed an invalid event data!");
                return BadRequest(new { statusCode = StatusCodes.Status400BadRequest, message = "There was a problem editing the event you've requested. Please input valid data!" });
            }

            try
            {
                var response = _eventRepository.EditEvent(eventDetails);

                if (!response)
                {
                    _serilogLogger.Error($"EventController: The user inputed an invalid event data!");
                    return BadRequest(new { statusCode = StatusCodes.Status404NotFound, message = "There was a problem editing the event you've requested. Please input valid data!" });
                }

                return Ok(new { statusCode = StatusCodes.Status200OK, message = "The event info was edited successfully!" });
            }
            catch (Exception ex)
            {
                _serilogLogger.Error(ex, "EventController: There was a problem editing the data of the requested event!");
                return BadRequest(new { statusCode = StatusCodes.Status404NotFound, message = "There was a problem editing the event you've requested. Please input valid data!" });
            }
        }

        /// <summary>
        /// Deletes a given event
        /// </summary>
        /// <param name="eventDetails">event that you want to delete</param>
        /// <returns>satus code with the result</returns>
        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteEvent(Event eventDetails)
        {
            if (eventDetails == null)
            {
                _serilogLogger.Warning($"EventController: The user inputed an invalid event!");
                return BadRequest(new { statusCode = StatusCodes.Status404NotFound, message = "There was a problem deleting the event you've requested. Please input valid data!" });
            }

            try
            {
                var response = _eventRepository.DeleteEvent(eventDetails);

                if (!response)
                {
                    _serilogLogger.Error($"EventController: It wasn't possible to delete the requested event!");
                    return BadRequest(new { statusCode = StatusCodes.Status400BadRequest, message = "There was a problem deleting the requested event. Please try again!" });
                }

                return Ok(new { statusCode = StatusCodes.Status200OK, message = "The event was deleted successfully!" });
            }
            catch (Exception ex)
            {
                _serilogLogger.Error(ex, "EventController: There was a problem deleting the event data!");
                return BadRequest(new { statusCode = StatusCodes.Status400BadRequest, message = "It wasn't possible to delete the requested event. Please try again!" });
            }
        }
    }
}
