using System;
using UnityEngine.Serialization;

namespace Model {
    internal class CommodityRecord {

        #region Public Properties

        public TradeInteraction.Commodity Commodity { get; private set; }
        public int Stock { get; set; }
        public float SupplyFactor { get; private set; }

        #endregion Public Properties

        #region Public Constructors

        public CommodityRecord(TradeInteraction.Commodity commodity, float supplyFactor) {
            this.Commodity = commodity;
            this.SupplyFactor = supplyFactor;
        }

        #endregion Public Constructors

        #region Public Methods

        public float GetMarketValue() {
            var stockScale = (float) Math.Exp(-Stock);
            float supplyScale = 1 / (2 + SupplyFactor);
            return Commodity.BaseValue * stockScale * supplyScale;
        }

        #endregion Public Methods

    }
}