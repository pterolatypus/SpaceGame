using UnityEngine;
using UnityEngine.Serialization;

namespace Controllers {
    public class RadarObject : MonoBehaviour {

        #region Public Properties

        internal GameObject TrackedObject { get; set; }

        #endregion Public Properties

        #region Private Methods

        // Use this for initialization
        private void Start() {
        }

        // Update is called once per frame
        private void Update() {
            if (TrackedObject == null) return;

            Vector3 targetPos = TrackedObject.transform.position;
            Vector3 dir = targetPos - transform.position;
            transform.up = dir;
        }

        #endregion Private Methods

    }
}