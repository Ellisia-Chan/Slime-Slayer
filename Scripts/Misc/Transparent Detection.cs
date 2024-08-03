using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TransparentDetection : MonoBehaviour {

    [Range(0, 1)]
    [SerializeField] private float transparencyAmount = 0.8f;
    [SerializeField] private float fadeTime = 0.4f;

    private SpriteRenderer spriteRenderer;
    private Tilemap tilemap;
    private float elapsedTime = 0f;
    private float fullTranscparencyAmount = 1f;
    private Coroutine spriteFadeCoroutine;
    private Coroutine tilemapFadeCoroutine;

    private void Awake() {
        if (!TryGetComponent(out spriteRenderer)) {
            spriteRenderer = null;
        }

        if (!TryGetComponent(out tilemap)) {
            tilemap = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.GetComponent<PlayerController>()) {
            if (spriteRenderer != null) {
                if (spriteFadeCoroutine != null) {
                    StopCoroutine(spriteFadeCoroutine);
                }
                spriteFadeCoroutine = StartCoroutine(SpriteFadeRoutine(spriteRenderer, fadeTime, spriteRenderer.color.a, transparencyAmount));

            } else if (tilemap != null) {
                if (tilemapFadeCoroutine != null) {
                    StopCoroutine(tilemapFadeCoroutine);
                }
                tilemapFadeCoroutine = StartCoroutine(TilemapFadeRoutine(tilemap, fadeTime, tilemap.color.a, transparencyAmount));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.GetComponent<PlayerController>()) {
            if (spriteRenderer != null) {
                if (spriteFadeCoroutine != null) {
                    StopCoroutine(spriteFadeCoroutine);
                }
                spriteFadeCoroutine = StartCoroutine(SpriteFadeRoutine(spriteRenderer, fadeTime, spriteRenderer.color.a, fullTranscparencyAmount));

            } else if (tilemap != null) {
                if (tilemapFadeCoroutine != null) {
                    StopCoroutine(tilemapFadeCoroutine);
                }
                tilemapFadeCoroutine = StartCoroutine(TilemapFadeRoutine(tilemap, fadeTime, tilemap.color.a, fullTranscparencyAmount));
            }
        }
    }

    private IEnumerator SpriteFadeRoutine(SpriteRenderer spriteRenderer, float fadeTime, float startValue, float targetTransparency) {
        elapsedTime = 0f;
        while (elapsedTime < fadeTime) {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, targetTransparency, elapsedTime / fadeTime);
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, newAlpha);
            yield return null;
        }
    }

    private IEnumerator TilemapFadeRoutine(Tilemap tilemap, float fadeTime, float startValue, float targetTransparency) {
        elapsedTime = 0f;
        while (elapsedTime < fadeTime) {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, targetTransparency, elapsedTime / fadeTime);
            tilemap.color = new Color(tilemap.color.r, tilemap.color.g, tilemap.color.b, newAlpha);
            yield return null;
        }
    }
}
