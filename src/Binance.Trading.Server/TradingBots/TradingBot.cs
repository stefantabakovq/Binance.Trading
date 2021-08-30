using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Binance.Trading.Domain.Models;
using Binance.Trading.Domain.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Binance.Trading.Server.TradingBots
{
    public class TradingBot : TradingBotAbstraction, ITradingBot
    {
        protected readonly ILogger _logger;
        protected readonly IPriceUpdater _priceUpdater;

        public TradingBot(ILogger<TradingBot> logger, IPriceUpdater priceUpdater, double balance, List<TradingStrategy> strategies, List<string> tickers)
        {
            _logger = logger;
            Tickers = tickers;
            Balance = balance;
            Strategies = strategies;
        }

        public void AddBalance(double amount)
            => Balance += amount;

        public void AddStrategy(TradingStrategy strategy)
        {
            Strategies.Add(strategy);
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
            Strategies.Remove(Strategies.Find(x => x.StrategyName == Name));
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                Dictionary<string, List<TradeAction>> entries = new Dictionary<string, List<TradeAction>>();
                Dictionary<string, List<TradeAction>> exits = new Dictionary<string, List<TradeAction>>();

                foreach (TradingStrategy strat in Strategies)
                {
                    var _s = strat.GetSettings();
                    foreach(var ticker in Tickers)
                    {
                        // If we havent reached max positions and havent already opened a position for this ticker
                        if (Positions.Count < _s.MaxConcurrentPositions && !Positions.Where(x => x.Ticker == ticker).Any())
                        {
                            var entryDecision = await strat.TakeEntryDecision(ticker);
                            entries[ticker].Add(entryDecision);
                            // If we have enough entry positions for this ticker trigger entry
                            if (entries[ticker].Count >= _s.EntryTresshold)
                            {
                                await TriggerEntry(ticker);
                                entries[ticker] = new List<TradeAction>();
                            }
                        }

                        // If we have opened a position for this ticker, check for exits
                        if(Positions.Where(x => x.Ticker == ticker).Any())
                        {
                            var exitDecision = await strat.TakeExitDecision(ticker);
                            exits[ticker].Add(exitDecision);
                            // If we have enough exit signals for this ticker trigger exit
                            if (exits[ticker].Count >= _s.ExitTresshold)
                            {
                                await TriggerExit(ticker);
                                exits[ticker] = new List<TradeAction>();
                            }
                        }
                    }                   
                }
            }
        }

        public async Task TriggerExit(string ticker)
        {

        }

        public async Task TriggerEntry(string ticker)
        {

        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
