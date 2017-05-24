using JetBrains.Annotations;
using Model;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Controllers {
    public class PlayerShipController : MonoBehaviour {

        #region Public Fields

        [FormerlySerializedAs("boostFactor")] public float BoostFactor = 4;
        [FormerlySerializedAs("thrust")] public float Thrust = 1f;

        #endregion Public Fields

        #region Private Fields

        private const float AnimSpeedMax = 2f;
        private const float AnimSpeedMin = 0.5f;
        [Component]private Animator _anim;
        private bool _controls = true;
        private IInteractable _interactionTarget;
        private float _maxSpeed = 1f;
        [Component]private Rigidbody2D _rb;
        [FormerlySerializedAs("txtInteract")] [SerializeField] private Text _txtInteract;

        #endregion Private Fields

        #region Public Methods

        internal void EnableControls(bool enabled) {
            _controls = enabled;
        }

        #endregion Public Methods

        #region Private Methods

        private void FixedUpdate() {
            _anim.SetBool("shipIsMoving", Input.GetButton("Forward") && _controls);
            float thrust = Input.GetButton("Boost") ? Thrust * BoostFactor : Thrust;
            if (Input.GetButton("Forward") && _controls) {
                _rb.AddForce(thrust * transform.up);
            }

            float curSpd = GetComponent<Rigidbody2D>().velocity.magnitude;
            _maxSpeed = Mathf.Max(curSpd, _maxSpeed);
            float animSpeed = AnimSpeedMin + (curSpd / _maxSpeed * (AnimSpeedMax - AnimSpeedMin));
            _anim.SetFloat("animSpeed", animSpeed);

            if (Input.GetButtonDown("Relocate") && _controls) {
                //GameObject.Find("GameController").GetComponent<GameController>().Relocate();
            }
        }

        private void OnTriggerEnter2D(Collider2D other) {
            var collisionOrbital = other.gameObject.GetComponent<WorldOrbital>();
            Orbital orbitalSource = collisionOrbital.Source;
            var interactableSource = orbitalSource as IInteractable;

            if (interactableSource != null) {
                SetInteractionTarget(interactableSource);
            }
        }

        private void OnTriggerExit2D(Collider2D other) {
            var collisionOrbital = other.gameObject.GetComponent<WorldOrbital>();
            Orbital orbitalSource = collisionOrbital.Source;
            var interactableSource = orbitalSource as IInteractable;

            if (interactableSource != null) {
                SetInteractionTarget(null);
            }
        }

        private void SetInteractionTarget([CanBeNull] IInteractable interactable) {
            _interactionTarget = interactable;
            if (_interactionTarget == null) {
                _txtInteract.gameObject.SetActive(false);
            }
            else {
                _txtInteract.text = _interactionTarget.GetInteractionText();
                _txtInteract.gameObject.SetActive(true);
            }
        }

        // Use this for initialization
        private void Start() {
           this.LoadComponents();
        }

        // Update is called once per frame
        private void Update() {
            Vector3 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            cursorPos.z = 0;
            Vector3 dir = cursorPos - transform.position;
            if (_controls) {
                transform.up = dir;
            }
            if (Input.GetButtonDown("Interact") && _controls && _interactionTarget != null) {
                _interactionTarget.Interact(this);
            }
        }

        #endregion Private Methods

    }
}