﻿using UnityEngine;
using System.Collections;

public class PropertiesExample : MonoBehaviour {

    [Component( "Main Camera" )]
    public Camera Camera1 { get; set; }
    [Component( "Light" )]
    public Camera Camera2 { get; set; }
    [Component( "NotExisting GameObject" )]
    public Camera Camera3 { get; set; }

    [Component]
    public BoxCollider2D Collider1 { get; set; }
    [Component( true, false )]
    public BoxCollider2D Collider2 { get; set; }

    [Component]
    private Transform transform1 { get; set; }
    [Component( true, false )]
    private Transform transform2 { get; set; }

    [Component]
    public TerrainCollider Terrain1 { get { return privateTerrain; } }
    [Component( false, true )]
    public TerrainCollider Terrain2 { get { return privateTerrain; } }

    private TerrainCollider privateTerrain;

    // Use this for initialization
    void Start() {
        this.LoadComponents();
    }

    // Update is called once per frame
    void Update() {

    }
}
