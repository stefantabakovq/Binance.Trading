using System;

namespace Binance.Trading.Domain.Models
{
    public class TradeAction
    {
        public TradeType Type { get; set; }
        public TradeDecision Action { get; set; }
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
        Long = 1,
        Short = 3,
    }
}
