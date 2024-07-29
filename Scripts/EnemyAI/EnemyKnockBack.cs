using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKnockBack : MonoBehaviour {

    [SerializeField] private float knockBackTime = 0.1f;

    private Rigidbody2D rb;
    private bool isKnockedBack;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    public void GetKnockedBack(Transform damageSoure, float knockBackThrust) {
        isKnockedBack = true;
        Vector2 difference = (transform.position - damageSoure.position).normalized * knockBackThrust * rb.mass;
        rb.AddForce(difference, ForceMode2D.Impulse);
        StartCoroutine(KnockRoutine());
    }

    private IEnumerator KnockRoutine() {
        yield return new WaitForSeconds(knockBackTime);
        rb.velocity = Vector2.zero;
        isKnockedBack = false;
    }

    public bool IsKnockedBack() {
        return isKnockedBack;
    }
}
