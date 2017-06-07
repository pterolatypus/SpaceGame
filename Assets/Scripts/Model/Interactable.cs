using Controllers;

namespace Model {
    internal interface Interactable {

        #region Public Methods

        string GetInteractionText();

        void Interact(PlayerShipController player);

        #endregion Public Methods

    }
}