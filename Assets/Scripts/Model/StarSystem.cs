using System.Collections;
using System.Collections.Generic;
using Controllers;
using JetBrains.Annotations;
using UnityEngine;
using Random = System.Random;

namespace Model {
    public class StarSystem : IEnumerable<GameObject> {

        #region Private Fields

        private static readonly string[] Sizes = {
            "Dwarf",
            "Star",
            "Giant",
            "Supergiant"
        };

        private static readonly string[] Temps = {
            "Red",
            "Yellow",
            "White",
            "Blue"
        };

        private readonly List<Orbital> _satellites;
        private GameObject _gameObject;
        private bool _isGenerated = false;
        private int _rotation;
        private GameObject _starPrefab;
        private int _temp, _size;

        #endregion Private Fields

        #region Public Properties

        [NotNull] public string Type {
            get { return Temps[_temp] + " " + Sizes[_size]; }
        }

        #endregion Public Properties

        #region Internal Properties

        internal Vector2 Coords { get; private set; }

        #endregion Internal Properties

        #region Private Properties

        private int Seed { get; set; }

        #endregion Private Properties

        #region Internal Constructors

        internal StarSystem(Vector2 coords, int seed) {
            _satellites = new List<Orbital>();
            Coords = coords;
            Seed = seed;
        }

        #endregion Internal Constructors

        #region Public Methods

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public IEnumerator<GameObject> GetEnumerator() {
            foreach (Orbital orbital in _satellites) {
                if (orbital.GameObject != null) yield return orbital.GameObject;
            }
        }

        #endregion Public Methods

        #region Internal Methods

        internal void Load() {
            Debug.Log("Loading system");
            Generate();
            //instantiate all the game objects
            _gameObject = Object.Instantiate(_starPrefab, new Vector2(0, 0), Quaternion.Euler(0, 0, _rotation));

            Vector3
                scale = Vector3.one * (_size + 2) *
                        (_size + 2); //TODO: What does this do? Either needs commenting, or assign those sub expressions to named local variables
            _gameObject.transform.localScale = scale;

            foreach (Orbital obj in _satellites) {
                obj.Load();
            }
        }

        internal void Unload() {
            Object.Destroy(_gameObject);
            foreach (Orbital obj in _satellites) {
                obj.Unload();
                Object.Destroy(obj.GameObject);
            }
            foreach (WorldPlanetoid p in Object.FindObjectsOfType<WorldPlanetoid>()) {
                Object.Destroy(p.gameObject);
            }
        }

        #endregion Internal Methods

        #region Private Methods

        private bool Generate() {
            if (_isGenerated) return false;

            var rand = new Random(Seed);
            _temp = rand.Next(Temps.Length);
            _size = rand.Next(Sizes.Length);

            int numOrbits = 5 * (_size + 1);
            for (var i = 1; i <= numOrbits; i++) {
                GenerateOrbital(rand, i);
            }

            _starPrefab = Resources.Load("Prefabs/Stars/" + Temps[_temp]) as GameObject;
            _rotation = (int) (360 * rand.NextDouble());
            return true;
        }

        private void GenerateOrbital(Random rand, int i) {
            double r = rand.NextDouble();
            if (r < 0.5) {
                var p = new Planetoid(rand.Next());
                _satellites.Add(p);
                p.Generate(i);
            }
            else {
                _satellites.Add(new EmptyOrbital(0));
            }
        }

        #endregion Private Methods

    }
}