using UnityEngine;
using Random = System.Random;

public class GameController : MonoBehaviour {
    public int numberOfSystems = 100;
    public WorldUI radar;
    private Galaxy g;
    private StarSystem current;

    [SerializeField] private PlayerShipController player;

    // Use this for initialization
    private void Awake() {
        g = new Galaxy(numberOfSystems);
        LoadSystem(g.home);
    }

    // Update is called once per frame
    private void Update() {
    }

    private void LoadSystem(StarSystem star) {
        if (current != null)
        {
            current.Unload();
            radar.Clear();
        }

        current = star;
        current.Load();

        foreach (var obj in current)
        {
            if (obj != null) radar.AddTrackingObject(obj as GameObject);
        }

        float angle = (float) new Random().NextDouble() * 360;
        var radius = 200;

        var x = (int) (radius * Mathf.Cos(angle));
        var y = (int) (radius * Mathf.Sin(angle));

        player.transform.position = new Vector3(x, y, 0);
    }

    public void Relocate() {
        LoadSystem(g.GetRandomStar());
    }
}