using Model;
using UI;
using UnityEngine;
using Random = System.Random;

namespace Controllers {
    public class GameController : MonoBehaviour {

        #region Public Fields

        public int NumberOfSystems = 100;
        public WorldUI Radar;

        #endregion Public Fields

        #region Private Fields

        private StarSystem _currentStarSystem;
        private Galaxy _galaxy;
        [SerializeField] private PlayerShipController _player;

        #endregion Private Fields

        #region Public Methods

        public void Relocate() {
            LoadSystem(_galaxy.GetRandomStar());
        }

        #endregion Public Methods

        #region Private Methods

        // Use this for initialization
        private void Awake() {
            _galaxy = new Galaxy(NumberOfSystems);
            LoadSystem(_galaxy.Home);
        }

        private void LoadSystem(StarSystem star) {
            if (_currentStarSystem != null) {
                _currentStarSystem.Unload();
                Radar.Clear();
            }

            _currentStarSystem = star;
            _currentStarSystem.Load();

            foreach (GameObject obj in _currentStarSystem) {
                Radar.AddTrackingObject(obj);
            }

            float angle = (float) new Random().NextDouble() * 360;
            const int radius = 200;

            var x = (int) (radius * Mathf.Cos(angle));
            var y = (int) (radius * Mathf.Sin(angle));

            _player.transform.position = new Vector3(x, y, 0);
        }

        // Update is called once per frame
        private void Update() {
        }

        #endregion Private Methods

    }
}