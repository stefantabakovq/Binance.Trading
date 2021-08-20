using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Binance.Trading.Domain.Models;
using Binance.Trading.Domain.Services;

namespace Binance.Trading.Server.TradingBots
{
    public class TradingBot : TradingBotAbstraction, ITradingBot
    {
        public void AddBalance(double amount)
            => Balance += amount;

        public void AddStrategy(TradingStrategy strategy)
        {
            strategies.Add(strategy);
        }

        public string GiveUpdate()
            => $"{DateTime.Now} - - - {InstanceID} | BOT {Name} | BALANCE : {Balance}";

        public Task<bool> LinkStrategies(TradingStrategy stratOne, TradingStrategy stratTwo)
        {
            throw new NotImplementedException();
        }

        public Task<string> ProduceReport(DateTime startTime, DateTime endTime)
        {
            throw new NotImplementedException();
        }

        public void RemoveBalance(double amount)
            => Balance -= amount;

        public void RemoveStrategy(string Name)
        {
            strategies.Remove(strategies.Find(x => x.StrategyName == Name));
        }

        public bool Start()
        {
            throw new NotImplementedException();
        }

        public bool Stop()
        {
            throw new NotImplementedException();
        }
    }
}
