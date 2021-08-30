using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Binance.Trading.Domain.Models
{
    public class TradingStrategy
    {
        public string StrategyName { get; set; }
        public int Period { get; set; }

        public virtual GeneralSettings GetSettings()
            => new GeneralSettings();

        public virtual Task<TradeAction> TakeEntryDecision(string tickerName)
        {
            throw new NotImplementedException();
        }

        public virtual Task<TradeAction> TakeExitDecision(string tickerName)
        {
            throw new NotImplementedException();
        }
    }
}
