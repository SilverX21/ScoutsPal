using Microsoft.Extensions.Logging;
using ScoutsPal.Services.EventsManagerAPI.DbContexts;
using ScoutsPal.Services.EventsManagerAPI.Models;
using ScoutsPal.Services.EventsManagerAPI.Services.Interfaces;
using Serilog;

namespace ScoutsPal.Services.EventsManagerAPI.Services
{
    public class NewsRepository : INewsRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly Serilog.ILogger _serilogLogger;
        public NewsRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _serilogLogger = Log.ForContext<NewsRepository>();
        }

        /// <summary>
        /// Method that creates a news
        /// </summary>
        /// <param name="news"></param>
        /// <returns></returns>
        public bool CreateNews(News news)
        {
            if (news == null)
            {
                _serilogLogger.Warning("NewsRepository: The inputed data is null");
                return false;
            }

            try
            {
                if (ExistsNews(news.NewsId))
                {
                    _serilogLogger.Warning("NewsRepository: news doesn't exist!");
                    return false;
                }

                _dbContext.News.Add(news);
                _dbContext.SaveChanges();
                _serilogLogger.Information("NewsRepository: The news was created!");
                return true;
            }
            catch (Exception ex)
            {
                _serilogLogger.Error(ex.Message, "NewsRepository: There was a problem during the execution");
                return false;
            }
        }

        /// <summary>
        /// Method that deletes a given news
        /// </summary>
        /// <param name="news"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool DeleteNews(News news)
        {
            if (news == null)
            {
                _serilogLogger.Warning("NewsRepository: The inputed data is null");
                return false;
            }

            try
            {
                if (ExistsNews(news.NewsId))
                {
                    _serilogLogger.Warning("NewsRepository: news doesn't exist!");
                    return false;
                }

                _dbContext.News.Remove(news);
                _dbContext.SaveChanges();
                _serilogLogger.Information("NewsRepository: The news was removed!");
                return true;
            }
            catch (Exception ex)
            {
                _serilogLogger.Warning(ex.Message, "NewsRepository: There was a problem during the execution");
                return false;
            }
        }

        /// <summary>
        /// Method that updates a certain news details
        /// </summary>
        /// <param name="news"></param>
        /// <returns></returns>
        public bool EditNews(News news)
        {
            if (news == null)
            {
                _serilogLogger.Warning("NewsRepository: The inputed data is null");
                return false;
            }

            try
            {
                if (!ExistsNews(news.NewsId))
                {
                    _serilogLogger.Warning("NewsRepository: The news doesn't exist!");
                    return false;
                }

                _dbContext.News.Update(news);
                _dbContext.SaveChanges();
                _serilogLogger.Information("NewsRepository: The news info was updated!");
                return true;
            }
            catch (Exception ex)
            {
                _serilogLogger.Error(ex.Message, "NewsRepository: There was a problem during the execution");
                return false;
            }
        }

        /// <summary>
        /// Method that checks if a given news exists
        /// </summary>
        /// <param name="newsId"></param>
        /// <returns></returns>
        public bool ExistsNews(long newsId)
        {
            if (newsId <= 0)
            {
                _serilogLogger.Warning("NewsRepository: The inputed data is not valid");
                return false;
            }

            _serilogLogger.Information("NewsRepository: confirmed news existence!");
            return _dbContext.News.Any(x => x.NewsId == newsId);
        }

        /// <summary>
        /// Method that checks if certain event has news
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public bool ExistsNewsForEvent(long eventId)
        {
            if (eventId <= 0)
            {
                _serilogLogger.Warning("NewsRepository: The inputed data is not valid");
                return false;
            }

            _serilogLogger.Information("NewsRepository: confirmed news existence!");
            return _dbContext.News.Any(x => x.EventId == eventId);
        }

        /// <summary>
        /// Method that gets all the news avaiable
        /// </summary>
        /// <returns></returns>
        public IEnumerable<News> GetAllNews()
        {
            try
            {
                return _dbContext.News.ToList();
            }
            catch (Exception ex)
            {
                _serilogLogger.Error(ex.Message, "NewsRepository: There was a problem during the execution!");
                return null;
            }
        }

        /// <summary>
        /// /Method that gets a certain news details
        /// </summary>
        /// <param name="newsId"></param>
        /// <returns></returns>
        public News GetNewsDetails(long newsId)
        {
            if (newsId <= 0)
            {
                _serilogLogger.Warning("NewsRepository: The inputed data isn't valid!");
                return null;
            }

            try
            {
                if (!ExistsNews(newsId))
                {
                    _serilogLogger.Warning("NewsRepository: The news doesn't exist!");
                    return null;
                }

                _serilogLogger.Information("NewsRepository: Fetched the news info!");
                return _dbContext.News.FirstOrDefault(x => x.EventId == newsId);
            }
            catch (Exception ex)
            {
                _serilogLogger.Error(ex.Message, "NewsRepository: There was a problem during the execution!");
                return null;
            }
        }

        /// <summary>
        /// Method that gets news by group
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public IEnumerable<News> GetNewsByGroup(int groupId)
        {
            List<News> newsList = new List<News>();

            if (groupId <= 0)
            {
                _serilogLogger.Warning("NewsRepository: The inputed data isn't valid!");
                return newsList;
            }

            try
            {
                newsList = _dbContext.News.Where(x => x.GroupId == groupId).ToList();
                _serilogLogger.Information("NewsRepository: Fetched some news by group!");
                return newsList;
            }
            catch (Exception ex)
            {
                _serilogLogger.Error(ex.Message, "NewsRepository: There was a problem during the execution!");
                return null;
            }
        }
    }
}
