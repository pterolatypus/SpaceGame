using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSystem : IEnumerable, IEnumerator {

    static readonly String[] sizes = {
        "Dwarf",
        "Star",
        "Giant",
        "Supergiant"
    };

    static readonly String[] temps = {
        "Red",
        "Yellow",
        "White",
        "Blue"
    };

    public Vector2 coords { get; private set; }
    public int seed { get; private set; }
    public String type {
        get { return temps[temp] + " " + sizes[size]; }
    }

    private int enumeratorPosition = -1;
    public GameObject Current {
        get {
            if (enumeratorPosition < 0) return null;
            if (enumeratorPosition > satellites.Count) throw new IndexOutOfRangeException();
            if (enumeratorPosition == 0) return gameObject;
            return satellites[enumeratorPosition - 1].gameObject;
        }
    }

    object IEnumerator.Current {
        get {
            if (enumeratorPosition < 0) return null;
            if (enumeratorPosition > satellites.Count) throw new IndexOutOfRangeException();
            if (enumeratorPosition == 0) return gameObject;
            return satellites[enumeratorPosition - 1].gameObject;
        }
    }

    private bool isGenerated = false;
    private int temp, size;
    private List<Orbital> satellites = new List<Orbital>();
    private GameObject starPrefab;
    private int rotation;
    private GameObject gameObject;


    public StarSystem(Vector2 coords, int seed) {
        this.coords = coords;
        this.seed = seed;
    }

    private bool Generate() {
        if (isGenerated) return false;
        var rand = new System.Random(seed);
        temp = rand.Next(temps.Length);
        size = rand.Next(sizes.Length);
        int numOrbits = 5 * (size + 1);
        for (int i = 1; i <= numOrbits; i++) {
            double r = rand.NextDouble();
            if (r < 0.5) {
                Planetoid p = new Planetoid(rand.Next());
                satellites.Add(p);
                p.Generate(i);
            } else {
                satellites.Add(new EmptyOrbital(0));
            }
        }
        starPrefab = Resources.Load("Prefabs/Stars/" + temps[temp]) as GameObject;
        rotation = (int) (360 * rand.NextDouble());
        return true;
    }

    public void Load() {
        Debug.Log("Loading system");
        this.Generate();
        //instantiate all the game objects
        gameObject = GameObject.Instantiate(starPrefab, new Vector2 (0,0), Quaternion.Euler(0,0, rotation));
        var scale = Vector3.one * (size+2)*(size+2);
        gameObject.transform.localScale = scale;
        foreach (Orbital obj in satellites) {
            obj.Load();
        }
    }

    public void Unload() {
        GameObject.Destroy(gameObject);
        foreach (Orbital obj in satellites) {
            obj.Unload();
            GameObject.Destroy(obj.gameObject);
        }
        foreach (WorldPlanetoid p in GameObject.FindObjectsOfType<WorldPlanetoid>()) {
            GameObject.Destroy(p.gameObject);
        }
    }

    public IEnumerator GetEnumerator() {
        return (IEnumerator)this;
    }

    public bool MoveNext() {
        if (enumeratorPosition > satellites.Count) return false;
        enumeratorPosition++;
        return (enumeratorPosition <= satellites.Count);
    }

    public void Reset() {
        enumeratorPosition = -1;
    }

    public void Dispose() { }
}