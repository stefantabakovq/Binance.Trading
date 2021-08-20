using System;
using System.Threading.Tasks;
using Binance.Trading.Domain.Services;

namespace Binance.Trading.Domain.Models
{
    public class TradingStrategy : ITradingStrategy
    {
        public string StrategyName { get; set; }
        public int Period { get; set; }

        public Task<TradeAction> TakeEntryDecision(string tickerName)
        {
            throw new NotImplementedException();
        }

        public Task<TradeAction> TakeExitDecision(string tickerName)
        {
            throw new NotImplementedException();
        }
    }
}
