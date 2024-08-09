using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectActivator : MonoBehaviour
{

    [SerializeField] private GameObject TransitionUI;

    private void Awake() {
        TransitionUI.SetActive(true);
    }
}
