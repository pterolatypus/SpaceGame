using Controllers;

namespace Model {
	public interface Interactable {

		void Interact(PlayerShipController player);
		string GetInteractionText();

	}
}
