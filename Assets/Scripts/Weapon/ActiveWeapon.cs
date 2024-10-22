using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWeapon : MonoBehaviour {

    public static ActiveWeapon Instance { get; private set; }

    [SerializeField] private MonoBehaviour currentActiveWeapon;

    private bool attackButtonDown, isAttacking = false;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start() {
        GameInput.Instance.OnAttackAction += GameInput_OnAttackAction;
        GameInput.Instance.OnAttackActionCancel += GameInput_OnAttackActionCancel;
    }

    private void GameInput_OnAttackAction(object sender, System.EventArgs e) {
        StartAttacking();
    }

    private void GameInput_OnAttackActionCancel(object sender, System.EventArgs e) {
        StopAttacking();
    }

    private void Update() {
        Attack();
    }

    private void Attack() {
        if (attackButtonDown && !isAttacking) {
            isAttacking = true;
            (currentActiveWeapon as IWeapon).Attack();
        }
    }

    private void StartAttacking() {
        attackButtonDown = true;
    }

    private void StopAttacking() {
        attackButtonDown = false;
    }

    public void NewWeapon(MonoBehaviour newWeapon) {
        currentActiveWeapon = newWeapon;
    }
    
    public void WeaponNull() {
        currentActiveWeapon = null;
    }

    public void ToggleIsAttacking(bool value) {
        isAttacking = value;
    }

    public MonoBehaviour GetCurrentActiveWeapon() {
        return currentActiveWeapon;
    }
}