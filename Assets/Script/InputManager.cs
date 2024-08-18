using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;
    PlayerControls playerControls;
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
    

    private void Awake()
    {
        instance = this;

    }
    private void OnEnable()
    {
        
        if(playerControls == null)
        {
            playerControls = new PlayerControls();
            playerControls.PlayerMovementControl.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
           
        }

        playerControls.Enable();
    }
    private void OnDisable()
    {
        playerControls.Disable();
    }


    public void HandleAllInputs()
    {
        HandleMovementInput();
    }
    public bool JumpTriggered()
    {
        return playerControls.FindAction("Jump").triggered;
    }
    public bool SprintTriggered()
    {
        return playerControls.FindAction("Sprint").triggered;
    }
    private void HandleMovementInput()
    {
        verticalInput = joystick.vertical;
        horizontalInput = joystick.horizontal;
        //moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput)+ Mathf.Abs(verticalInput));
        //animatorManager.UpdateAnimatorValues(0, moveAmount);
    }

    private void Update()
    {
        HandleAllInputs();
        if (Input.touchCount == 0)
        {
            isLooking = false;
            lookFingerID = -1;
            targetLook = Vector2.zero;
        }
        else
        {
            if (isLooking && lookFingerID != -1)
            {
                int index = -1;

                for (int i = 0; i < Input.touchCount; i++)
                {
                    if (Input.GetTouch(i).fingerId == lookFingerID)
                    {
                        index = i;
                        break;
                    }
                }

                if (index >= 0)
                {
                    Vector2 touchDelta = Input.GetTouch(index).position - cachedTouchPos;
                    cachedTouchPos = Input.GetTouch(index).position;
                    touchDelta = touchDelta / Time.deltaTime;
                    touchDelta /= (140f / camsensitivity);
                    touchDelta = Vector3.ClampMagnitude(touchDelta, 7f);
                    targetLook.x = Mathf.Abs(touchDelta.y) > 0.3f ? touchDelta.y : 0f;
                    targetLook.y = Mathf.Abs(touchDelta.x) > 0.3f ? touchDelta.x : 0f;
                }
                else
                {
                    targetLook = Vector2.zero;
                }
            }
            else
            {
                targetLook = Vector2.zero;
            }
        }

        lookValue = Vector2.Lerp(lookValue, targetLook, 15 * Time.deltaTime);

        if (Mathf.Abs(lookValue.x) < 0.01f)
        {
            lookValue.x = 0;
        }
        if (Mathf.Abs(lookValue.y) < 0.01f)
        {
            lookValue.y = 0;
        }

    }
    public void LookPressed(BaseEventData eventData)
    {
     
        if (isLooking) return;
        isLooking = true;
        PointerEventData pointerEventData = (PointerEventData)eventData;
        cachedTouchPos = pointerEventData.position;
        lookFingerID = -1;
        foreach (var touch in Input.touches)
        {
            if (touch.position == cachedTouchPos)
                lookFingerID = touch.fingerId;
        }
    }

    public void LookReleased()
    {
        
        foreach (var touch in Input.touches)
        {
            if (touch.fingerId == lookFingerID)
            {
                if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                {
                    isLooking = false;
                    lookFingerID = -1;
                    targetLook = Vector2.zero;
                }
                break;
            }
        }
    }
    public void ToggleSprintingTrue()
    {
        isSprinting = true;
    }
    public void ToggleSprintingFalse()
    {
        isSprinting = false;
    }
}
