namespace Binance.Trading.Domain.Models
{
    public class GeneralSettings
    {
        public double MaxPosBalanceAsPerc { get; set; }
        public int MaxConcurrentPositions { get; set; }
        public double TakeProfitPerc { get; set; }
        public double StopLossPerc { get; set; }
        public int CooldownPeriodMinutes { get; set; }
        public int EntryTresshold { get; set; }
        public int ExitTresshold { get; set; }
    }
}
