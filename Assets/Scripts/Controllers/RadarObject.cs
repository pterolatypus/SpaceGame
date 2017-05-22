using UnityEngine;

namespace Controllers {
	public class RadarObject : MonoBehaviour {

		public GameObject trackedObject { get; set; }

		// Use this for initialization
		void Start () {
		
		}
	
		// Update is called once per frame
		void Update () {
			if (trackedObject != null) {
				Vector3 targetPos = trackedObject.transform.position;
				Vector3 dir = targetPos - transform.position;
				transform.up = dir;
			}
		}
	}
}
