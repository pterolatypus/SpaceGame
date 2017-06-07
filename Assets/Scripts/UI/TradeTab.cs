using Model;

namespace UI {
    public class TradeTab : InteractionTab {

        #region Private Fields

        private TradeInteraction _source;

        #endregion Private Fields

        #region Public Methods

        internal void Bind(TradeInteraction interaction) {
            _source = interaction;
        }

        #endregion Public Methods

        #region Internal Methods

        internal override string GetTitle() {
            return "Trade";
        }

        #endregion Internal Methods

    }
}