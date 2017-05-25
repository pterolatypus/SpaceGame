using System.Collections.Generic;
using Controllers;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI {
    public class WorldUI : MonoBehaviour {

        #region Public Fields

        [FormerlySerializedAs("pipPrefab")] public GameObject PipPrefab;
        [FormerlySerializedAs("player")] public GameObject Player;

        #endregion Public Fields

        #region Private Fields

        private Dictionary<GameObject, RadarObject> _dictionary;

        #endregion Private Fields

        #region Public Methods

        public void RemoveTrackingObject(GameObject obj) {
            _dictionary.Remove(obj);
        }

        #endregion Public Methods

        #region Internal Methods

        internal void AddTrackingObject(GameObject obj) {
            var pip = Instantiate(PipPrefab, transform).GetComponent<RadarObject>();
            pip.TrackedObject = obj;
            _dictionary.Add(obj, pip);
        }

        internal void Clear() {
            _dictionary.Clear();
        }

        #endregion Internal Methods

        #region Private Methods

        // Use this for initialization
        private void Awake() {
            _dictionary = new Dictionary<GameObject, RadarObject>();
        }

        // Update is called once per frame
        private void Update() {
            transform.position = Player.transform.position;
        }

        #endregion Private Methods

    }
}