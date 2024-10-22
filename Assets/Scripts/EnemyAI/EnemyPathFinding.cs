using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathFinding : MonoBehaviour {

    [SerializeField] private float moveSpeed = 2f;

    private Rigidbody2D rb;
    private Vector2 moveDir;
    private EnemyKnockBack enemyKnockBack;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        enemyKnockBack = GetComponent<EnemyKnockBack>();
    }

    private void FixedUpdate() {
        if (enemyKnockBack.IsKnockedBack()) { return; }

        rb.MovePosition(rb.position + moveDir * (moveSpeed * Time.fixedDeltaTime));
    }

    public void MoveToTargetPosition(Vector2 position) {
        moveDir = position;
    }
}
