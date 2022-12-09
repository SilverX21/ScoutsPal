using ScoutsPal.Services.EventsManagerAPI.Models;

namespace ScoutsPal.Services.EventsManagerAPI.Services.Interfaces
{
    public interface INewsRepository
    {
        public IEnumerable<News> GetAllNews();

        public IEnumerable<News> GetNewsByGroup(long groupId);

        public News GetNewsDetails(long newsId);

        public bool CreateNews(News news);

        public bool EditNews(News news);

        public bool DeleteNews(News news);

        public bool ExistsNewsForEvent(long newsId);

        public bool ExistsNews(long newsId);

    }
}
