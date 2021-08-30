using System;

namespace Binance.Trading.Domain.Models
{
    public class TradeAction
    {
        public TradeType type { get; set; }
        public TradeDecision action { get; set; }
        public DateTime timeTaken { get; set; }
        public DateTime timePlaced { get; set; }
    }

    public enum TradeType : uint
    {
        Spot = 0,
        Margin = 1,
        Futures = 2 
    }

    public enum TradeDecision : uint
    {
        Hold = 0,
        MarketBuy = 1,
        LimitBuy = 2,
        MarketSell = 3,
        LimitSell = 4
    }
}
