namespace Model {
    public partial class Planetoid {

        #region Public Structs

        public struct PlanetType {

            #region Internal Properties

            internal string Name { get; private set; }
            internal float Weight { get; private set; }

            #endregion Internal Properties

            #region Internal Constructors

            internal PlanetType(string name, float weight) : this() {
                Name = name;
                Weight = weight;
            }

            #endregion Internal Constructors

        }

        #endregion Public Structs

    }
}