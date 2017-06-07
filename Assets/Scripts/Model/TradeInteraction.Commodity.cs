using System;

namespace Model {
    internal partial class TradeInteraction {

        #region Internal Structs

        [Serializable]
        internal struct Commodity {

            #region Public Properties

            public float BaseValue { get; private set; }
            public string Name { get; private set; }
            public float[] SupplyFactor { get; private set; }

            #endregion Public Properties

            #region Public Constructors

            public Commodity(string name, float[] supplyFactor, float baseValue) : this() {
                Name = name;
                SupplyFactor = supplyFactor;
                BaseValue = baseValue;
            }

            #endregion Public Constructors

        }

        #endregion Internal Structs

    }
}