using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour {

    public static PlayerAnimator Instance { get; private set; }

    private const string MOVE_X = "moveX";
    private const string MOVE_Y = "moveY";

    private Animator playerAnimator;
    private SpriteRenderer spriteRenderer;
    private Vector2 movementDir;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(Instance);
            Instance = this;
            return;
        }

        Instance = this;
        playerAnimator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update() {
        if (playerAnimator != null) {
            movementDir = PlayerController.Instance.GetMovementDir();
            playerAnimator.SetFloat(MOVE_X, movementDir.x);
            playerAnimator.SetFloat(MOVE_Y, movementDir.y);
        }
    }

    public void FlipPlayerSpriteX(bool flipX) {
        if (flipX) {
            spriteRenderer.flipX = true;
        } else {
            spriteRenderer.flipX = false;
        }
    }

    public void ResetPlayerAnimator() {
        playerAnimator.SetFloat(MOVE_X, 0);
        playerAnimator.SetFloat(MOVE_Y, 0);
    }
}
