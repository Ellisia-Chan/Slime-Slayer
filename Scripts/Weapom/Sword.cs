using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour {

    public static Sword Instance { get; private set; }

    [SerializeField] private Transform swordCollider;

    private void Awake() {
        Instance = this;
        DisableSwordCollider();
    }

    private void Start() {
        GameInput.Instance.OnAttackAction += GameInput_OnAttackAction;
    }

    private void OnDisable() {
        GameInput.Instance.OnAttackAction -= GameInput_OnAttackAction;
    }

    private void GameInput_OnAttackAction(object sender, System.EventArgs e) {
        if (SwordAnimator.Instance.IsAttackButtonDown() && !SwordAnimator.Instance.IsAttackingPerform()) {
            EnableSwordCollider();
        }
    }

    public void EnableSwordCollider() {
        swordCollider.gameObject.SetActive(true);
    }

    public void DisableSwordCollider() {
        swordCollider.gameObject.SetActive(false);
    }

    public Transform GetSwordCollider() {
        return swordCollider;
    }
}
