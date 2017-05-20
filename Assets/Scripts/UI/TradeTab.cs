using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeTab : InteractionTab {

    private TradeInteraction source;

    public void Bind(TradeInteraction interaction) {
        source = interaction;
    }

    public override string GetTitle() {
        return "Trade";
    }
}
