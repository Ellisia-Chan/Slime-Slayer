using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaManager : MonoBehaviour {

    public static AreaManager Instance { get; private set; }

    [SerializeField] private CinemachineStateDrivenCamera virtualCamera;

    private CinemachineConfiner confiner;
    private GameObject areaObject;
    private Camera cam;

    private void Awake() {
        Instance = this;
        cam = Camera.main;
        confiner = virtualCamera.GetComponent<CinemachineConfiner>();
    }

    public void AreaToTeleport(GameObject targetArea, GameObject currentArea, Transform areaEntrance, Collider2D targetAreaCamConfiner) {
        Show(targetArea);
        Hide(currentArea);

        PlayerController.Instance.transform.position = areaEntrance.transform.position;
        cam.transform.position = new Vector3(areaEntrance.position.x, areaEntrance.position.y, cam.transform.position.z);
        
        Parallax.Instance.ResetCameraStartPos();
        UpdateCameraConfiner(targetArea, targetAreaCamConfiner);
    }

    private void UpdateCameraConfiner(GameObject targetArea, Collider2D cameraConfiner) {
        Collider2D newCameraConfiner = cameraConfiner;

        if (confiner != null && newCameraConfiner) {
            confiner.m_BoundingShape2D = newCameraConfiner;
            confiner.InvalidatePathCache();
        }
    }

    public void Show(GameObject nextArea) {
        nextArea.SetActive(true);
    }

    public void Hide(GameObject previousArea) {
        previousArea.SetActive(false);
    }

    
}
