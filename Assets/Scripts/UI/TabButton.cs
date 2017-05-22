using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class TabButton : MonoBehaviour {

        public InteractionUI window { get; private set; }
        public InteractionTab tab { get; private set; }
        [SerializeField]
        private Text textObject;

        public string text {
            get {
                return textObject.text;
            }
            set {
                textObject.text = value;
            }
        }

        public void Bind(InteractionUI window, InteractionTab tab) {
            this.window = window;
            this.tab = tab;
        }

        public void SelectTab() {
            window.SetActiveTab(tab);
        }

    }
}
