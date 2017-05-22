using UnityEngine;
using UnityEngine.Serialization;

namespace Controllers {
    public class CameraFollowPlayer : MonoBehaviour {

        #region Public Fields

        [FormerlySerializedAs("cameraFollowDelay")] public float CameraFollowDelay = 0.3f;
        [FormerlySerializedAs("cameraZoomFactor")] public float CameraZoomFactor = 0.5f;
        [FormerlySerializedAs("player")] public GameObject Player;

        #endregion Public Fields

        #region Private Fields

        [Component] private Camera _camera;
        private Vector3 _cameraVelocity = new Vector3(0, 0, 0);
        private bool _follow = true;

        #endregion Private Fields

        #region Internal Methods

        internal void LockToPosition(Vector2? pos) {
            LockToPosition(pos, 0f);
        }

        internal void LockToPosition(Vector2? pos, float zoom) {
            if (!pos.HasValue) {
                _follow = true;
            }
            else {
                _follow = false;
                transform.position = new Vector3(pos.Value.x, pos.Value.y, -10);
                _camera.orthographicSize = 5f - zoom;
            }
        }

        #endregion Internal Methods

        #region Private Methods

        // Update is called once per frame
        private void FixedUpdate() {
            if (!_follow) return;

            Vector3 target = Player.transform.position - new Vector3(0, 0, 10);
            transform.position = Vector3.SmoothDamp(transform.position, target, ref _cameraVelocity, CameraFollowDelay);
            _camera.orthographicSize = 5f + (_cameraVelocity.magnitude * CameraZoomFactor);
        }

        private void Start() {
            this.LoadComponents();
        }

        #endregion Private Methods

    }
}