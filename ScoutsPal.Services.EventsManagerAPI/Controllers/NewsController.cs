using EventsPal.Services.EventsManagerAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ScoutsPal.Services.EventsManagerAPI.Models;
using ScoutsPal.Services.EventsManagerAPI.Services;
using ScoutsPal.Services.EventsManagerAPI.Services.Interfaces;
using Serilog;
using System.Reflection;

namespace ScoutsPal.Services.EventsManagerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class NewsController : ControllerBase
    {
        private readonly INewsRepository _newsRepository;
        private readonly Serilog.ILogger _serilogLogger;

        public NewsController(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
            _serilogLogger = Log.ForContext<NewsController>();
        }

        /// <summary>
        /// Gets all the news avaiable
        /// </summary>
        /// <returns>list of news</returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<News>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetAllNews()
        {
            try
            {
                var response = _newsRepository.GetAllNews();

                if (response == null || !response.Any())
                {
                    _serilogLogger.Error($"NewsController: There wasn't any data found");
                    return BadRequest(new { statusCode = StatusCodes.Status404NotFound, message = "There was a problem getting the data you've requested, please try again." });
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                _serilogLogger.Error(ex, "There was a problem getting the data for the requested event!");
                return BadRequest(new { statusCode = StatusCodes.Status400BadRequest, message = "There was a problem getting the data you've requested, please try again." });
            }
        }

        /// <summary>
        /// Gets a list of news by group
        /// </summary>
        /// <param name="groupId">the group that you want to filter the news</param>
        /// <returns>list of news by the given group</returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<News>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetNewsByGroup(long groupId)
        {
            if (groupId <= 0)
            {
                _serilogLogger.Warning($"NewsController: The user inputed an invalid groupId");
                return BadRequest(new { statusCode = StatusCodes.Status400BadRequest, message = "There was a problem getting the news you've requested" });
            }

            try
            {
                var response = _newsRepository.GetNewsByGroup(groupId);

                if (response == null || !response.Any())
                {
                    _serilogLogger.Error($"NewsController: There wasn't any data found");
                    return BadRequest(new { statusCode = StatusCodes.Status404NotFound, message = "There was a problem getting the data you've requested, please try again." });
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                _serilogLogger.Error(ex, "There was a problem getting the news data you've requested!");
                return BadRequest(new { statusCode = StatusCodes.Status400BadRequest, message = "There was a problem getting the data you've requested, please try again." });
            }
        }

        /// <summary>
        /// Gets the details of a given news
        /// </summary>
        /// <param name="newsId">news you want to check</param>
        /// <returns>satus code with the result</returns>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetNewsDetails(long newsId)
        {
            if (newsId < 0)
            {
                _serilogLogger.Warning($"NewsController: The user inputed an invalid newsId");
                return BadRequest(new { statusCode = StatusCodes.Status400BadRequest, message = "There was a problem getting the news details you've requested" });
            }

            try
            {
                var response = _newsRepository.GetNewsDetails(newsId);

                if (response == null)
                {
                    _serilogLogger.Error($"NewsController: The requested news wasn't found ");
                    return BadRequest(new { statusCode = StatusCodes.Status404NotFound, message = "There was a problem getting the news you've requested" });
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                _serilogLogger.Error(ex, "There was a problem getting the data for the requested news!");
                return BadRequest(new { statusCode = StatusCodes.Status400BadRequest, message = "There was a problem getting the news details you've requested" });
            }
        }

        /// <summary>
        /// Creates a news
        /// </summary>
        /// <param name="news">news to create</param>
        /// <returns>satus code with the result</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult CreateNews(News news)
        {
            if (news == null)
            {
                _serilogLogger.Warning($"NewsController: The user inputed some invalid data!");
                return BadRequest(new { statusCode = StatusCodes.Status400BadRequest, message = "There was a problem creating the news you've requested. Please input valid data!" });
            }

            try
            {
                var response = _newsRepository.CreateNews(news);

                if (!response)
                {
                    _serilogLogger.Error($"NewsController: The user inputed an invalid data!");
                    return BadRequest(new { statusCode = StatusCodes.Status400BadRequest, message = "There was a problem creating the news. Please try again!" });
                }

                return Ok(new { statusCode = StatusCodes.Status200OK, message = "The Scout was created!" });
            }
            catch (Exception ex)
            {
                _serilogLogger.Error(ex, "NewsController: There was a problem getting the news data required!");
                return BadRequest(new { statusCode = StatusCodes.Status400BadRequest, message = "There was a problem creating the news. Please try again" });
            }
        }

        /// <summary>
        /// Updated a given news
        /// </summary>
        /// <param name="news">news to update</param>
        /// <returns>satus code with the result</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult EditNews(News news)
        {
            if (news == null)
            {
                _serilogLogger.Warning($"NewsController: The user inputed an invalid news data!");
                return BadRequest(new { statusCode = StatusCodes.Status400BadRequest, message = "There was a problem editing the news you've requested. Please input valid data!" });
            }

            try
            {
                var response = _newsRepository.EditNews(news);

                if (!response)
                {
                    _serilogLogger.Error($"NewsController: The user inputed an invalid news data!");
                    return BadRequest(new { statusCode = StatusCodes.Status404NotFound, message = "There was a problem editing the news you've requested. Please input valid data!" });
                }

                return Ok(new { statusCode = StatusCodes.Status200OK, message = "The event info was edited successfully!" });
            }
            catch (Exception ex)
            {
                _serilogLogger.Error(ex, "NewsController: There was a problem editing the data of the requested news!");
                return BadRequest(new { statusCode = StatusCodes.Status404NotFound, message = "There was a problem editing the news you've requested. Please input valid data!" });
            }
        }

        /// <summary>
        /// Deletes a given news
        /// </summary>
        /// <param name="news">news to delete</param>
        /// <returns>satus code with the result</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteNews(News news)
        {
            if (news == null)
            {
                _serilogLogger.Warning($"NewsController: The user inputed an invalid news!");
                return BadRequest(new { statusCode = StatusCodes.Status404NotFound, message = "There was a problem deleting the news you've requested. Please input valid data!" });
            }

            try
            {
                var response = _newsRepository.DeleteNews(news);

                if (!response)
                {
                    _serilogLogger.Error($"NewsController: It wasn't possible to delete the requested news!");
                    return BadRequest(new { statusCode = StatusCodes.Status400BadRequest, message = "There was a problem deleting the requested news. Please try again!" });
                }

                return Ok(new { statusCode = StatusCodes.Status200OK, message = "The scout was deleted successfully!" });
            }
            catch (Exception ex)
            {
                _serilogLogger.Error(ex, "NewsController: There was a problem deleting the news data!");
                return BadRequest(new { statusCode = StatusCodes.Status400BadRequest, message = "It wasn't possible to delete the requested news. Please try again!" });
            }
        }
    }
}
