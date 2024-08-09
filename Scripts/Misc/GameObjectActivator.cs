using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectActivartor : MonoBehaviour
{

    [SerializeField] private GameObject TransitionUI;

    private void Awake() {
        TransitionUI.SetActive(true);
    }
}
