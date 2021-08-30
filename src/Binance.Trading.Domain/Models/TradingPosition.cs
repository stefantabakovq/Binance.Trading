using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Trading.Domain.Models
{
    public class TradingPosition
    {
        public string Ticker { get; set; }
        public double CurrentSize { get; set; }
        public double StartSize { get; set; }
        public bool Long { get; set; }
        public PositionDirection Direction { get; set; }
        
        public TradingPosition(string ticker, double size, PositionDirection direction)
        {
            Ticker = ticker;
            Direction = direction;
            (CurrentSize, StartSize) = (size, size);
        }

        public SizeChange GetChange()
            => new SizeChange() { AbsoluteChange = CurrentSize - StartSize, 
                                  PercentageChange = (CurrentSize - StartSize) / (StartSize * 100) };
    }

    public enum PositionDirection : int
    {
        Long = 0, 
        Short = 1
    }
}
