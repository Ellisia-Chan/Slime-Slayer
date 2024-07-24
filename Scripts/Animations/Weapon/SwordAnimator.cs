using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAnimator : MonoBehaviour {

    public static SwordAnimator Instance { get; private set; }

    private const string ATTACK_TRIGGER = "Attack";

    private Animator swordAnimator;
    private Vector3 mousePos;
    private Vector3 playerTransformPosition;
    private Vector3 playerScreenPoint;
    private float angleRotation;

    private void Awake() {
        Instance = this;
        swordAnimator = GetComponent<Animator>();
    }

    private void Start() {
        GameInput.Instance.OnAttackAction += GameInput_OnAttackAction;
    }

    private void OnDestroy() {
        GameInput.Instance.OnAttackAction -= GameInput_OnAttackAction;
    }

    private void GameInput_OnAttackAction(object sender, System.EventArgs e) {
        swordAnimator.SetTrigger(ATTACK_TRIGGER);
        SwordSlashAnimator.Instance.SpawnSwordSlash();
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
        } else {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angleRotation);

        }
    }
}
