using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using Random = System.Random;

namespace Model {

	internal class Star : Orbital {

		private static readonly List<string> Sizes = new List<string> {
			"Dwarf",
			"Star",
			"Giant",
			"Supergiant"
		};

		private static readonly List<string> Temps = new List<string> {
			"Red",
			"Yellow",
			"White",
			"Blue"
		};

		private GameObject _prefab;
		private float _rotation;
		private int _temp;

		internal int Size { get; set; }

		[NotNull]
		internal string Type {
			get { return Temps[_temp] + " " + Sizes[Size]; }
		}

		public Star(int seed) : base(seed) {
		}

		internal override void Load() {
			Generate();
			GameObject = GameObject.Instantiate(_prefab, new Vector2(0, 0), Quaternion.Euler(0, 0, _rotation));

			float scaleFactor = (float) Math.Pow(Size + 2, 2);
			Vector3 scaleVector = Vector3.one * scaleFactor;
			GameObject.transform.localScale = scaleVector;
		}

		private void Generate() {
			if (!base.Generate(0)) return;
			Random rand = new Random(Seed);

			Size = rand.Next(0, Sizes.Count);
			_temp = rand.Next(0, Temps.Count);

			_prefab = (GameObject) Resources.Load("Prefabs/Stars/" + Star.Temps[_temp]);

			_rotation = 360 * (float) rand.NextDouble();
		}
	}

}