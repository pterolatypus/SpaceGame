using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour {

    public GameObject player;
    public float cameraFollowDelay = 0.3f;
    public float cameraZoomFactor = 0.5f;
    private Vector3 cameraVelocity = new Vector3(0, 0, 0);
    private bool follow = true;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void FixedUpdate() {
        if (follow) {
            Vector3 target = player.transform.position - new Vector3(0, 0, 10);
            Vector3 newPos = Vector3.SmoothDamp(transform.position, target, ref cameraVelocity, cameraFollowDelay);
            transform.position = newPos;
            GetComponent<Camera>().orthographicSize = 5f + cameraVelocity.magnitude * cameraZoomFactor;
        }
    }

    public void LockToPosition(Nullable<Vector2> pos) {
        LockToPosition(pos, 0f);
    }

    public void LockToPosition(Nullable<Vector2> pos, float zoom) {
        if (!pos.HasValue) {
            follow = true;
        }
        else {
            follow = false;
            transform.position = new Vector3(pos.Value.x, pos.Value.y, -10);
            GetComponent<Camera>().orthographicSize = 5f - zoom;
        }
    }
}