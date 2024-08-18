using EasyCharacterMovement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterControlFake : Character
{
    public static CharacterControlFake Instance;
    public bool moveAble;
    public bool isSprinting;
    public Vector3 characterMovementDirection;
    protected override void Awake()
    {
        Instance = this;
        base.Awake();
        jumpImpulse = 4.5f;
    }


    protected override void HandleInput()
    {
        if (camera)
        {
            // If Camera is assigned, add input movement relative to camera look direction
            float _horizontalInput = InputManager.instance.horizontalInput;
            float _verticalInput = InputManager.instance.verticalInput;

            Vector3 movementDirection = Vector3.zero;
            movementDirection += Vector3.right * _horizontalInput;
            movementDirection += Vector3.forward * _verticalInput;
            movementDirection = movementDirection.relativeTo(cameraTransform);
            characterMovementDirection = movementDirection;
         

        }
    }

    protected override void OnJump(InputAction.CallbackContext context)
    {
        //Nothing
    }
    protected override void OnSprint(InputAction.CallbackContext context)
    {
        //Nothing
    }
}
