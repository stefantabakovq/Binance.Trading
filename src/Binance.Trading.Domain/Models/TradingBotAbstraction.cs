using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Binance.Trading.Domain.Services;

namespace Binance.Trading.Domain.Models
{
    public abstract class TradingBotAbstraction
    {
        public string Name { get; set; }
        public double Balance { get; set; }
        public string InstanceID { get; set; }
        public double MaxPositionSize { get; set; }
        public double MinPositionSize { get; set; }

        public List<TradingStrategy> strategies { get; set; }
        public List<Dictionary<DateTime, string>> OrderLog { get; set; }
        public List<Dictionary<DateTime, string>> PositionLog { get; set; }
        public List<Dictionary<DateTime, double>> BalanceLog { get; set; }
        public List<Dictionary<DateTime, List<TradingStrategy>>> StrategyLog {get;set;}      
    }
}
