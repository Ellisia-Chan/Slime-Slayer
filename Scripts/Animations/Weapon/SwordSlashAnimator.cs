using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSlashAnimator : MonoBehaviour {

    public static SwordSlashAnimator Instance { get; private set; }

    [SerializeField] private GameObject SwordSlashPrefab;
    [SerializeField] private Transform SwordSlashSpawnPoint;

    private GameObject slashObject;

    private void Awake() {
        Instance = this;
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
