using BlazorBB2026.Model;
using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;


namespace BlazorBB2026.Services
{
    public class CommonService : ICommonService
    {
        private readonly IConfiguration _configuration;
        public CommonService(IConfiguration _config)
        {
            _configuration = _config;
        }
        public async Task<List<Currency>> GetCurrencies()
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                DynamicParameters parameters = new DynamicParameters();
                //parameters.Add("Page", Page);
                //parameters.Add("Rows", Rows);
                var sqlquery = "SELECT * FROM vk.Currency";
                IEnumerable<Currency> currencylist = await con.QueryAsync<Currency>(sqlquery, parameters, commandType: CommandType.Text);
                return currencylist.ToList();
            }
        }
        public async Task<string> PutCurrency(Currency currency)
        {
            using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                const string sqlquery = @"UPDATE [vk].[Currency] SET [Currency3N]=@Currency3N ,[CurrencyName]=@CurrencyName ,[LastValue]=@LastValue ,[IsEnabled]=@IsEnabled WHERE CurrencyId=@CurrencyId";
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                try
                {
                    await conn.ExecuteAsync(sqlquery, new { currency.CurrencyId, currency.Currency3N, currency.CurrencyName, currency.LastValue, currency.IsEnabled }, commandType: CommandType.Text);
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
            return "Currency row updated";
        }
        public async Task<string> DeleteCurrency(Currency currency)
        {
            using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                const string sqlquery = @"DELETE FROM [vk].[Currency] WHERE [CurrencyId]=@CurrencyId";
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                try
                {
                    await conn.ExecuteAsync(sqlquery, new { currency.CurrencyId, currency.Currency3N, currency.CurrencyName, currency.LastValue, currency.IsEnabled }, commandType: CommandType.Text);
                }
                catch (Exception ex)
                {
                    //throw ex;
                    return ex.Message;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }
            return "Currency row deleted";
        }
        public async Task<string> PostCurrency(Currency currency)
        {
            using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                const string sqlquery = @"INSERT INTO [vk].[Currency] ([CurrencyId] ,[Currency3N] ,[CurrencyName] ,[LastValue] ,[IsEnabled]) VALUES (@CurrencyId, @Currency3N, @CurrencyName, @LastValue, @IsEnabled)";
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                try
                {
                    await conn.ExecuteAsync(sqlquery, new { currency.CurrencyId, currency.Currency3N, currency.CurrencyName, currency.LastValue, currency.IsEnabled }, commandType: CommandType.Text);
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
            return "Currency row created";
        }
        public async Task<List<Country>> GetCountries()
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                DynamicParameters parameters = new DynamicParameters();
                //parameters.Add("Page", Page);
                //parameters.Add("Rows", Rows);
                var sqlquery = "SELECT * FROM vk.Country";
                IEnumerable<Country> countrylist = await con.QueryAsync<Country>(sqlquery, parameters, commandType: CommandType.Text);
                return countrylist.ToList();
            }
        }
    }
}
