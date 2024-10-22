using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAnimator : MonoBehaviour {

    public static SwordAnimator Instance { get; private set; }

    private const string ATTACK_TRIGGER = "Attack";

    private Animator swordAnimator;
    private Transform swordCollider;
    private Vector3 mousePos;
    private Vector3 playerTransformPosition;
    private Vector3 playerScreenPoint;
    private float angleRotation;


    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(Instance);
            Instance = this;
            return;
        }

        Instance = this;
        swordAnimator = GetComponent<Animator>();
    }

    private void Start() {
        swordCollider = PlayerController.Instance.GetWeaponCollider();
    }

    private void Update() {
        SwordFollowMouseOffset();
    }

    private void SwordFollowMouseOffset() {
        mousePos = PlayerController.Instance.GetMousePosition();
        playerTransformPosition = PlayerController.Instance.GetTransformPosition();

        playerScreenPoint = Camera.main.WorldToScreenPoint(playerTransformPosition);
        angleRotation = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if (mousePos.x < playerScreenPoint.x) {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, angleRotation);
            swordCollider.transform.rotation = Quaternion.Euler(0, -180, 0);
        } else {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angleRotation);
            swordCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void AttackAnimation() {
        swordAnimator.SetTrigger(ATTACK_TRIGGER);
    }

    public void OnSwordAnimationEnd() {
        Sword.Instance.DisableSwordCollider();
    }
}
