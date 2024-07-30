using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public static PlayerController Instance { get; private set; }

    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float dashSpeed = 4f;
    [SerializeField] private TrailRenderer trailRenderer;

    private Rigidbody2D rb;
    private Vector2 movement;
    private Vector3 mousePos;
    private Vector3 playerScreenPoint;

    private bool isFacingLeft = false;
    private bool isDashing = false;

    private float dashTime = 0.2f;
    private float dashCD = 0.2f;

    private float defaultMoveSpeed = 4f;

    private void Awake() {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        GameInput.Instance.OnDashAction += GameInput_OnDashAction;
    }

    private void GameInput_OnDashAction(object sender, System.EventArgs e) {
        Dash();
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

    private void Dash() {
        if (!isDashing) {
            isDashing = true;
            moveSpeed *= dashSpeed;
            trailRenderer.emitting = true;
            StartCoroutine(DashRoutine());
        }
    }

    private IEnumerator DashRoutine() {
        yield return new WaitForSeconds(dashTime);
        moveSpeed = defaultMoveSpeed;
        trailRenderer.emitting = false;
        yield return new WaitForSeconds(dashCD);
        isDashing = false;
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
