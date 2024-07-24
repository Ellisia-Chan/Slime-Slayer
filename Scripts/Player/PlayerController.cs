using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public static PlayerController Instance { get; private set; }

    [SerializeField] private float moveSpeed = 1f;

    private Rigidbody2D rb;
    private Vector2 movement;
    private Vector3 mousePos;
    private Vector3 playerScreenPoint;
    private bool isFacingLeft;

    private void Awake() {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        PlayerInput();
    }

    private void FixedUpdate() {
        PlayerMovementDirection();
        HandleMovement();
    }

    private void PlayerInput() {
        movement = GameInput.Instance.GetMovementVectorNormalized();
    }

    private void HandleMovement() {
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }

    private void PlayerMovementDirection() {
         mousePos = Input.mousePosition;
         playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        if (mousePos.x < playerScreenPoint.x) { 
            PlayerAnimator.Instance.FlipPlayerSpriteX(true);
            isFacingLeft = true;
        } else {
            PlayerAnimator.Instance.FlipPlayerSpriteX(false);
            isFacingLeft = false;
        }
    }

    public Vector2 GetMovementDir() {
        return movement;
    }

    public Vector3 GetMousePosition() {
        return mousePos;
    }

    public Vector3 GetTransformPosition() {
        return transform.position;
    }

    public bool IsFacingLeft() {
        return isFacingLeft;
    }
}
