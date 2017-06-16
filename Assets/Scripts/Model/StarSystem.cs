using System.Collections;
using System.Collections.Generic;
using Controllers;
using JetBrains.Annotations;
using UnityEngine;
using Random = System.Random;

namespace Model
{
    public class StarSystem : IEnumerable<GameObject>
    {
        #region Private Fields

        private readonly List<Orbital> _satellites;
        private bool _isGenerated = false;
        private int _rotation;

        #endregion Private Fields

        #region Public Properties

        [NotNull]
        public string Type
        {
            get { return ((Star)_satellites[0]).Type; }
        }

        #endregion Public Properties

        #region Internal Properties

        internal Vector2 Coords { get; private set; }

        #endregion Internal Properties

        #region Private Properties

        private int Seed { get; set; }

        #endregion Private Properties

        #region Internal Constructors

        internal StarSystem(Vector2 coords, int seed)
        {
            _satellites = new List<Orbital>();
            Coords = coords;
            Seed = seed;
        }

        #endregion Internal Constructors

        #region Public Methods

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<GameObject> GetEnumerator()
        {
            foreach (Orbital orbital in _satellites)
            {
                if (orbital.GameObject != null) yield return orbital.GameObject;
            }
        }

        #endregion Public Methods

        #region Internal Methods

        internal void Load()
        {
            Debug.Log("Loading system");
            Generate();
            foreach (Orbital obj in _satellites)
            {
                obj.Load();
            }
        }

        internal void Unload()
        {
            foreach (Orbital obj in _satellites)
            {
                obj.Unload();
            }
        }

        #endregion Internal Methods

        #region Private Methods

        private bool Generate()
        {
            if (_isGenerated) return false;

            var rand = new Random(Seed);

            Star star = GenerateStar(rand);
            _satellites.Add(star);

            int numOrbits = 5 * (star.Size + 1);
            for (var i = 1; i <= numOrbits; i++) {
                var orbitalObject = GenerateOrbital(rand, i);
                _satellites.Add(orbitalObject);
            }

            return true;
        }

        private Orbital GenerateOrbital(Random rand, int orbital) {
            Orbital orbitalObject;
            if (orbital == 0) orbitalObject = GenerateStar(rand);
            else orbitalObject = GenerateNonStarOrbital(rand, orbital);
            return orbitalObject;
        }

        private Star GenerateStar(Random rand) {
            return new Star(rand.Next());
        }

        private Orbital GenerateNonStarOrbital(Random rand, int orbital) {
            double r = rand.NextDouble();
            if (r < 0.5) return new Planetoid(rand.Next(), orbital);
            else return new EmptyOrbital(0);
        }

        #endregion Private Methods
    }
}