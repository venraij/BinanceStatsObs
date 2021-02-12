using System;

namespace Model
{
    public class Asset
    {
        string name { get; set; }
        float walletBalance { get; set; }
        float unrealizedProfit { get; set; }
        float marginBalance { get; set; }
        float maintMargin { get; set; }
        float initialMargin { get; set; }
        float positionInitialMargin { get; set; }
        float openOrderInitialMargin { get; set; }
        float crossWalletBalance { get; set; }
        float crossUnPnl { get; set; }
        float availableBalance { get; set; }
        float maxWithdrawAmount { get; set; }
    }
}
