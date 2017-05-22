using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI {
    public class TabButton : MonoBehaviour {

        #region Private Fields

        [FormerlySerializedAs("textObject")][SerializeField] private Text textObject;

        #endregion Private Fields

        #region Public Properties

        public InteractionTab tab { get; private set; }

        public string text {
            get { return textObject.text; }
            set { textObject.text = value; }
        }

        public InteractionUI window { get; private set; }

        #endregion Public Properties

        #region Public Methods

        public void Bind(InteractionUI window, InteractionTab tab) {
            this.window = window;
            this.tab = tab;
        }

        public void SelectTab() {
            window.SetActiveTab(tab);
        }

        #endregion Public Methods

    }
}