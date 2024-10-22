using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectActivator : MonoBehaviour {

    [SerializeField] private List<GameObject> gameObjectsList = new List<GameObject>();

    private void Awake() {
        foreach (GameObject item in gameObjectsList) {
            item.SetActive(true);
        }
    }
}
