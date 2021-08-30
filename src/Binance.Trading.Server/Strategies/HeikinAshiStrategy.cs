using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Binance.Trading.Domain.Models;
using Binance.Trading.Server.StrategySettings;
using Microsoft.Extensions.Configuration;

namespace Binance.Trading.Server.Strategies
{
    class HeikinAshiStrategy : TradingStrategy
    {
        private readonly HeikinAshiStrategySettings _settings;

        public HeikinAshiStrategy(IConfiguration configuration)
        {
            _settings = new HeikinAshiStrategySettings();
            configuration.GetSection("HeikinAshiStrategy").Bind(_settings);
        }

        public override GeneralSettings GetSettings()
            => _settings;

        public override Task<TradeAction> TakeEntryDecision(string tickerName)
        {
            return base.TakeEntryDecision(tickerName);
        }

        public override Task<TradeAction> TakeExitDecision(string tickerName)
        {
            return base.TakeExitDecision(tickerName);
        }
    }
}
