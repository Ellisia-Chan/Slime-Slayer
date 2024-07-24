using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlueSlimeAI : MonoBehaviour {


    private enum State {
        Roaming,
    }

    private State state;
    private EnemyPathFinding enemyPathFinding;
    private Vector2 roamPosition;

    private float secondsOffset = 2f;

    private void Awake() {
        enemyPathFinding = GetComponent<EnemyPathFinding>();
        state = State.Roaming;
    }

    private void Start() {
        StartCoroutine(RoamingRoutine());
    }

    private IEnumerator RoamingRoutine() {
        while (state == State.Roaming) {
            roamPosition = GetRoamingPosition();
            enemyPathFinding.MoveToTargetPosition(roamPosition);
            yield return new WaitForSeconds(secondsOffset);
        }
    }

    private Vector2 GetRoamingPosition() {
        return new Vector2(Random.Range(-1, 1f), Random.Range(-1, 1f)).normalized;
    }
}
