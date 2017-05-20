using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeInteraction : OrbitalInteraction {

    private System.Random rand;

    public struct Commodity {
        public string name { get; private set; }
        public float[] supplyFactor { get; private set; }
        public float baseValue { get; private set; }

        public Commodity(string name, float[] supplyFactor, float baseValue) {
            this.name = name;
            this.supplyFactor = supplyFactor;
            this.baseValue = baseValue;
        }
    }

    static Commodity[] tradeGoods = {
        new Commodity("Food", new float[] {0f, 1f, -0.6f, -0.4f}, 10f),
        new Commodity("Water", new float[] {0f, 1f, -0.6f, -0.4f}, 8f),
        new Commodity("Metal", new float[] {0f, -0.7f, 1f, -0.3f}, 30f),
        new Commodity("Electronics", new float[] {0f, -0.3f, -0.7f, 1f}, 80f)
    };

    private float minSF = 0.8f, maxSF = 1.2f;
    private List<CommodityRecord> records;
    private static GameObject prefab = Resources.Load("Prefabs/UI/TradeTab") as GameObject;

    public TradeInteraction(Orbital o, int seed) : base(o) {
        this.rand = new System.Random(seed);
        records = new List<CommodityRecord>();
    }

    public void Generate(int techlevel) {
        foreach (Commodity commodity in tradeGoods) {
            float sf = commodity.supplyFactor[techlevel];
            float r = (float) rand.NextDouble();
            sf = sf * (minSF + r * (maxSF - minSF));
            CommodityRecord rec = new CommodityRecord(commodity, sf);
            records.Add(rec);
        }
    }

    public override InteractionTab GetTab() {
        TradeTab tab = GameObject.Instantiate(prefab).GetComponent<TradeTab>();
        tab.Bind(this);
        return tab;
    }

}
