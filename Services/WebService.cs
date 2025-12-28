using BlazorBB2026.Components.Web;
using BlazorBB2026.Model;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using Article = BlazorBB2026.Model.Article;
namespace BlazorBB2026.Services
{
    public class WebService : IWebService { 
        private readonly IConfiguration _configuration;
        public WebService(IConfiguration _config)
        {
            _configuration = _config;
        }
        public async Task<List<Calendar>> GetCalendars(){
            List<Calendar> calendars = new List<Calendar>();
            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var result = await conn.QueryAsync<Calendar>("SELECT DateId, CalendarName, DescriptionES, DescriptionEN FROM Calendars ORDER BY DateId");
                calendars = result.ToList();
            }
            return calendars;
        }
        public async Task<string> PutCalendar(Calendar calendar){
            using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                const string sqlquery = @"UPDATE [vk].[Calendar] SET [DateId]=@DateId ,[CalendarName]=@CalendarName ,[DescriptionES]=@DescriptionES ,[DescriptionEN]=@DescriptionEN WHERE DateId=@DateId";
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                try
                {
                    await conn.ExecuteAsync(sqlquery, new { calendar.DateId, calendar.CalendarName, calendar.DescriptionES, calendar.DescriptionEN }, commandType: CommandType.Text);
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }
            return "Calentar row updated";
        }
        public async Task<string> DeleteCalendar(Calendar calendar){
            using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                const string sqlquery = @"DELETE FROM [vk].[Calendar] WHERE DateId=@DateId";
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                try
                {
                    await conn.ExecuteAsync(sqlquery, new { calendar.DateId }, commandType: CommandType.Text);
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }
            return "Calendar row deleted";
        }
        public async Task<string> PostCalendar(Calendar calendar){
            using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                const string sqlquery = @"INSERT INTO [vk].[Calendar] ([DateId],[CalendarName],[DescriptionES],[DescriptionEN]) VALUES (@DateId,@CalendarName,@DescriptionES,@DescriptionEN)";
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                try
                {
                    await conn.ExecuteAsync(sqlquery, new { calendar.DateId, calendar.CalendarName, calendar.DescriptionES, calendar.DescriptionEN }, commandType: CommandType.Text);
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }
            return "Calendar row inserted";
        }
        public async Task<List<Article>> GetArticles(){
            List<Article> articles = new List<Article>();
            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var result = await conn.QueryAsync<Article>("SELECT ArticleId, [Page], TitleES, TitleEN, Svg, TextES, TextEN, DescriptionES, DescriptionEN, Url, IsEnabled FROM [vk].[Article] ORDER BY ArticleId");
                articles = result.ToList();
            }
            return articles;
        }
        public async Task<string> PutArticle(Article article){
            using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                const string sqlquery = @"UPDATE [vk].[Article] SET [Page]=@Page, [TitleES]=@TitleES ,[TitleEN]=@TitleEN ,[Svg]=@Svg ,[TextES]=@TextES ,[TextEN]=@TextEN ,[DescriptionES]=@DescriptionES ,[DescriptionEN]=@DescriptionEN ,[Url]=@Url ,[IsEnabled]=@IsEnabled WHERE ArticleId=@ArticleId";
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                try
                {
                    await conn.ExecuteAsync(sqlquery, new { article.Page, article.TitleES, article.TitleEN, article.Svg, article.TextES, article.TextEN, article.DescriptionES, article.DescriptionEN, article.Url, article.IsEnabled, article.ArticleId }, commandType: CommandType.Text);
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }
            return "Article row updated";
        }
        public async Task<string> DeleteArticle(Article article){
            using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                const string sqlquery = @"DELETE FROM [vk].[Article] WHERE ArticleId=@ArticleId";
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                try
                {
                    await conn.ExecuteAsync(sqlquery, new { article.ArticleId }, commandType: CommandType.Text);
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }
            return "Article row deleted";
        }
        public async Task<string> PostArticle(Article article) {
            using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                const string sqlquery = @"INSERT INTO [vk].[Article] ([ArticleId],[Page],[TitleES],[TitleEN],[Svg],[TextES],[TextEN],[DescriptionES],[DescriptionEN],[Url],[IsEnabled]) VALUES (@ArticleId,@Page,@TitleES,@TitleEN,@Svg,@TextES,@TextEN,@DescriptionES,@DescriptionEN,@Url,@IsEnabled)";
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                try
                {
                    await conn.ExecuteAsync(sqlquery, new { article.ArticleId, article.Page, article.TitleES, article.TitleEN, article.Svg, article.TextES, article.TextEN, article.DescriptionES, article.DescriptionEN, article.Url, article.IsEnabled }, commandType: CommandType.Text);
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }
            return "Article row inserted";
        }
        public async Task<List<Announcement>> GetAnnouncements() {
            List<Announcement> announcements = new List<Announcement>();
            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var result = await conn.QueryAsync<Announcement>("SELECT AnnouncementId, AnnouncementDate, TitleES, TitleEN, DescriptionES, DescriptionEN FROM vk.Announcement ORDER BY AnnouncementDate DESC");
                announcements = result.ToList();
            }
            return announcements;
        }
        public async Task<string> PutAnnouncement(Announcement announcement)
        {
            using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                const string sqlquery = @"UPDATE [vk].[Announcement] SET [AnnouncementDate]=@AnnouncementDate, [TitleES]=@TitleES ,[TitleEN]=@TitleEN ,[DescriptionES]=@DescriptionES,[DescriptionEN]=@DescriptionEN WHERE [AnnouncementId]=@AnnouncementId";
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                try
                {
                    await conn.ExecuteAsync(sqlquery, new { announcement.AnnouncementId, announcement.AnnouncementDate, announcement.TitleES, announcement.TitleEN, announcement.DescriptionES, announcement.DescriptionEN }, commandType: CommandType.Text);
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }
            return "Article row updated";
        }
        public async Task<string> DeleteAnnouncement(Announcement announcement) { 
            using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                const string sqlquery = @"DELETE FROM [vk].[Announcement] WHERE AnnouncementId=@AnnouncementId";
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                try
                {
                    await conn.ExecuteAsync(sqlquery, new { announcement.AnnouncementId }, commandType: CommandType.Text);
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }
            return "Announcement row deleted";
        }
        public async Task<string> PostAnnouncement(Announcement announcement) { 
            using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                const string sqlquery = @"INSERT INTO [vk].[Announcement] ([AnnouncementDate],[TitleES],[TitleEN],[DescriptionES],[DescriptionEN]) VALUES (@AnnouncementDate,@TitleES,@TitleEN,@DescriptionES,@DescriptionEN)";
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                try
                {
                    await conn.ExecuteAsync(sqlquery, new { announcement.AnnouncementDate, announcement.TitleES, announcement.TitleEN, announcement.DescriptionES, announcement.DescriptionEN }, commandType: CommandType.Text);
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }
            return "Announcement row inserted";
        }
        public async Task<List<MetaTag>> GetMetaTags() { 
            List<MetaTag> metaTags = new List<MetaTag>();
            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var result = await conn.QueryAsync<MetaTag>("SELECT [LanguageId],[Keywords],[Description] FROM [vk].[MetaTag] ORDER BY LanguageId");
                metaTags = result.ToList();
            }
            return metaTags;
        }
        public async Task<string> PutMetaTags(MetaTag metaTag) { 
            using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                const string sqlquery = @"UPDATE [vk].[MetaTag] SET [Keywords]=@Keywords, [Description]=@Description WHERE LanguageId=@LanguageId";
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                try
                {
                    await conn.ExecuteAsync(sqlquery, new { metaTag.LanguageId, metaTag.Keywords, metaTag.Description }, commandType: CommandType.Text);
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }
            return "MetaTag row updated";
        }
        public async Task<string> DeleteMetaTags(MetaTag metaTag) { 
            using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                const string sqlquery = @"DELETE FROM [vk].[MetaTag] WHERE LanguageId=@LanguageId";
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                try
                {
                    await conn.ExecuteAsync(sqlquery, new { metaTag.LanguageId }, commandType: CommandType.Text);
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }
            return "MetaTag row deleted";
        }
        public async Task<string> PostMetaTags(MetaTag metaTag) { 
            using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                const string sqlquery = @"INSERT INTO [vk].[MetaTag] ([LanguageId],[Keywords],[Description]) VALUES (@LanguageId,@Keywords,@Description)";
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                try
                {
                    await conn.ExecuteAsync(sqlquery, new { metaTag.LanguageId, metaTag.Keywords, metaTag.Description }, commandType: CommandType.Text);
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }
            return "MetaTag row inserted";
        }
    }
}