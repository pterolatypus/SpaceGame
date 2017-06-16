using UnityEngine;
using Random = System.Random;

namespace Model {
    public abstract class Orbital {

        #region Protected Fields

        protected float Radius, Angle;

        #endregion Protected Fields

        #region Private Fields

        private const float MinRadius = 200;
        private const float RadiusRange = 100;

        #endregion Private Fields

        #region Protected Fields

            protected bool _isGenerated;

        #endregion Protected Fields

        #region Internal Properties

        internal GameObject GameObject { get; set; }

        #endregion Internal Properties

        #region Protected Properties

        protected int Seed { get; private set; }

        #endregion Protected Properties

        #region Protected Constructors

        protected Orbital(int seed) {
            Seed = seed;
        }

        #endregion Protected Constructors

        #region Internal Methods

        internal abstract void Load();

        internal void Unload() {
            Object.Destroy(GameObject);
        }

        #endregion Internal Methods

        #region Protected Methods

        protected bool Generate(int orbital) {
            if (_isGenerated) return false;

            var r = new Random(Seed);
            Radius = (orbital + 1) * MinRadius + (float) r.NextDouble() * RadiusRange - RadiusRange / 2;
            Angle = (float) r.NextDouble() * 360f;
            _isGenerated = true;
            return true;
        }

        #endregion Protected Methods

    }
}