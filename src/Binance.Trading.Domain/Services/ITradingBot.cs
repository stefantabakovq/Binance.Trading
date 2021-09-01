using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Binance.Trading.Domain.Models;

namespace Binance.Trading.Domain.Services
{
    public interface ITradingBot
    {
        void AddBalance(double amount);
        void RemoveBalance(double amount);
        Task<string> ProduceReport(DateTime startTime, DateTime endTime);
        void AddStrategy(TradingStrategy strategy);
        void RemoveStrategy(string Name);
        public Task StartAsync(CancellationToken cancellationToken);
        public Task StopAsync(CancellationToken cancellationToken);
    }
}
