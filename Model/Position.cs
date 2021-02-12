using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Position
    {
        string symbol { get; set; }
        float initialMargin { get; set; }
        float maintMargin { get; set; }
        float unrealizedProfit { get; set; }
        float positionInitialMargin { get; set; }
        float openOrderInitialMargin { get; set; }
        float leverage { get; set; }
        bool isolated { get; set; }
        float entryPrice { get; set; }
        float maxNotional { get; set; }
        float positionSide { get; set; }
        float positionAmt { get; set; }
    }
}
