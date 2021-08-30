using Binance.Trading.Domain.Models;

namespace Binance.Trading.Server.StrategySettings
{
    public class HeikinAshiStrategySettings : GeneralSettings
    {
        public int ReversalTresshold { get; set; }
    }
}
