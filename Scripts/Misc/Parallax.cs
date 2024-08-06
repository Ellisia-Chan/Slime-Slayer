using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {

    public static Parallax Instance { get; private set; }

    [SerializeField] private float parallaxOffset = -0.25f;

    private Camera cam;
    private Vector2 startPos;
    private Vector2 travelPos => (Vector2)cam.transform.position - startPos;

    private void Awake() {
        Instance = this;
        cam = Camera.main;
        startPos = transform.position;
    }

    private void FixedUpdate() {
        MoveParallax();
    }

    public void ResetCameraStartPos() {
        transform.position = startPos - travelPos * parallaxOffset;
    }

    private void MoveParallax() {
        transform.position = startPos + travelPos * parallaxOffset;
    }
}
