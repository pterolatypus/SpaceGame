using Model;

namespace UI {
    public class TradeTab : InteractionTab {

        private TradeInteraction source;

        public void Bind(TradeInteraction interaction) {
            source = interaction;
        }

        internal override string GetTitle() {
            return "Trade";
        }
    }
}
