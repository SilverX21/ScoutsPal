using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScoutsPAl.Services.ScoutsManagerAPI.Models;
using ScoutsPAl.Services.ScoutsManagerAPI.Services.Interfaces;
using Serilog;

namespace ScoutsPAl.Services.ScoutsManagerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ScoutTypeController : ControllerBase
    {
        private readonly IScoutTypeRepository _scoutTypeRepository;
        private readonly Serilog.ILogger _serilogLogger;

        public ScoutTypeController(IScoutTypeRepository scoutTypeRepository)
        {
            _scoutTypeRepository = scoutTypeRepository;
            _serilogLogger = Log.ForContext<ScoutTypeController>();
        }

        /// <summary>
        /// Gets the details of a given scout type
        /// </summary>
        /// <param name="scoutTypeId"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ScoutType))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetScoutTypeDetails(int scoutTypeId)
        {
            if (scoutTypeId < 0)
            {
                _serilogLogger.Warning($"ScoutTypeController: The user inputed an invalid scout type id");
                return BadRequest(new { statusCode = StatusCodes.Status400BadRequest, message = "There was a problem getting the scout type details you've requested" });
            }

            try
            {
                var response = _scoutTypeRepository.GetScoutTypeDetails(scoutTypeId);

                if (response == null)
                {
                    _serilogLogger.Error($"ScoutTypeController: The requested scout type wasn't found ");
                    return BadRequest(new { statusCode = StatusCodes.Status404NotFound, message = "There was a problem getting the scout types you've requested" });
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                _serilogLogger.Error(ex, "There was a problem getting the data for getting the scout type required!");
                return BadRequest(new { statusCode = StatusCodes.Status400BadRequest, message = "There was a problem getting the scout type details you've requested" });
            }
        }

        /// <summary>
        /// Creates a scout type
        /// </summary>
        /// <param name="scoutType"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ProducesResponseType(200, Type = typeof(ScoutType))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult CreateScoutType(ScoutType scoutType)
        {
            if (scoutType == null)
            {
                _serilogLogger.Warning($"ScoutTypeController: The user inputed some invalid data!");
                return BadRequest(new { statusCode = StatusCodes.Status400BadRequest, message = "There was a problem creating the scout type. Please input valid data" });
            }

            try
            {
                var response = _scoutTypeRepository.CreateScoutType(scoutType);

                if (!response)
                {
                    _serilogLogger.Error($"ScoutTypeController: The user inputed an invalid scoutTypeId");
                    return BadRequest(new { statusCode = StatusCodes.Status400BadRequest, message = "There was a problem creating the scout type. Please input valid data" });
                }

                return Ok(new { statusCode = StatusCodes.Status200OK, message = "The scout type was created!" });
            }
            catch (Exception ex)
            {
                _serilogLogger.Error(ex, "There was a problem getting the data for getting the scout type required!");
                return BadRequest(new { statusCode = StatusCodes.Status400BadRequest, message = "There was a problem creating the scout type. Please try again" });
            }
        }

        /// <summary>
        /// Edits the info of a given scout type
        /// </summary>
        /// <param name="scoutType"></param>
        /// <returns></returns>
        [HttpPut]
        [ValidateAntiForgeryToken]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult EditScoutType(ScoutType scoutType)
        {
            if (scoutType == null)
            {
                _serilogLogger.Warning($"ScoutTypeController: The user inputed an invalid scout type");
                return BadRequest(new { statusCode = StatusCodes.Status400BadRequest, message = "There was a problem editing the scout type you've requested. Please input valid data" });
            }

            try
            {
                var response = _scoutTypeRepository.EditScoutType(scoutType);

                if (!response)
                {
                    _serilogLogger.Error($"ScoutTypeController: The user inputed an invalid scout type");
                    return BadRequest(new { statusCode = StatusCodes.Status404NotFound, message = "There was a problem editing the scout type you've requested. Please input valid data" });
                }

                return Ok(new { statusCode = StatusCodes.Status200OK, message = "The scout type info was edited successfully!" });
            }
            catch (Exception ex)
            {
                _serilogLogger.Error(ex, "There was a problem editing the data of the required scout type!");
                return BadRequest(new { statusCode = StatusCodes.Status404NotFound, message = "There was a problem editing the scout type you've requested. Please input valid data" });
            }
        }

        /// <summary>
        /// Deletes a given scout type
        /// </summary>
        /// <param name="scoutType"></param>
        /// <returns></returns>
        [HttpDelete]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteScout(ScoutType scoutType)
        {
            if (scoutType == null)
            {
                _serilogLogger.Warning($"ScoutTypeController: The user inputed an invalid scout type");
                return BadRequest(new { statusCode = StatusCodes.Status404NotFound, message = "Scout type data invalid. Please insert valid scout data!" });
            }

            try
            {
                var response = _scoutTypeRepository.DeleteScoutType(scoutType);

                if (!response)
                {
                    _serilogLogger.Error($"ScoutTypeController: It wasn't possible to delete the required scout type!");
                    return BadRequest(new { statusCode = StatusCodes.Status400BadRequest, message = "It wasn't possible to delete the requested scout type. Please try again!" });
                }

                return Ok(new { statusCode = StatusCodes.Status200OK, message = "The scout type was deleted successfully!" });
            }
            catch (Exception ex)
            {
                _serilogLogger.Error(ex, "There was a problem deleting the scout type data!");
                return BadRequest(new { statusCode = StatusCodes.Status400BadRequest, message = "It wasn't possible to delete the requested scout type. Please try again!" });
            }
        }
    }
}