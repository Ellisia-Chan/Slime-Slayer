using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    [SerializeField] private float roamChangeDirTime = 2f;

    private enum State {
        Roaming,
    }

    private State state;
    private EnemyPathFinding enemyPathFinding;
    private Vector2 roamPosition;

    private void Awake() {
        enemyPathFinding = GetComponent<EnemyPathFinding>();
        state = State.Roaming;
    }

    private void OnDisable() {
        StopAllCoroutines();
    }

    private void OnEnable() {
        StartCoroutine(RoamingRoutine());
    }

    private void Start() {
        StartCoroutine(RoamingRoutine());
    }

    private IEnumerator RoamingRoutine() {
        while (state == State.Roaming) {
            roamPosition = GetRoamingPosition();
            enemyPathFinding.MoveToTargetPosition(roamPosition);
            yield return new WaitForSeconds(roamChangeDirTime);
        }
    }

    private Vector2 GetRoamingPosition() {
        return new Vector2(Random.Range(-1, 1f), Random.Range(-1, 1f)).normalized;
    }
}
