namespace Model {
    public abstract class OrbitalInteraction {

        #region Private Fields

        private Orbital _obj;

        #endregion Private Fields

        #region Protected Constructors

        protected OrbitalInteraction(Orbital obj) {
            _obj = obj;
        }

        #endregion Protected Constructors

        #region Internal Methods

        internal abstract InteractionTab GetTab();

        #endregion Internal Methods

    }
}