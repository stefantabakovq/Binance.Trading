using System.Threading.Tasks;
using Binance.Trading.Domain.Models;

namespace Binance.Trading.Domain.Services
{
    public interface ITradingStrategy
    {
        Task<TradeAction> TakeEntryDecision(string tickerName);
        Task<TradeAction> TakeExitDecision(string tickerName);
    }
}
