using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class TouchManager : MonoBehaviour
{
    const float SWIPE_THRESHOLD = 40f;

    PlayerInput input;

    InputAction runningSwipeSideways;
    InputAction runningTap;
    InputAction runningSwipeUp;

    public Action<Vector2> OnMove;
    public Action OnJump;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();

        runningSwipeSideways = input.actions["SwipeSideways"];
        runningSwipeUp = input.actions["SwipeUp"];
        runningTap = input.actions["Tap"];
    }

    private void OnEnable()
    {
        runningSwipeSideways.performed += RunningSwipe_performed;
        runningSwipeUp.performed += RunningJump_performed;
        runningTap.performed += RunningJump_performed;
    }

    private void OnDisable()
    {
        runningSwipeSideways.performed -= RunningSwipe_performed;
        runningSwipeUp.performed -= RunningJump_performed;
        runningTap.performed -= RunningJump_performed;
    }

    private void RunningJump_performed(InputAction.CallbackContext obj)
    {
        float delta = obj.ReadValue<float>();
        if (delta >= SWIPE_THRESHOLD || delta == 0f)
        {
            OnJump?.Invoke();
            Debug.Log("Jump delta: " + delta);
        }
    }

    private void RunningSwipe_performed(InputAction.CallbackContext obj)
    {
        Vector2 delta = obj.ReadValue<Vector2>();
        if (Mathf.Abs(delta.x) >= SWIPE_THRESHOLD)
        {
            OnMove?.Invoke(delta);
        }
    }

}
