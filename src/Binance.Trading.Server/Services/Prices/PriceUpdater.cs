using System;
using System.Threading.Tasks;
using Binance.Trading.Domain.Services;

namespace Binance.Trading.Server.Services.Prices
{
    public class PriceUpdater : IPriceService
    {
        public Task<double> GetCurrentPriceTicker(string tickerName)
        {
            throw new NotImplementedException();
        }

        public Task<double> GetCurrentPriceTime(DateTime time)
        {
            throw new NotImplementedException();
        }

        public Task GetPricesForTicker(string tickerName)
        {
            throw new NotImplementedException();
        }
    }
}
