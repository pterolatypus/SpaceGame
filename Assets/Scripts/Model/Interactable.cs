using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Interactable {

	void Interact(PlayerShipController player);
    string GetInteractionText();

}
