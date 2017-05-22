using System;
using System.Collections.Generic;
using System.Linq;
using Controllers;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = System.Random;

namespace Model {
    public partial class Planetoid : Orbital, IInteractable {

        #region Public Fields

        public static readonly List<string> TechLevels = new List<string> {
            "Uninhabited",
            "Agricultural",
            "Industrial",
            "Technological"
        };

        public static readonly List<PlanetType> Types = new List<PlanetType> {
            new PlanetType("Dwarf Planet", 1.5f),
            new PlanetType("Rocky Planet", 1f),
            new PlanetType("Ice Planet", 0.8f),
            new PlanetType("Ice Giant", 0.5f),
            new PlanetType("Gas Giant", 1.3f)
        };

        public PlanetType Type;

        #endregion Public Fields

        #region Private Fields

        private static readonly List<PlanetType> TypesNoIce = Types.Where(planetType => {
            bool isIceType = planetType.Name.StartsWith("Ice", StringComparison.Ordinal);
            return !isIceType;
        }).ToList();

        private List<OrbitalInteraction> _interactions;

        private GameObject _prefab;

        private float _rotation;

        private int _techLevel;

        #endregion Private Fields

        #region Public Constructors

        public Planetoid(int seed) : base(seed) {
        }

        #endregion Public Constructors

        #region Public Methods

        public new bool Generate(int orbital) {
            if (!base.Generate(orbital)) return false;

            List<PlanetType> validTypes = orbital < 4 ? TypesNoIce : Types;

            var r = new Random(Seed);
            double val = r.NextDouble();
            float div = validTypes.Sum(t => t.Weight);

            var total = 0f;
            Type = validTypes[validTypes.Count - 1];

            foreach (PlanetType t in validTypes) {
                total += t.Weight / div;
                if (total > val) {
                    Type = t;
                    break;
                }
            }

            _techLevel = r.Next(TechLevels.Count);

            _interactions = new List<OrbitalInteraction>();
            var info = new InfoInteraction(this, Seed);
            info.Generate(_techLevel, orbital, Type);

            _interactions.Add(info);
            if (_techLevel > 0) {
                var trade = new TradeInteraction(this, Seed);
                trade.Generate(_techLevel);
                //interactions.Add(trade);
            }

            _prefab = (GameObject) Resources.Load("Prefabs/Planets/" + Type.Name);
            _rotation = (int) (360 * r.NextDouble());
            return true;
        }

        public string GetInteractionText() {
            return "Press the interact key to land";
        }

        public void Interact(PlayerShipController player) {
            Debug.Log("Oh no you friccin moron, you just got INTERACTED! Tag your friend to totally INTERACT them.");

            var window = Object.Instantiate(Resources.Load("Prefabs/UI/InteractionWindow") as GameObject).GetComponent<InteractionUI>();
            window.transform.SetParent(GameObject.Find("ScreenUI").transform, false);
            window.player = player;
            window.source = this;

            var t = (RectTransform) window.transform;
            t.offsetMax = Vector2.zero;

            foreach (OrbitalInteraction i in _interactions) {
                window.AddTab(i);
            }
        }

        public new void Unload() {
            Object.Destroy(GameObject);
        }

        #endregion Public Methods

        #region Internal Methods

        internal override void Load() {
            var x = (int) (Radius * Mathf.Cos(Angle));
            var y = (int) (Radius * Mathf.Sin(Angle));

            GameObject = Object.Instantiate(_prefab, new Vector3(x, y, 0), Quaternion.Euler(0, 0, _rotation));
            var wp = GameObject.GetComponent<WorldPlanetoid>();
            wp.Source = this;
        }

        #endregion Internal Methods

    }
}