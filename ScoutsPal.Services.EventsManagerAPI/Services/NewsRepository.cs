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

        public bool CreateNews(News news)
        {
            throw new NotImplementedException();
        }

        public bool DeleteNews(News news)
        {
            throw new NotImplementedException();
        }

        public bool EditNews(News news)
        {
            throw new NotImplementedException();
        }

        public bool ExistsNews(long newsId)
        {
            throw new NotImplementedException();
        }

        public bool ExistsNewsForEvent(long eventId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<News> GetAllNews()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<News> GetNewsByGroup(int groupId)
        {
            throw new NotImplementedException();
        }

        public News GetNewsDetails(long eventId)
        {
            throw new NotImplementedException();
        }
    }
}
