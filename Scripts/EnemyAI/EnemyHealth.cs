using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    [SerializeField] private int startingHealth = 3;
    [SerializeField] private GameObject deathVFXPrefab;

    private EnemyKnockBack enemyKnockBack;
    private EnemyHitFlash enemyHitFlash;
    private int currentHealth;
    private float knockedBackThrust = 15f;

    private void Awake() {
        enemyKnockBack = GetComponent<EnemyKnockBack>();
        enemyHitFlash = GetComponent<EnemyHitFlash>();
    }

    private void Start() {
        currentHealth = startingHealth;
    }

    public void TakeDamage(int damage) {
        currentHealth -= damage;
        enemyKnockBack.GetKnockedBack(PlayerController.Instance.transform, knockedBackThrust);
        StartCoroutine(enemyHitFlash.FlashRoutine());
        StartCoroutine(DetectDeathRoutine());
    }

    private IEnumerator DetectDeathRoutine() {
        yield return new WaitForSeconds(enemyHitFlash.GetRestoreMatTime());
        DetectDeath();
    }

    public void DetectDeath() {
        if (currentHealth <= 0) {
            Instantiate(deathVFXPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}