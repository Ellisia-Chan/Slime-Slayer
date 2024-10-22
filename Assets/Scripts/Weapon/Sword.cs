using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour, IWeapon {

    public static Sword Instance { get; private set; }

    [SerializeField] private float swordCD = 0.3f;

    private Transform swordCollider;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        swordCollider = PlayerController.Instance.GetWeaponCollider();
    }

    public void Attack() {
        SwordAnimator.Instance.AttackAnimation();
        SwordSlashAnimator.Instance.SpawnSwordSlash();
        EnableSwordCollider();
        StartCoroutine(AttackCDRoutine());
    }

    private IEnumerator AttackCDRoutine() {
        yield return new WaitForSeconds(swordCD);
        ActiveWeapon.Instance.ToggleIsAttacking(false);
        DisableSwordCollider();
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
