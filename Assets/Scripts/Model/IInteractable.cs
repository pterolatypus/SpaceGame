using Controllers;

namespace Model {
	internal interface IInteractable {

		void Interact(PlayerShipController player);
		string GetInteractionText();

	}
}
