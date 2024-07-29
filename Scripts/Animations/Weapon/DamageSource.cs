using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSource : MonoBehaviour {

    [SerializeField] private int damageAmount = 1;

    private EnemyHealth enemyHealth;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.GetComponent<EnemyHealth>()) {
            enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage(damageAmount);
        }
    }
}
