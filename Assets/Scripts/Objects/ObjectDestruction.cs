using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestruction : MonoBehaviour {

    [SerializeField] private GameObject destroyVFX;
    [SerializeField] private string VFXSortingLayer = "Default";
    [SerializeField] private int VFXSortingLayerOrder = 0;

    private ParticleSystemRenderer ps;
    private GameObject destructionVFX;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.GetComponent<DamageSource>()) {
            HandleDestruction();
        }

        CheckEnemyKnockback(collision);
    }

    private void OnTriggerStay2D(Collider2D collision) {
        CheckEnemyKnockback(collision);
    }

    private void HandleDestruction() {
        CreateDestructionVFX();
        SetSortingLayer(destructionVFX);
        Destroy(gameObject);
    }

    private void CreateDestructionVFX() {
        destructionVFX = Instantiate(destroyVFX, transform.position, Quaternion.identity);
    }

    private void CheckEnemyKnockback(Collider2D collision) {
        if (collision.gameObject.GetComponent<EnemyAI>()) {
            EnemyKnockBack enemyKnockBack = collision.gameObject.GetComponent<EnemyKnockBack>();
            if (enemyKnockBack.IsKnockedBack()) {
                HandleDestruction();
            }
        }
    }

    private void SetSortingLayer(GameObject obj) {
        ps = obj.GetComponent<ParticleSystemRenderer>();
        if (ps != null) {
            ps.sortingLayerName = VFXSortingLayer;
            ps.sortingOrder = VFXSortingLayerOrder;
        }
    }
}
