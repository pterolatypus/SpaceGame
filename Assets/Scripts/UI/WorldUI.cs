using System.Collections;
using System.Collections.Generic;
using Controllers;
using UnityEngine;

public class WorldUI : MonoBehaviour {

	public GameObject player;
	public GameObject pipPrefab;
	private Dictionary<GameObject, RadarObject> dictionary = new Dictionary<GameObject, RadarObject>();

	// Use this for initialization
	void Start() {

	}

	// Update is called once per frame
	void Update() {
		this.transform.position = player.transform.position;
	}

	public void AddTrackingObject(GameObject obj) {
		RadarObject pip = GameObject.Instantiate(pipPrefab, this.transform).GetComponent<RadarObject>();
		pip.trackedObject = obj;
		dictionary.Add(obj, pip);
	}

	public void RemoveTrackingObject(GameObject obj) {
		dictionary.Remove(obj);
	}

	public void Clear() {
		dictionary.Clear();
	}
}