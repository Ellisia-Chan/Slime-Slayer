using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSlashAnimator : MonoBehaviour {

    private const string SWORD_SLASH_SPAWN_POINT = "SwordSlashSpawnPoint";

    public static SwordSlashAnimator Instance { get; private set; }

    [SerializeField] private GameObject SwordSlashPrefab;
    [SerializeField] private Transform SwordSlashSpawnPoint;

    private GameObject slashObject;
    private Transform weaponCollider;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        weaponCollider = PlayerController.Instance.GetWeaponCollider();
        SwordSlashSpawnPoint = GameObject.Find(SWORD_SLASH_SPAWN_POINT).transform;
    }

    public void SpawnSwordSlash() {
        slashObject = Instantiate(SwordSlashPrefab, SwordSlashSpawnPoint.position, Quaternion.identity);
        slashObject.transform.parent = SwordAnimator.Instance.transform.parent;
    }

    public void SwingUpSlashFlip() {
        slashObject.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);

        if (PlayerController.Instance.IsFacingLeft()) {
            slashObject.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void SwingDownSlashFlip() {
        slashObject.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

        if (PlayerController.Instance.IsFacingLeft()) {
            slashObject.GetComponent<SpriteRenderer>().flipX = true;
        }
    }
}
