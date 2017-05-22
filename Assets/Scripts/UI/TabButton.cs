using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI {
    public class TabButton : MonoBehaviour {

        #region Private Fields

        [FormerlySerializedAs("textObject")] [SerializeField] private Text _textObject;

        #endregion Private Fields

        #region Internal Properties

        internal string Text {
            get { return _textObject.text; }
            set { _textObject.text = value; }
        }

        #endregion Internal Properties

        #region Private Properties

        private InteractionTab Tab { get; set; }
        private InteractionUI Window { get; set; }

        #endregion Private Properties

        #region Public Methods

        public void SelectTab() {
            Window.SetActiveTab(Tab);
        }

        #endregion Public Methods

        #region Internal Methods

        internal void Bind(InteractionUI window, InteractionTab tab) {
            Window = window;
            Tab = tab;
        }

        #endregion Internal Methods

    }
}