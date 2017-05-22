using System;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = System.Random;

namespace Model {
    public class InfoInteraction : OrbitalInteraction {

        #region Private Fields

        private static readonly GameObject Prefab = (GameObject) Resources.Load("Prefabs/UI/InfoTab");
        private readonly Random _rand;

        #endregion Private Fields

        #region Internal Properties

        internal string Text { get; private set; }

        #endregion Internal Properties

        #region Internal Constructors

        internal InfoInteraction(Orbital obj, int seed) : base(obj) {
            _rand = new Random(seed);
        }

        #endregion Internal Constructors

        #region Public Methods

        internal override InteractionTab GetTab() {
            var tab = Object.Instantiate(Prefab).GetComponent<InfoTab>();
            tab.Bind(this);
            return tab;
        }

        #endregion Public Methods

        #region Internal Methods

        internal void Generate(int techlevel, int orbital, Planetoid.PlanetType type) {
            string strtype = "Planet type: " + type.Name;
            AddLine(strtype);

            string techl = "Tech Level: " + Planetoid.Techlevels[techlevel];
            AddLine(techl);

            var pop = (float) (_rand.NextDouble() + 0.5);
            pop *= techlevel * 2;
            string population;
            if (pop > 1) {
                population = Math.Round(pop, 2) + " billion";
            }
            else if (pop > 0) {
                population = Math.Round(pop * 1000, 2) + " million";
            }
            else {
                population = "Zero";
            }
            population = "Population: " + population;
            AddLine(population);

            //AddLine("Orbital shell: " + orbital);

            float temp = _rand.Next(193, 213);
            temp *= (float) Math.Sqrt(3f / orbital);
            temp -= 173;
            string temperature = "Average temperature: " + Math.Round(temp, 0) + " degrees Celsius";
            AddLine(temperature);

            var orbitPeriod = (float) (_rand.NextDouble() - 0.5);
            orbitPeriod = 1 + orbitPeriod / 5f;
            orbitPeriod *= orbital / 3f;
            string period = "Orbital period: " + Math.Round(orbitPeriod, 2) + " Earth years";
            AddLine(period);
        }

        #endregion Internal Methods

        #region Private Methods

        private void AddLine(string line) {
            Text += line;
            Text += Environment.NewLine;
        }

        #endregion Private Methods

    }
}