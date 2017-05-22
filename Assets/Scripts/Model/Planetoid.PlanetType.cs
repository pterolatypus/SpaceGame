namespace Model {
    public partial class Planetoid : Orbital, IInteractable {

        #region Public Structs

        public struct PlanetType {

            #region Public Properties

            public string Name { get; private set; }
            public float Weight { get; private set; }

            #endregion Public Properties

            #region Public Constructors

            public PlanetType(string name, float weight) : this() {
                Name = name;
                Weight = weight;
            }

            #endregion Public Constructors

        }

        #endregion Public Structs

    }
}