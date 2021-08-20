using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Binance.Trading.Domain.Models;

namespace Binance.Trading.Domain.Services
{
    public interface ITradingBot
    {
        bool Start();
        bool Stop();
        void AddBalance(double amount);
        void RemoveBalance(double amount);
        Task<string> ProduceReport(DateTime startTime, DateTime endTime);
        void AddStrategy(TradingStrategy strategy);
        void RemoveStrategy(string Name);
        Task<bool> LinkStrategies(TradingStrategy stratOne, TradingStrategy stratTwo);
        string GiveUpdate();
    }
}
