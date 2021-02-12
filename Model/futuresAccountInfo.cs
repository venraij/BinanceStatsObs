using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    class futuresAccountInfo
    {
        int feeTier { get; set; }
        bool canTrade { get; set; }
        bool canDeposit { get; set; }
        bool canWithdraw { get; set; }
        int updateTime { get; set; }
        float totalInitialMargin { get; set; }
        float totalMaintMargin { get; set; }
        float totalWalletBalance { get; set; }
        float totalUnrealizedProfit { get; set; }
        float totalMarginBalance { get; set; }
        float totalPositionInitialMargin { get; set; }
        float totalOpenOrderInitialMargin { get; set; }
        float totalCrossWalletBalance { get; set; }
        float totalCrossUnPnl { get; set; }
        float availableBalance { get; set; }
        float maxWithdrawAmount { get; set; }
        List<Asset> assets { get; set; }
        List<Position> positions { get; set; }
    }
}
