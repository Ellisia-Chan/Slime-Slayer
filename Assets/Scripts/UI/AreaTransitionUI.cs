using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaTransitionUI : MonoBehaviour {

    private const string AREA_TRANSITION_ENTRY = "Area Transition Entry anim";
    private const string AREA_TRANSITION_EXIT = "Area Transition Exit anim";

    public event EventHandler OnAreaChange;

    public static AreaTransitionUI Instance { get; private set; }

    private Animator animator;

    private float transitionTime = 1f;
    private bool IsTeleportToAreaRoutine = false;

    private void Awake() {
        Instance = this;
        animator = GetComponent<Animator>();
        Hide();
    }

    public void StartTransition() {
        if (IsTeleportToAreaRoutine) {
            StopCoroutine(StartTransitionRoutine());
            IsTeleportToAreaRoutine = false;

        }
        StartCoroutine(StartTransitionRoutine());
    }

    private IEnumerator StartTransitionRoutine() {
        IsTeleportToAreaRoutine = true;
        GameInput.Instance.playerInputActions.Disable();
        PlayerAnimator.Instance.ResetPlayerAnimator();
        PlayTransitionEntry();

        yield return new WaitForSeconds(transitionTime);

        OnAreaChange?.Invoke(this, EventArgs.Empty);

        yield return new WaitForSeconds(transitionTime);

        PlayTransitionExit();

        yield return new WaitForSeconds(transitionTime);
        GameInput.Instance.playerInputActions.Enable();

        IsTeleportToAreaRoutine = false;
        Hide();
    }

    private void PlayTransitionEntry() {
        animator.Play(AREA_TRANSITION_ENTRY);
    }

    private void PlayTransitionExit() {
        animator.Play(AREA_TRANSITION_EXIT);
    }

    public void Show() {
        gameObject.SetActive(true);
    }

    public void Hide() {
        gameObject.SetActive(false);
    }
}
