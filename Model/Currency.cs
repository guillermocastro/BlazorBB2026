using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BlazorBB2026.Model
{
    public class Currency
    {
        public string CurrencyId { get; set; } = null!;
        public string Currency3N { get; set; } = null!;
        public string CurrencyName { get; set; } = null!;
        public decimal? LastValue { get; set; }
        public bool IsEnabled { get; set; } = true;
    }
}
