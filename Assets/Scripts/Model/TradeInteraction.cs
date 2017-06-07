using System;
using System.Collections.Generic;
using UI;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = System.Random;

namespace Model {
    [Serializable]
    internal partial class TradeInteraction : OrbitalInteraction {

        #region Private Fields

        private const float _maxSf = 1.2f;
        private const float _minSf = 0.8f;

        private static readonly List<Commodity> _tradeGoods = new List<Commodity> {
            new Commodity("Food", new[] {0f, 1f, -0.6f, -0.4f}, 10f),
            new Commodity("Water", new[] {0f, 1f, -0.6f, -0.4f}, 8f),
            new Commodity("Metal", new[] {0f, -0.7f, 1f, -0.3f}, 30f),
            new Commodity("Electronics", new[] {0f, -0.3f, -0.7f, 1f}, 80f)
        };

        private static readonly GameObject Prefab = (GameObject) Resources.Load("Prefabs/UI/TradeTab");
        private readonly Random _rand;

        private List<CommodityRecord> _records;

        #endregion Private Fields

        #region Internal Constructors

        internal TradeInteraction(Orbital o, int seed) : base(o) {
            _rand = new Random(seed);
            _records = new List<CommodityRecord>();
        }

        #endregion Internal Constructors

        #region Internal Methods

        internal void Generate(int techLevel) {
            foreach (Commodity commodity in _tradeGoods) {
                float sf = commodity.SupplyFactor[techLevel];
                var r = (float) _rand.NextDouble();
                sf = sf * (_minSf + r * (_maxSf - _minSf));
                var rec = new CommodityRecord(commodity, sf);
                _records.Add(rec);
            }
        }

        internal override InteractionTab GetTab() {
            var tab = Object.Instantiate(Prefab).GetComponent<TradeTab>();
            tab.Bind(this);
            return tab;
        }

        #endregion Internal Methods

    }
}