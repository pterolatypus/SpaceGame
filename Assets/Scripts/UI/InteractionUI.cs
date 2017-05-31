using System.Collections.Generic;

using Controllers;

using Model;

using UnityEngine;
using UnityEngine.Serialization;

namespace UI {
    public class InteractionUI : MonoBehaviour {

        #region Private Fields

        private static GameObject _button;

        [Component("Main Camera")]
        private CameraFollowPlayer _playerCamera;

        [FormerlySerializedAs("tabButtonHolder")]
        [SerializeField]
        private GameObject _tabButtonHolder;

        private List<InteractionTab> _tabs;

        private GameObject _worldUI;

        #endregion Private Fields

        #region Internal Properties

        internal PlayerShipController Player { get; set; }

        internal Orbital Source { get; set; }

        #endregion Internal Properties

        #region Public Methods

        public void Awake() {
            if (_button == null) {
                _button = (GameObject)Resources.Load("Prefabs/UI/TabButton");
            }

            _tabs = new List<InteractionTab>();
            this.LoadComponents();
        }

        public void Start() {
            Player.EnableControls(false);
            Vector3 planetWorldPos = Source.GameObject.transform.position;

            _playerCamera.LockToPosition(planetWorldPos);
            Vector3 planetPos = Camera.main.WorldToScreenPoint(planetWorldPos);
            planetPos.x += 170;
            Vector3 lockPos = Camera.main.ScreenToWorldPoint(planetPos);

            _playerCamera.LockToPosition(lockPos, 2f);
            _worldUI = GameObject.Find("InWorldUI");
            _worldUI.SetActive(false);

            Player.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }

        public void Update() {
            if (Input.GetButtonDown("Cancel")) {
                Close();
            }
        }

        #endregion Public Methods

        #region Internal Methods

        internal void AddTab(OrbitalInteraction interaction) {
            InteractionTab tab = interaction.GetTab();
            _tabs.Add(tab);

            if (_tabs.Count > 1) {
                tab.gameObject.SetActive(false);
            }

            var t = (RectTransform)tab.transform;
            t.SetParent(transform, false);

            var button = Instantiate(_button).GetComponent<TabButton>();
            button.Bind(this, tab);
            button.Text = tab.GetTitle();
            button.transform.SetParent(_tabButtonHolder.transform, false);

            ((RectTransform)button.transform).localPosition = new Vector2(10 + 170 * (_tabs.Count - 1), 0);
        }

        internal void SetActiveTab(InteractionTab tab) {
            ClearTabs();
            tab.gameObject.SetActive(true);
        }

        #endregion Internal Methods

        #region Private Methods

        private void ClearTabs() {
            foreach (InteractionTab tab in _tabs) {
                tab.gameObject.SetActive(false);
            }
        }

        private void Close() {
            Destroy(gameObject);
            Player.EnableControls(true);
            _playerCamera.FollowTarget(Player.gameObject);
            _worldUI.SetActive(true);
        }

        #endregion Private Methods

    }
}