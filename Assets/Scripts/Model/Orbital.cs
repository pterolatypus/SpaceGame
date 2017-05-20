using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Orbital {
    
    protected float radius, angle;
    private bool isGenerated = false;
    protected int seed { get; private set; }
    private static float minRadius = 200, radiusRange = 100;
    public GameObject gameObject { get; protected set; }

    public Orbital(int seed) {
        this.seed = seed;
    }

    public bool Generate(int orbital) {
        if (isGenerated) return false;
        var r = new System.Random(seed);
        radius = ((orbital+1)*minRadius + (float) r.NextDouble()*radiusRange - radiusRange/2);
        angle = (float) r.NextDouble()*360f;
        isGenerated = true;
        return true;
    }

    public abstract void Load();

    public void Unload() {
        GameObject.Destroy(gameObject);
    }

}
