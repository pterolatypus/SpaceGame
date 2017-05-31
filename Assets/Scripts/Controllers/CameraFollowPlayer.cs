using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Controllers {
    [UsedImplicitly]
    internal class CameraFollowPlayer : MonoBehaviour {

        #region Private Fields

        [Component] private Camera _camera;
        [SerializeField] private GameObject _player;
        [SerializeField] private readonly float _cameraFollowDelay = 0.3f;
        [SerializeField] private readonly float _cameraZoomFactor = 0.5f;
        private Vector3 _cameraVelocity = new Vector3(0, 0, 0);
        private bool _cameraVelocityZoom = true;
        private bool _isFollowing = true;
        private const float CameraMinimumZoom = 5.0f;
        private const float CameraZOffset = 10.0f;
        private GameObject _targetObject;

        #endregion Private Fields

        #region Private Properties

        private Vector3 Position {
            get { return transform.position; }
            set { transform.position = value; }
        }

        private Vector3 TargetPosition {
            get { return _targetObject.transform.position; }
        }

        private float CameraSize {
            set { _camera.orthographicSize = value; }
        }

        #endregion Private Properties

        #region Internal Methods

        internal void LockToPosition(Vector2 targetPosition, float zoom = 0f) {
            _isFollowing = false;
            Position = new Vector3(targetPosition.x, targetPosition.y, CameraZOffset);
            CameraSize = CameraMinimumZoom - zoom;
        }

        internal void FollowTarget(GameObject target) {
            _isFollowing = true;
            if (target == null) target = _player;
            _targetObject = target;
        }

        #endregion Internal Methods

        #region Private Methods


        [UsedImplicitly]
        private void Start() {
            this.LoadComponents();
            FollowTarget(_player);
        }

        [UsedImplicitly]
        private void FixedUpdate() {
            if (_isFollowing) MoveToPlayerSmoothly();
            if (_cameraVelocityZoom) UpdateCameraZoom();
        }

        private void MoveToPlayerSmoothly() {
            Vector3 target = GetTargetCameraPosition();
            Position = Vector3.SmoothDamp(Position, target, ref _cameraVelocity, _cameraFollowDelay);
        }

        private Vector3 GetTargetCameraPosition() {
            return TargetPosition - new Vector3(0, 0, CameraZOffset);
        }

        private void UpdateCameraZoom() {
            CameraSize = CameraMinimumZoom + (_cameraVelocity.magnitude * _cameraZoomFactor);
        }

        #endregion Private Methods

    }
}