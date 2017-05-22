using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace Model
{
    public partial class TradeInteraction : OrbitalInteraction {

        #region Private Fields

        private static readonly GameObject Prefab = (GameObject) Resources.Load("Prefabs/UI/TradeTab");

        private static readonly Commodity[] _tradeGoods = {
            new Commodity("Food", new[] {0f, 1f, -0.6f, -0.4f}, 10f),
            new Commodity("Water", new[] {0f, 1f, -0.6f, -0.4f}, 8f),
            new Commodity("Metal", new[] {0f, -0.7f, 1f, -0.3f}, 30f),
            new Commodity("Electronics", new[] {0f, -0.3f, -0.7f, 1f}, 80f)
        };

        private readonly float _minSf = 0.8f;
        private readonly float _maxSf = 1.2f;
        private readonly Random _rand;

        private List<CommodityRecord> _records;

        #endregion Private Fields

        #region Public Constructors

        internal TradeInteraction(Orbital o, int seed) : base(o) {
            _rand = new Random(seed);
            _records = new List<CommodityRecord>();
        }

        #endregion Public Constructors

        #region Public Methods

        internal void Generate(int techLevel) {
            foreach (Commodity commodity in _tradeGoods) {
                float sf = commodity.SupplyFactor[techLevel];
                var r = (float) _rand.NextDouble();
                sf = sf * (_minSf + r * (_maxSf - _minSf));
                var rec = new CommodityRecord(commodity, sf);
                _records.Add(rec);
            }
        }

        #endregion Public Methods

        #region Internal Methods

        internal override InteractionTab GetTab() {
            var tab = Object.Instantiate(Prefab).GetComponent<TradeTab>();
            tab.Bind(this);
            return tab;
        }

        #endregion Public Structs

    }
}