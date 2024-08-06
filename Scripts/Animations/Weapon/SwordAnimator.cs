using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAnimator : MonoBehaviour {

    public static SwordAnimator Instance { get; private set; }

    private const string ATTACK_TRIGGER = "Attack";

    [SerializeField] private float swordCD = 0.3f;

    private Animator swordAnimator;
    private Transform swordCollider;
    private Vector3 mousePos;
    private Vector3 playerTransformPosition;
    private Vector3 playerScreenPoint;
    private float angleRotation;
    private bool attackButtondDown = false;
    private bool isAttacking = false;


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
        GameInput.Instance.OnAttackAction += GameInput_OnAttackAction;
        GameInput.Instance.OnAttackActionCancel += GameInput_OnAttackActionCancel;
    }

    private void OnDisable() {
        GameInput.Instance.OnAttackAction -= GameInput_OnAttackAction;
        GameInput.Instance.OnAttackActionCancel -= GameInput_OnAttackActionCancel;
    }

    private void GameInput_OnAttackAction(object sender, System.EventArgs e) {
        attackButtondDown = true;
    }

    private void GameInput_OnAttackActionCancel(object sender, EventArgs e) {
        attackButtondDown = false;
    }

    private void Update() {
        SwordFollowMouseOffset();
        Attack();
    }

    private void Attack() {
        if (attackButtondDown && !isAttacking) {
            isAttacking = true;
            swordAnimator.SetTrigger(ATTACK_TRIGGER);
            SwordSlashAnimator.Instance.SpawnSwordSlash();
            Sword.Instance.EnableSwordCollider();
            StartCoroutine(AttackCDRoutine());
        }
    }

    private IEnumerator AttackCDRoutine() {
        yield return new WaitForSeconds(swordCD);   
        Sword.Instance.DisableSwordCollider();
        isAttacking = false;
    }


    private void SwordFollowMouseOffset() {
        mousePos = PlayerController.Instance.GetMousePosition();
        playerTransformPosition = PlayerController.Instance.GetTransformPosition();
        swordCollider = Sword.Instance.GetSwordCollider();

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

    public void OnSwordAnimationEnd() {
        Sword.Instance.DisableSwordCollider();
    }

    public bool IsAttackingPerform() {
        return isAttacking;
    }

    public bool IsAttackButtonDown() {
        return attackButtondDown;
    }


}
