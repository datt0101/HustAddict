using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLocomotion : MonoBehaviour
{
    InputManager inputManager;
    Vector3 moveDir;
    TouchInput touchInput;
    Animator animator;
    public Transform cameraObject;
    public Rigidbody rb;
    public float movementSpeed = 7f;
    public float rotationSpeed = 10f;
    public float jumpForce = 10f;
    public bool isGrounded = false;
    public bool isSliding = false;
    public bool isJumping = false;
    public float testTime;


    [Header("In Air")]
    public float inAirTimer;
    public float fallingVel;
    public LayerMask groundLayer;
    public float raycastDistance = 0.1f;
    public GameObject centerPos;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        touchInput = FindObjectOfType<TouchInput>();
        inputManager = GetComponent<InputManager>();
        rb = GetComponent<Rigidbody>();
    }

    private void HandleMovement()
    {
        moveDir = cameraObject.forward * inputManager.verticalInput;
        moveDir = moveDir + cameraObject.right * inputManager.horizontalInput;
        moveDir.Normalize();
        moveDir.y = 0;

        moveDir = moveDir * movementSpeed;
        Vector3 movementVel = moveDir;
        rb.velocity = movementVel;

    }

    private void HandleRotation()
    {
        Vector3 targetDir = Vector3.zero;
        targetDir = cameraObject.forward * inputManager.verticalInput;
        targetDir = targetDir + cameraObject.right * inputManager.horizontalInput;
        targetDir.Normalize();
        targetDir.y = 0;
        if (targetDir == Vector3.zero)
            targetDir = transform.forward;
        Quaternion targetRotation = Quaternion.LookRotation(targetDir);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        transform.rotation = playerRotation;
    }
    private void HandleFalling()
    {
        RaycastHit hit;
        Vector3 targetPosition;
        targetPosition = centerPos.transform.position;

        if (!isGrounded || isSliding)
        {
            ApplyGravity();
        }
        Debug.DrawRay(centerPos.transform.position, Vector3.down * raycastDistance, Color.white);
        if (Physics.Raycast(centerPos.transform.position, Vector3.down, out hit, raycastDistance, groundLayer))
        {
            float angle;
            angle = Vector3.Angle(hit.normal, Vector3.up);

            if (!isGrounded)
            {
                isGrounded = true;
                Vector3 rayCastHitPoint = hit.point;
                targetPosition.y = rayCastHitPoint.y;

                isJumping = false;
                animator.SetBool("isJumping", false);
                animator.SetBool("isFalling", false);
                if (!isSliding)
                {
                    inAirTimer = 0;

                }
            }
            if (angle > 45f)
            {
                isSliding = true;

            }
            else
            {
                isSliding = false;
                inAirTimer = 0;
                touchInput.jumpAble = true;
            }


        }
        else
        {
            isGrounded = false;
            touchInput.jumpAble = false;
        }
        if (isGrounded && !isSliding)
        {
            rb.position = new Vector3(rb.position.x, hit.point.y, rb.position.z);

        }

    }
    public void HandleJumping()
    {

        if (isJumping)
        {
            rb.velocity += Vector3.up * jumpForce;
        }

    }

    public void HandleAllMovements()
    {
        HandleMovement();
        HandleRotation();
        HandleJumping();
        HandleFalling();
    }
    void ApplyGravity()
    {
        inAirTimer += Time.deltaTime;
        rb.velocity -= Vector3.up * fallingVel * inAirTimer;
    }

}
