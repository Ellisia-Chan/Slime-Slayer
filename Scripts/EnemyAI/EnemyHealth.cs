using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    [SerializeField] private int startingHealth = 3;
    [SerializeField] private GameObject deathVFXPrefab;
    [SerializeField] private string deathVFXSortingLayerName = "Default";
    [SerializeField] private int deathVFXSortingLayerOrder = 0;

    private EnemyKnockBack enemyKnockBack;
    private EnemyHitFlash enemyHitFlash;
    private ParticleSystemRenderer ps;
    private GameObject deathVFX;
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
            deathVFX = Instantiate(deathVFXPrefab, transform.position, Quaternion.identity);
            SetSortingLayer(deathVFX);
            Destroy(gameObject);
        }
    }

    private void SetSortingLayer(GameObject obj) {
        ps = obj.GetComponent<ParticleSystemRenderer>();
        if (ps != null) {
            ps.sortingLayerName = deathVFXSortingLayerName;
            ps.sortingOrder = deathVFXSortingLayerOrder;
        }
    }
}