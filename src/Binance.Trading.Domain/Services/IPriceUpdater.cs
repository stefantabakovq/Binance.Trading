using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Binance.Trading.Domain.Services
{
    public interface IPriceUpdater
    {
        Task GetPricesForTicker(string tickerName);
        Task<double> GetCurrentPriceTicker(string tickerName);
        Task<double> GetCurrentPriceTime(DateTime time);
    }
}
