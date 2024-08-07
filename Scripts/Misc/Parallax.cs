using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {

    public static Parallax Instance { get; private set; }

    [SerializeField] private float parallaxOffset = -0.25f;
    [SerializeField] private GameObject treeTop;

    private Camera cam;
    private Vector2 startPos;
    private Vector2 travelPos => (Vector2)cam.transform.position - startPos;
    private Vector2 defaultPos;

    private void Awake() {
        Instance = this;
        cam = Camera.main;
        defaultPos = treeTop.transform.position;
    }

    private void OnEnable() {
        startPos = defaultPos;
    }

    private void FixedUpdate() {
        if (treeTop.activeInHierarchy) {
            MoveParallax();
        }
    }

    public void ResetCameraStartPos() {
        treeTop.transform.position = startPos - travelPos * parallaxOffset;
        Debug.Log("Position Resetted");
    }

    private void MoveParallax() {
        treeTop.transform.position = startPos + travelPos * parallaxOffset;
    }
}
