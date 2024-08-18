using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManagerMinigame : MonoBehaviour
{

    [SerializeField]
    FloatingJoystick joystick;
    public Vector2 movementInput;
    public Vector2 lookValue;
    public Vector2 cachedTouchPos;
    public Vector2 targetLook;
    public float camsensitivity = 1f;
    int lookFingerID;
    public bool isLooking;
    public float moveAmount;
    public float verticalInput;
    public float horizontalInput;
    public bool isSprinting;

    private void Update()
    {
        HandleAllInputs();
    }

    public void HandleAllInputs()
    {
        HandleMovementInput();
    }
   
    private void HandleMovementInput()
    {
        verticalInput = joystick.vertical;
        horizontalInput = joystick.horizontal;
        //moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput)+ Mathf.Abs(verticalInput));
        //animatorManager.UpdateAnimatorValues(0, moveAmount);
    }
}
