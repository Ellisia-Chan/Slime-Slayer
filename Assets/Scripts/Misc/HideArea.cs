using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideArea : MonoBehaviour {
    private void Awake() {
        gameObject.SetActive(false);
    }

}
