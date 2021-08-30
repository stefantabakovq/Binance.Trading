using System;
using System.Threading.Tasks;

namespace Binance.Trading.Domain.Services
{
    public interface IPriceService
    {
        Task GetPricesForTicker(string tickerName);
        Task<double> GetCurrentPriceTicker(string tickerName);
        Task<double> GetCurrentPriceTime(DateTime time);
    }
}
