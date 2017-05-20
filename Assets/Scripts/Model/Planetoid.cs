using System;
using System.Collections.Generic;
using UnityEngine;

public class Planetoid : Orbital, Interactable {

    public struct PlanetType {
        public string name { get; private set; }
        public float weight { get; private set;  }
        public PlanetType(string name, float weight) {
            this.name = name;
            this.weight = weight;
        }
    }

    public static PlanetType[] types = {
        new PlanetType("Dwarf Planet", 1.5f),
        new PlanetType("Rocky Planet", 1f),
        new PlanetType("Ice Planet", 0.8f),
        new PlanetType("Ice Giant", 0.5f),
        new PlanetType("Gas Giant", 1.3f)
    };

    private static PlanetType[] typesNoIce = {
        new PlanetType("Dwarf Planet", 1.5f),
        new PlanetType("Rocky Planet", 1f),
        new PlanetType("Gas Giant", 1.3f)
    };

    public static String[] techlevels = {
        "Uninhabited",
        "Agricultural",
        "Industrial",
        "Technological"
    };

    private int techlevel;
    private GameObject prefab;
    private float rotation;
    private List<OrbitalInteraction> interactions;
    public PlanetType type;

    public Planetoid(int seed) : base(seed) { }

    new public bool Generate(int orbital) {
        if (!base.Generate(orbital)) return false;
        PlanetType[] validTypes = (orbital < 4) ? typesNoIce : types;
        var r = new System.Random(seed);
        double val = r.NextDouble();
        float div = 0f;
        foreach (PlanetType t in validTypes) {
            div += t.weight;
        }
        float total = 0f;
        type = validTypes[validTypes.Length-1];
        foreach (PlanetType t in validTypes) {
            total += t.weight / div;
            if (total > val) {
                type = t;
                break;
            }
        }
        techlevel = r.Next(techlevels.Length);

        interactions = new List<OrbitalInteraction>();
        InfoInteraction info = new InfoInteraction(this, seed);
        info.Generate(techlevel, orbital, type);
        interactions.Add(info);
        if (techlevel > 0) {
            TradeInteraction trade = new TradeInteraction(this, seed);
            trade.Generate(techlevel);
            //interactions.Add(trade);
        }
        prefab = Resources.Load("Prefabs/Planets/" + type.name) as GameObject;
        rotation = (int) (360 * r.NextDouble());
        return true;
    }

    public override void Load() {
        int x = (int) (radius * Mathf.Cos(angle));
        int y = (int)(radius * Mathf.Sin(angle));
        gameObject = GameObject.Instantiate(prefab, new Vector3(x, y, 0), Quaternion.Euler(0, 0, rotation));
        WorldPlanetoid wp = gameObject.GetComponent<WorldPlanetoid>();
        wp.Source = this;
    }

    public string GetInteractionText() {
        return "Press the interact key to land";
    }

    public void Interact(PlayerShipController player) {
        Debug.Log("Oh no you friccin moron, you just got INTERACTED! Tag your friend to totally INTERACT them.");
        InteractionUI window = GameObject.Instantiate(Resources.Load("Prefabs/UI/InteractionWindow") as GameObject).GetComponent<InteractionUI>();
        window.transform.SetParent(GameObject.Find("ScreenUI").transform, false);
        window.player = player;
        window.source = this;
        RectTransform t = (RectTransform) window.transform;
        t.offsetMax = Vector2.zero;
        foreach (OrbitalInteraction i in interactions) {
            window.AddTab(i);
        }
    }

    public new void Unload() {
        GameObject.Destroy(gameObject);
    }

}
