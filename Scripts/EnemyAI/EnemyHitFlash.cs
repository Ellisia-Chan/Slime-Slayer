using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitFlash : MonoBehaviour
{


    [SerializeField] private Material whiteFlashMat;
    [SerializeField] private float restoreDefaultMatTime = 0.2f;

    private Material defaultMat;
    private SpriteRenderer spriteRenderer;
    private EnemyHealth enemyHealth;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyHealth = GetComponent<EnemyHealth>();
        defaultMat = spriteRenderer.material;
    }

    public IEnumerator FlashRoutine() {
        spriteRenderer.material = whiteFlashMat;
        yield return new WaitForSeconds(restoreDefaultMatTime);
        spriteRenderer.material = defaultMat;
        enemyHealth.DetectDeath();
    }
}
