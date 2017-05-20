using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public int numberOfSystems = 100;
    public WorldUI radar;
    Galaxy g;
    StarSystem current;
    [SerializeField]
    private PlayerShipController player;
	// Use this for initialization
	void Awake () {
        g = new Galaxy(numberOfSystems);
        LoadSystem(g.home);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void LoadSystem(StarSystem star) {

        if (current != null) {
            current.Unload();
            radar.Clear();
        }

        current = star;
        current.Load();
        foreach (GameObject obj in current) {
            if (obj == null) continue;
            radar.AddTrackingObject(obj);
        }

        float angle = (float) (new System.Random().NextDouble()) * 360;
        int radius = 200;

        int x = (int)(radius * Mathf.Cos(angle));
        int y = (int)(radius * Mathf.Sin(angle));

        player.transform.position = new Vector3(x, y, 0);
    }

    public void Relocate() {
        LoadSystem(g.GetRandomStar());
    }

}
