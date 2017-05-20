using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OrbitalInteraction {

    protected Orbital obj;

    public OrbitalInteraction(Orbital obj) {
        this.obj = obj;
    }

    public abstract InteractionTab GetTab();
    

}
