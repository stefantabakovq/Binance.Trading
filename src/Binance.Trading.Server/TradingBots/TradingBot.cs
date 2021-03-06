using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Binance.Trading.Domain.Models;
using Binance.Trading.Domain.Services;
using Microsoft.Extensions.Logging;
using Binance.Net;

namespace Binance.Trading.Server.TradingBots
{
    public class TradingBot : TradingBotAbstraction, ITradingBot
    {
        protected readonly ILogger _logger;
        protected readonly BinanceClient _client;

        protected readonly int Leverage;

        public TradingBot(BinanceClient client, double balance, List<TradingStrategy> strategies, List<string> tickers, int leverage = 3)
        {
            _client = client;
            Tickers = tickers;
            (Balance, AvailableBalance) = (balance, balance);
            Strategies = strategies;
            Leverage = leverage;
        }

        public void AddBalance(double amount)
            => Balance += amount;

        public void AddStrategy(TradingStrategy strategy)
        {
            Strategies.Add(strategy);
        }

        public override string ToString()
            => $"{DateTime.Now} - - - {InstanceID} | BOT {Name} | BALANCE : {Balance}";
       

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
                if(Balance != 0)
                    await CycleEntries();
                if(Positions.Count != 0)
                    await CycleExits();
            }                   
        }

        private async Task CycleEntries()
        {
            Dictionary<string, List<TradeAction>> entries = new Dictionary<string, List<TradeAction>>();
            foreach (TradingStrategy strat in Strategies)
            {
                var _s = strat.GetSettings();
                foreach (var ticker in Tickers)
                {
                    // If we havent reached max positions and havent already opened a position for this ticker
                    if (Positions.Count < _s.MaxConcurrentPositions && !Positions.Where(x => x.Ticker == ticker).Any())
                    {
                        var entryDecision = await strat.TakeEntryDecision(ticker);
                        entries[ticker].Add(entryDecision);
                        // If we have enough entry positions for this ticker trigger entry
                        if (entries[ticker].Count >= _s.EntryTresshold)
                        {
                            var success = await TriggerEntry(ticker, entries[ticker]);
                            if (success)
                            {
                                entries[ticker] = new List<TradeAction>();
                            }
                        }
                    }
                }
            }
        }

        private async Task CycleExits()
        {
            Dictionary<string, List<TradeAction>> exits = new Dictionary<string, List<TradeAction>>();
            foreach (TradingStrategy strat in Strategies)
            {
                var _s = strat.GetSettings();
                foreach (var ticker in Tickers)
                {
                    // If we have opened a position for this ticker, check for exits
                    if (Positions.Where(x => x.Ticker == ticker).Any())
                    {
                        var exitDecision = await strat.TakeExitDecision(ticker);
                        exits[ticker].Add(exitDecision);
                        // If we have enough exit signals for this ticker trigger exit
                        if (exits[ticker].Count >= _s.ExitTresshold)
                        {
                            var success = await TriggerExit(ticker);
                            if (success) exits[ticker] = new List<TradeAction>();
                        }
                    }
                }
            }
        }

        private async Task<bool> TriggerExit(string ticker, List<TradeAction> exitSignals)
        {
            if (exitSignals.All(x => x.Action == TradeDecision.Hold)) return false;

            if(exitSignals.All(x => x.Action == exitSignals[0].Action))
            {
                var currentPosition = Positions.Find(x => x.Ticker == ticker);
                var closeOrderId = 
            }           
        }

        private async Task<bool> TriggerEntry(string ticker, List<TradeAction> entrySignals, double amount)
        {
            // If all signals uncertain, dont take entry
            if (entrySignals.All(x => x.Action == TradeDecision.Hold)) return false;

            await _client.FuturesUsdt.ChangeInitialLeverageAsync(ticker, Leverage);
            await _client.FuturesUsdt.ChangeMarginTypeAsync(ticker, Net.Enums.FuturesMarginType.Isolated);

            if (entrySignals.All(x => x.Action == TradeDecision.Long))
            {
                await Long(ticker, amount);
                return true;
            }

            if (entrySignals.All(x => x.Action == TradeDecision.Short))
            {
                await Short(ticker, amount);
                return true;
            }
            return false;
        }

        private async Task Long(string ticker, double amount)
        {
            var iD = await _client.FuturesUsdt.Order.PlaceOrderAsync(ticker, 
                Net.Enums.OrderSide.Buy,
                Net.Enums.OrderType.Market, 
                (decimal?)amount, 
                Net.Enums.PositionSide.Long);

            var pos = await _client.FuturesUsdt.GetPositionInformationAsync(ticker);
            Positions.Add(new TradingPosition(ticker, 0.01, PositionDirection.Long));
        }

        private async Task Short(string ticker, double amount)
        {
            var iD = await _client.FuturesUsdt.Order.PlaceOrderAsync(ticker, 
                Net.Enums.OrderSide.Sell, 
                Net.Enums.OrderType.Market, 
                (decimal?)amount, 
                Net.Enums.PositionSide.Short);

            var pos = await _client.FuturesUsdt.GetPositionInformationAsync(ticker);
            Positions.Add(new TradingPosition(ticker, 0.01, PositionDirection.Short));
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
