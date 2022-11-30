using ScoutsPal.Services.EventsManagerAPI.Models;

namespace ScoutsPal.Services.EventsManagerAPI.Services.Interfaces
{
    public interface INewsRepository
    {
        public IEnumerable<News> GetAllNews();

        public IEnumerable<News> GetNewsByGroup(int groupId);

        public News GetNewsDetails(long eventId);

        public bool CreateNews(News news);

        public bool EditNews(News news);

        public bool DeleteNews(News news);

        public bool ExistsNewsForEvent(long eventId);

        public bool ExistsNews(long newsId);

    }
}
