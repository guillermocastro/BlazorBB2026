using BlazorBB2026.Model;

namespace BlazorBB2026.Services
{
    public interface IWebService
    {
        Task<List<Calendar>> GetCalendars();
        Task<string> PutCalendar(Calendar calendar);
        Task<string> DeleteCalendar(Calendar calendar);
        Task<string> PostCalendar(Calendar calendar);
        Task<List<Article>> GetArticles();
        Task<string> PutArticle(Article article);
        Task<string> DeleteArticle(Article article);
        Task<string> PostArticle(Article article);
        Task<List<Announcement>> GetAnnouncements();
        Task<string> PutAnnouncement(Announcement announcement);
        Task<string> DeleteAnnouncement(Announcement announcement);
        Task<string> PostAnnouncement(Announcement announcement);
        Task<List<MetaTag>> GetMetaTags();
        Task<string> PutMetaTags(MetaTag metaTag);
        Task<string> DeleteMetaTags(MetaTag metaTag);
        Task<string> PostMetaTags(MetaTag metaTag);
    }
}
