using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScoutsPAl.Services.ScoutsManagerAPI.Models;
using ScoutsPAl.Services.ScoutsManagerAPI.Services.Interfaces;

namespace ScoutsPAl.Services.ScoutsManagerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ScoutController : ControllerBase
    {
        private readonly ILogger<ScoutController> _logger;
        private readonly IScoutRepository _scoutRepository;

        public ScoutController(IScoutRepository scoutRepository, ILogger<ScoutController> logger)
        {
            _logger = logger;
            _scoutRepository = scoutRepository;
        }

        /// <summary>
        /// Gets a list of scouts filtered by type
        /// </summary>
        /// <param name="scoutTypeId">Scout Type required</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<Scout>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetScoutsByType(int scoutTypeId)
        {
            if (scoutTypeId <= 0)
            {
                _logger.LogWarning($"ScoutsController: The user inputed an invalid ScoutTypeId");
                return BadRequest(new { statusCode = StatusCodes.Status400BadRequest, message = "There was a problem getting the scouts you've requested" });
            }

            try
            {
                var response = _scoutRepository.GetScoutsByType(scoutTypeId);

                if (response == null || !response.Any())
                {
                    _logger.LogError($"ScoutsController: There wasn't any data found");
                    return BadRequest(new { statusCode = StatusCodes.Status404NotFound, message = "There was a problem getting the data you've requested, please try again." });
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was a problem getting the data for getting the scout Type required!");
                return BadRequest(new { statusCode = StatusCodes.Status400BadRequest, message = "There was a problem getting the data you've requested, please try again." });
            }
        }

        /// <summary>
        /// Gets the details of a given scout
        /// </summary>
        /// <param name="scoutId"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(Scout))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetScoutDetails(long scoutId)
        {
            if (scoutId < 0)
            {
                _logger.LogWarning($"ScoutsController: The user inputed an invalid scout id");
                return BadRequest(new { statusCode = StatusCodes.Status400BadRequest, message = "There was a problem getting the scout details you've requested" });
            }

            try
            {
                var response = _scoutRepository.GetScoutDetails(scoutId);

                if (response == null)
                {
                    _logger.LogError($"ScoutsController: The requested scout wasn't found ");
                    return BadRequest(new { statusCode = StatusCodes.Status404NotFound, message = "There was a problem getting the scouts you've requested" });
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was a problem getting the data for getting the Scout required!");
                return BadRequest(new { statusCode = StatusCodes.Status400BadRequest, message = "There was a problem getting the scout details you've requested" });
            }
        }

        /// <summary>
        /// Creates a scouts
        /// </summary>
        /// <param name="scout">scout details</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ProducesResponseType(200, Type = typeof(Scout))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult CreateScout(Scout scout)
        {
            if (scout == null)
            {
                _logger.LogWarning($"ScoutsController: The user inputed some invalid data!");
                return BadRequest(new { statusCode = StatusCodes.Status400BadRequest, message = "There was a problem getting the scouts you've requested. Please input valid data" });
            }

            try
            {
                var response = _scoutRepository.CreateScout(scout);

                if (!response)
                {
                    _logger.LogError($"ScoutsController: The user inputed an invalid scoutId");
                    return BadRequest(new { statusCode = StatusCodes.Status400BadRequest, message = "There was a problem creating the scout. Please input valid data" });
                }

                return Ok(new { statusCode = StatusCodes.Status200OK, message = "The Scout was created!" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was a problem getting the data for getting the Scout required!");
                return BadRequest(new { statusCode = StatusCodes.Status400BadRequest, message = "There was a problem creating the scout. Please try again" });
            }
        }

        /// <summary>
        /// Edits a given scout
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        [ValidateAntiForgeryToken]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult EditScout(Scout scout)
        {
            if (scout == null)
            {
                _logger.LogWarning($"ScoutsController: The user inputed an invalid scout");
                return BadRequest(new { statusCode = StatusCodes.Status400BadRequest, message = "There was a problem editing the scout you've requested. Please input valid data" });
            }

            try
            {
                var response = _scoutRepository.EditScout(scout);

                if (!response)
                {
                    _logger.LogError($"ScoutsController: The user inputed an invalid scout");
                    return BadRequest(new { statusCode = StatusCodes.Status404NotFound, message = "There was a problem editing the scout you've requested. Please input valid data" });
                }

                return Ok(new { statusCode = StatusCodes.Status200OK, message = "The scout info was edited successfully!" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was a problem editing the data of the required Scout!");
                return BadRequest(new { statusCode = StatusCodes.Status404NotFound, message = "There was a problem editing the scout you've requested. Please input valid data" });
            }
        }

        /// <summary>
        /// Deletes a given scout
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteScout(Scout scout)
        {
            if (scout == null)
            {
                _logger.LogWarning($"ScoutsController: The user inputed an invalid scout");
                return BadRequest(new { statusCode = StatusCodes.Status404NotFound, message = "Scout data invalid. Please insert valid scout data!" });
            }

            try
            {
                var response = _scoutRepository.DeleteScout(scout);

                if (!response)
                {
                    _logger.LogError($"ScoutsController: It wasn't possible to delete the required scout!");
                    return BadRequest(new { statusCode = StatusCodes.Status400BadRequest, message = "It wasn't possible to delete the requested scout. Please try again!" });
                }

                return Ok(new { statusCode = StatusCodes.Status200OK, message = "The scout was deleted successfully!" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was a problem deleting the scout data!");
                return BadRequest(new { statusCode = StatusCodes.Status400BadRequest, message = "It wasn't possible to delete the requested scout. Please try again!" });
            }
        }
    }
}