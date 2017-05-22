using Model;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI {
    public class InfoTab : InteractionTab {

        #region Private Fields

        [FormerlySerializedAs("data")] [SerializeField] private Text _data;

        private InfoInteraction _source;

        #endregion Private Fields

        #region Public Methods

        public void Bind(InfoInteraction interaction) {
            _source = interaction;
        }

        public override string GetTitle() {
            return "Info";
        }

        public void Start() {
            _data.text = _source.Text;
        }

        #endregion Public Methods

    }
}