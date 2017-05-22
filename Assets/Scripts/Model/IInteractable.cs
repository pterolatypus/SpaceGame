using Controllers;

namespace Model {
    internal interface IInteractable {

        #region Public Methods

        string GetInteractionText();

        void Interact(PlayerShipController player);

        #endregion Public Methods

    }
}