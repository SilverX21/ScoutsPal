using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ScoutsPAl.Services.ScoutsManagerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ScoutController : ControllerBase
    {
        private readonly ILogger<ScoutController> _logger;

        public ScoutController(ILogger<ScoutController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Gets a list of scouts filtered by type
        /// </summary>
        /// <param name="scoutTypeId">Scout Type required</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetScoutsByType(int scoutTypeId)
        {
            if (scoutTypeId < 0)
            {
                _logger.LogWarning($"ScoutsController: The user inputed an invalid ScoutTypeId");
                return BadRequest("There was a problem getting the scouts you've requested");
            }

            try
            {
                //logica dos scouts
                var response = "";
                //logica dos scouts

                if (response == null)
                {
                    _logger.LogWarning($"ScoutsController: The user inputed an invalid ScoutTypeId");
                    return BadRequest("There was a problem getting the data you've requested, please try again.");
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was a problem getting the data for getting the scout Type required!");
                return BadRequest("There was a problem getting the data you've requested, please try again.");
            }
        }

        // GET: Scout/Details/5
        [HttpGet]
        public ActionResult Details(int scoutId)
        {
            if (scoutId < 0)
            {
                _logger.LogWarning($"ScoutsController: The user inputed an invalid scout id");
                return BadRequest("There was a problem getting the scout you've requested");
            }

            try
            {
                //logica dos scouts
                var response = "";
                //logica dos scouts

                if (response == null)
                {
                    _logger.LogWarning($"ScoutsController: The user inputed an invalid scoutId");
                    return BadRequest("There was a problem getting the data you've requested, please try again.");
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was a problem getting the data for getting the Scout required!");
                return BadRequest("There was a problem getting the data you've requested, please try again.");
            }
        }

        // GET: Scout/Create
        public ActionResult Create()
        {
            return Ok();
        }

        // POST: Scout/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return Ok();
            }
        }

        // GET: Scout/Edit/5
        public ActionResult Edit(int id)
        {
            return Ok();
        }

        // POST: Scout/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return Ok();
            }
        }

        // GET: Scout/Delete/5
        public ActionResult Delete(int id)
        {
            return Ok();
        }

        // POST: Scout/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return Ok();
            }
        }
    }
}