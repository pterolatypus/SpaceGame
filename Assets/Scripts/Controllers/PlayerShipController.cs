using System;
using JetBrains.Annotations;
using Model;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Controllers {
    public class PlayerShipController : MonoBehaviour {

        #region Private Fields

        private const float AnimSpeedMax = 2f;
        private const float AnimSpeedMin = 0.5f;
        private IInteractable _interactionTarget;
        private float _greatestObservedSpeed = 1f;
        [Component] private Animator _anim;
        [Component]private Rigidbody2D _rb;
        [SerializeField] private Text _txtInteract;
        [SerializeField] private float _boostFactor = 4;
        [SerializeField] private float _thrust = 1f;

        #endregion Private Fields

        #region Internal Properties

        internal bool ControlsEnabled { get; set; }

        #endregion Internal Properties

        #region Private Properties

        private bool HasInteractionTarget {
            get { return (_interactionTarget != null); }
        }

        #endregion Private Properties

        #region Private Methods

        [UsedImplicitly]
        private void Start() {
            this.LoadComponents();
            ControlsEnabled = true;
        }

        [UsedImplicitly]
        private void Update() {
            if (ControlsEnabled) TurnToFaceCursor();
            if (Input.GetButtonDown("Interact") && ControlsEnabled && HasInteractionTarget) _interactionTarget.Interact(this);
        }

        private void TurnToFaceCursor() {
            transform.up = GetDirectionToCursor();
        }

        private Vector3 GetDirectionToCursor() {
            return GetAdjustedCursorPosition() - transform.position;
        }

        private static Vector3 GetAdjustedCursorPosition() {
            Vector3 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            cursorPos.z = 0;
            return cursorPos;
        }

        [UsedImplicitly]
        private void FixedUpdate() {
            HandleThrustControl();
            UpdateAnimation();
        }

        private void HandleThrustControl() {
            float thrust = Input.GetButton("Boost") ? _thrust * _boostFactor : _thrust;
            if (Input.GetButton("Forward") && ControlsEnabled) _rb.AddForce(thrust * transform.up);
        }

        private void UpdateAnimation() {
            bool isAnimated = ControlsEnabled && Input.GetButton("Forward");
            _anim.SetBool("shipIsMoving", isAnimated);
            if (isAnimated) UpdateAnimationSpeed();
        }

        private void UpdateAnimationSpeed() {
            float currentSpeed = _rb.velocity.magnitude;
            _greatestObservedSpeed = Mathf.Max(currentSpeed, _greatestObservedSpeed);
            float animSpeed = AnimSpeedMin + (currentSpeed / _greatestObservedSpeed * (AnimSpeedMax - AnimSpeedMin));
            _anim.SetFloat("animSpeed", animSpeed);
        }

        [UsedImplicitly]
        private void OnTriggerEnter2D([NotNull] Collider2D other) {
            var interactableSource = other.gameObject.GetComponent<WorldOrbital>().Source as IInteractable;
            if (interactableSource != null) SetInteractionTarget(interactableSource);
        }

        [UsedImplicitly]
        private void OnTriggerExit2D([NotNull] Collider2D other) {
            var interactableSource = other.gameObject.GetComponent<WorldOrbital>().Source as IInteractable;
            if (interactableSource != null) SetInteractionTarget(null);
        }

        private void SetInteractionTarget([CanBeNull] IInteractable interactable) {
            _interactionTarget = interactable;
            UpdateInteractionText();
        }

        private void UpdateInteractionText() {
            if (HasInteractionTarget) _txtInteract.text = _interactionTarget.GetInteractionText();
            _txtInteract.gameObject.SetActive(HasInteractionTarget);
        }

        #endregion Private Methods

    }
}