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
            using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                DynamicParameters parameters = new DynamicParameters();
                //parameters.Add("Page", Page);
                var sqlquery = "SELECT * FROM vk.Currency";
                IEnumerable<Currency> currencylist = await conn.QueryAsync<Currency>(sqlquery, parameters, commandType: CommandType.Text);
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
                var sqlquery = "SELECT * FROM vk.Country";
                IEnumerable<Country> countrylist = await con.QueryAsync<Country>(sqlquery, parameters, commandType: CommandType.Text);
                return countrylist.ToList();
            }
        }
        public async Task<List<InvoiceClass>> GetInvoiceClasses() {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                DynamicParameters parameters = new DynamicParameters();
                //parameters.Add("Page", Page);
                var sqlquery = "SELECT * FROM vk.InvoiceClass";
                IEnumerable<InvoiceClass> InvoiceClasslist = await con.QueryAsync<InvoiceClass>(sqlquery, parameters, commandType: CommandType.Text);
                return InvoiceClasslist.ToList();
            }
        }
        public async Task<List<InvoiceIssuer>> GetInvoiceIssuers() {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                DynamicParameters parameters = new DynamicParameters();
                //parameters.Add("Page", Page);
                var sqlquery = "SELECT * FROM vk.InvoiceIssuer";
                IEnumerable<InvoiceIssuer> InvoiceIssuerlist = await con.QueryAsync<InvoiceIssuer>(sqlquery, parameters, commandType: CommandType.Text);
                return InvoiceIssuerlist.ToList();
            }
        }
        public async Task<List<InvoiceType>> GetInvoiceTypes() {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                DynamicParameters parameters = new DynamicParameters();
                //parameters.Add("Page", Page);
                var sqlquery = "SELECT * FROM vk.InvoiceType";
                IEnumerable<InvoiceType> InvoiceTypelist = await con.QueryAsync<InvoiceType>(sqlquery, parameters, commandType: CommandType.Text);
                return InvoiceTypelist.ToList();
            }
        }
        public async Task<List<OperationKey>> GetOperationKeys() {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                DynamicParameters parameters = new DynamicParameters();
                //parameters.Add("Page", Page);
                var sqlquery = "SELECT * FROM vk.OperationKey";
                IEnumerable<OperationKey> OperationKeylist = await con.QueryAsync<OperationKey>(sqlquery, parameters, commandType: CommandType.Text);
                return OperationKeylist.ToList();
            }
        }
        public async Task<List<OperationType>> GetOperationTypes() {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                DynamicParameters parameters = new DynamicParameters();
                //parameters.Add("Page", Page);
                var sqlquery = "SELECT * FROM vk.OperationType";
                IEnumerable<OperationType> OperationTypelist = await con.QueryAsync<OperationType>(sqlquery, parameters, commandType: CommandType.Text);
                return OperationTypelist.ToList();
            }
        }
        //public string GetCS()
        //{
        //    var cs = _configuration.GetConnectionString("DefaultConnection") ?? "";
        //    return cs;
        //}
        public async Task<bool> IsRoleMember(string? username, string? rolename) 
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("user", username);
                parameters.Add("role", rolename);
                var sqlquery = "SELECT u.[UserName],r.[Name] AS 'RoleName' FROM [dbo].[AspNetUsers] u INNER JOIN [dbo].[AspNetUserRoles] ur ON u.Id=ur.UserId INNER JOIN [dbo].[AspNetRoles] r ON ur.RoleId=r.Id WHERE u.[UserName]=ISNULL(@user,'') AND r.[Name]=ISNULL(@role,'')";
                IEnumerable<UserRole> userRoleList = await con.QueryAsync<UserRole>(sqlquery, parameters, commandType: CommandType.Text);
                if (userRoleList.Any())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public async Task<List<UserRole>> GetUserRoles(string? username)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("user", username);
                var sqlquery = "SELECT u.[UserName],r.[Name] AS 'RoleName' FROM [dbo].[AspNetUsers] u INNER JOIN [dbo].[AspNetUserRoles] ur ON u.Id=ur.UserId INNER JOIN [dbo].[AspNetRoles] r ON ur.RoleId=r.Id WHERE u.[UserName]=ISNULL(@user,'')";
                IEnumerable<UserRole> userRoleList = await con.QueryAsync<UserRole>(sqlquery, parameters, commandType: CommandType.Text);
                return userRoleList.ToList();
            }
        }
        public async Task<List<AspNetUsers>> GetAllUsers()
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                DynamicParameters parameters = new DynamicParameters();
                var sqlquery = "SELECT * FROM [dbo].[AspNetUsers]";
                IEnumerable<AspNetUsers> userList = await con.QueryAsync<AspNetUsers>(sqlquery, parameters, commandType: CommandType.Text);
                return userList.ToList();
            }
        }
        public async Task<List<AspNetRoles>> GetAllRoles()
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                DynamicParameters parameters = new DynamicParameters();
                var sqlquery = "SELECT * FROM [dbo].[AspNetRoles]";
                IEnumerable<AspNetRoles> roleList = await con.QueryAsync<AspNetRoles>(sqlquery, parameters, commandType: CommandType.Text);
                return roleList.ToList();
            }
        }
    }
}
