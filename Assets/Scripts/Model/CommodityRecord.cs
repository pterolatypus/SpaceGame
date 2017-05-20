using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommodityRecord {

    public float supplyFactor { get; private set; }
    public TradeInteraction.Commodity commodity { get; private set; }

    public int stock { get; set; }

    public CommodityRecord(TradeInteraction.Commodity commodity, float supplyFactor) {
        this.commodity = commodity;
        this.supplyFactor = supplyFactor;
    }

    public float getMarketValue() {
        float stockScale = (float) Math.Exp(-stock);
        float supplyScale = 1/ (2 + supplyFactor);
        return commodity.baseValue * stockScale * supplyScale;
    }

}
