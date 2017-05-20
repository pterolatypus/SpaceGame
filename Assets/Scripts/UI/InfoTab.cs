using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoTab : InteractionTab {

    [SerializeField]
    private Text data;
    
    private InfoInteraction source;

    public void Bind(InfoInteraction interaction) {
        source = interaction;
    }

    public void Start() {
        data.text = source.text;
    }

    public override string GetTitle() {
        return "Info";
    }

}
