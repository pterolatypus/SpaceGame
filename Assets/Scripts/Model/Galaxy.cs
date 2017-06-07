using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace Model {
    internal class Galaxy {

        #region Private Fields

        private readonly Random _rand;

        #endregion Private Fields

        #region Public Properties

        public StarSystem Home { get; private set; }
        public List<StarSystem> Stars { get; private set; }

        #endregion Public Properties

        #region Public Constructors

        public Galaxy(int numberOfSystems) : this(numberOfSystems, new Random().Next()) {
        }

        public Galaxy(int numberOfSystems, int seed) {
            Stars = new List<StarSystem>();
            _rand = new Random(seed);
            GenerateWorld(numberOfSystems);
        }

        #endregion Public Constructors

        #region Public Methods

        public static StarSystem GenerateStar(Random generator) {
            const int coordsConstant = int.MaxValue / 2;
            return new StarSystem(
                new Vector2(generator.Next() - coordsConstant, generator.Next() - coordsConstant),
                generator.Next()
            );
        }

        public StarSystem GetRandomStar() {
            int r = _rand.Next(Stars.Count);
            return Stars[r];
        }

        #endregion Public Methods

        #region Private Methods

        private void GenerateWorld(int numberOfSystems) {
            //Generate Galaxy
            for (var i = 0; i < numberOfSystems; i++) {
                StarSystem s = GenerateStar(_rand);
                Stars.Add(s);
            }

            Stars.Sort(
                (l, r) => Vector2.SqrMagnitude(l.Coords).CompareTo(Vector2.SqrMagnitude(r.Coords))
            );
            Debug.Log("Generated " + Stars.Count + " system(s)");
            Home = Stars[0];
        }

        #endregion Private Methods

    }
}