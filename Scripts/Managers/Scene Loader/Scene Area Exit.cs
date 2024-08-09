using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneAreaExit : MonoBehaviour {


    [SerializeField] private GameObject currentArea;
    [SerializeField] private GameObject targetArea;
    [SerializeField] private Transform targetAreaEntrance;
    [SerializeField] private Collider2D targetAreaCamConfiner;

    private void Start() {
        AreaTransitionUI.Instance.OnAreaChange += AreaTransitionUI_OnAreaChange;
    }

    private void OnEnable() {
        if (AreaTransitionUI.Instance != null) {
            AreaTransitionUI.Instance.OnAreaChange += AreaTransitionUI_OnAreaChange;
        }
    }

    private void OnDisable() {
        if (AreaTransitionUI.Instance != null) {
            AreaTransitionUI.Instance.OnAreaChange -= AreaTransitionUI_OnAreaChange;
        }
    }

    private void AreaTransitionUI_OnAreaChange(object sender, System.EventArgs e) {
        TeleportToArea();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.GetComponent<PlayerController>()) {
            AreaTransitionUI.Instance.Show();
            AreaTransitionUI.Instance.StartTransition();
        }
    }

    public void TeleportToArea() {
        AreaManager.Instance.AreaToTeleport(targetArea, currentArea, targetAreaEntrance, targetAreaCamConfiner);
    }

}