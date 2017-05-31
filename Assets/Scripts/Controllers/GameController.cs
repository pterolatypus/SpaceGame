using JetBrains.Annotations;
using Model;
using UI;
using UnityEngine;
using Random = System.Random;

namespace Controllers {
    public class GameController : MonoBehaviour {

        #region Private Fields

        private StarSystem _currentStarSystem;
        private Galaxy _galaxy;

        [SerializeField] private readonly int _numberOfSystems = 100;
        [SerializeField] private WorldUI _radar;
        [SerializeField] private PlayerShipController _player;

        #endregion Private Fields

        #region Private Methods

        [UsedImplicitly]
        private void Awake() {
            _galaxy = new Galaxy(_numberOfSystems);
        }

        [UsedImplicitly]
        private void Start() {
            TravelToStarSystem(_galaxy.Home);
        }

        private void TravelToStarSystem(StarSystem star) {
            UnloadCurrentSystem();
            LoadNewSystem(star);
            RefreshRadar();
            RandomisePlayerPosition();
        }

        private void UnloadCurrentSystem() {
            if (_currentStarSystem != null) _currentStarSystem.Unload();
        }

        private void LoadNewSystem(StarSystem star) {
            _currentStarSystem = star;
            _currentStarSystem.Load();
        }

        private void RefreshRadar() {
            _radar.Clear();

            foreach (GameObject obj in _currentStarSystem) {
                _radar.AddTrackingObject(obj);
            }
        }

        private void RandomisePlayerPosition() {
            Vector3 newPosition = GetRandomPositionAtRadius(200f);
            _player.transform.position = newPosition;
        }

        private static Vector3 GetRandomPositionAtRadius(float radius) {
            float angle = (float) new Random().NextDouble() * 360;

            var x = (int) (radius * Mathf.Cos(angle));
            var y = (int) (radius * Mathf.Sin(angle));

            return new Vector3(x, y, 0);
        }

        #endregion Private Methods

    }
}