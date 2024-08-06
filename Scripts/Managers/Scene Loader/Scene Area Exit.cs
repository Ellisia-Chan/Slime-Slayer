using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneAreaExit : MonoBehaviour
{

    [SerializeField] private GameObject currentArea;
    [SerializeField] private GameObject targetArea;
    [SerializeField] private Transform targetAreaEntrance;
    [SerializeField] private Collider2D targetAreaCamConfiner;


    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.GetComponent<PlayerController>()) {
            AreaManager.Instance.AreaToTeleport(targetArea, currentArea, targetAreaEntrance, targetAreaCamConfiner);
        }
    }

}
