using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoInteraction : OrbitalInteraction {

    private static GameObject prefab = Resources.Load("Prefabs/UI/InfoTab") as GameObject;
    private System.Random rand;
    public string text { get; private set; }

    public InfoInteraction(Orbital obj, int seed) : base(obj) {
        rand = new System.Random(seed);
    }

    public override InteractionTab GetTab() {
        InfoTab tab = GameObject.Instantiate(prefab).GetComponent<InfoTab>();
        tab.Bind(this);
        return tab;
    }

    public void Generate(int techlevel, int orbital, Planetoid.PlanetType type) {

        string strtype = "Planet type: " + type.name;
        AddLine(strtype);

        string techl = "Tech Level: " + Planetoid.techlevels[techlevel];
        AddLine(techl);

        float pop = (float) (rand.NextDouble() + 0.5);
        pop *= techlevel * 2;
        string population;
        if (pop > 1) {
            population = Math.Round(pop, 2) + " billion";
        } else if (pop > 0) {
            population = Math.Round(pop * 1000, 2) + " million";
        } else {
            population = "Zero";
        }
        population = "Population: " + population;
        AddLine(population);

        //AddLine("Orbital shell: " + orbital);

        float temp = rand.Next(193, 213);
        temp *= (float) Math.Sqrt(3f/orbital);
        temp -= 173;
        string temperature = "Average temperature: " + Math.Round(temp, 0) + " degrees Celsius";
        AddLine(temperature);

        float orbitPeriod = (float) (rand.NextDouble() - 0.5);
        orbitPeriod = 1 + (orbitPeriod / 5f);
        orbitPeriod *= (orbital / 3f);
        string period = "Orbital period: " + Math.Round(orbitPeriod, 2) + " Earth years";
        AddLine(period);
    }

    public void AddLine(string line) {
        text += line;
        text += Environment.NewLine;
    }
}
