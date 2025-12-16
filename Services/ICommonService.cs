using BlazorBB2026.Model;

namespace BlazorBB2026.Services
{
    public interface ICommonService
    {
        Task<List<Currency>> GetCurrencies();
        Task<string> PutCurrency(Currency currency);
        Task<string> DeleteCurrency(Currency currency);
        Task<string> PostCurrency(Currency currency);
        Task<List<Country>> GetCountries();

    }
}
