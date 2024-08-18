using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonController : MonoBehaviour
{
    private ThirdPersonActionAsset playerActionsAsset;
    private InputAction move;

    public bool isGrounded;

    private Rigidbody rb;
    [SerializeField]
    private float movementForce = 1f;
    [SerializeField]
    private float jumpForce = 5f;
    [SerializeField]
    private float maxSpeed = 5f;
    private Vector3 forceDirection = Vector3.zero;

    [SerializeField]
    private Camera playerCamera;

    [SerializeField] private Animator m_animator = null;
    private List<Collider> m_collisions = new List<Collider>();

    //test 
    

    private void Awake()
    {
        if (!m_animator) { gameObject.GetComponent<Animator>(); }
        rb = this.GetComponent<Rigidbody>();
        playerActionsAsset = new ThirdPersonActionAsset();
    }

    private void OnEnable()
    {
        playerActionsAsset.Player.Jump.started += DoJump;
        move = playerActionsAsset.Player.Move;
        playerActionsAsset.Player.Enable();
    }
    private void OnDisable()
    {
        playerActionsAsset.Player.Jump.started -= DoJump;
        playerActionsAsset.Player.Disable();
    }
    private void FixedUpdate()
    {
        forceDirection += move.ReadValue<Vector2>().x * GetCameraRight(playerCamera) * movementForce;
        forceDirection += move.ReadValue<Vector2>().y * GetCameraForward(playerCamera) * movementForce;

        
        rb.AddForce(forceDirection,ForceMode.Impulse);
        forceDirection = Vector3.zero;

        if(rb.velocity.y < 0f)
        {
            rb.velocity -= Vector3.down * Physics.gravity.y * Time.fixedDeltaTime;
        }
        Vector3 horizontalVelocity = rb.velocity;
        horizontalVelocity.y = 0f;
        if (horizontalVelocity.sqrMagnitude > maxSpeed * maxSpeed)
        {
            rb.velocity = horizontalVelocity.normalized * maxSpeed + Vector3.up * rb.velocity.y;
        }

        LookAt();

        MoveAnim();
    }
    void MoveAnim()
    {
        if(isGrounded)
        {
            m_animator.SetBool("isJumping", false);
            m_animator.SetBool("isFalling", false);
            if (Mathf.Abs(rb.velocity.x) > .5f || Mathf.Abs(rb.velocity.z) > .5f)
            {
                m_animator.SetBool("isRunning", true);
            }
            else
            {
                m_animator.SetBool("isRunning", false);
            }
        }
        else
        {
            m_animator.SetBool("isRunning", false);
            if (rb.velocity.y > Mathf.Epsilon)
            {
                m_animator.SetBool("isJumping", true);
            }
            else if (rb.velocity.y < 0)
            {
                m_animator.SetBool("isFalling", true);
            }
        }
        

    }
    private void LookAt()
    {
        Vector3 direction = rb.velocity;
        direction.y = 0f;

        if(move.ReadValue<Vector2>().sqrMagnitude > 0.1f && direction.sqrMagnitude > 0.1f)
        {
            this.rb.rotation = Quaternion.LookRotation(direction,Vector3.up);
        }
        else
        {
            rb.angularVelocity = Vector3.zero;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint[] contactPoints = collision.contacts;
        for (int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
            {
                if (!m_collisions.Contains(collision.collider))
                {
                    m_collisions.Add(collision.collider);
                }
                isGrounded = true;
            }
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        ContactPoint[] contactPoints = collision.contacts;
        bool validSurfaceNormal = false;
        for (int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
            {
                validSurfaceNormal = true; break;
            }
        }

        if (validSurfaceNormal)
        {
            isGrounded = true;
            if (!m_collisions.Contains(collision.collider))
            {
                m_collisions.Add(collision.collider);
            }
        }
        else
        {
            if (m_collisions.Contains(collision.collider))
            {
                m_collisions.Remove(collision.collider);
            }
            if (m_collisions.Count == 0) { isGrounded = false; }
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (m_collisions.Contains(collision.collider))
        {
            m_collisions.Remove(collision.collider);
        }
        if (m_collisions.Count == 0) { isGrounded = false; }
    }
    private Vector3 GetCameraForward(Camera playerCamera)
    {
        Vector3 forward = playerCamera.transform.forward;
        forward.y = 0;
        return forward.normalized;
    }
    private Vector3 GetCameraRight(Camera playerCamera)
    {
        Vector3 right = playerCamera.transform.right;
        right.y = 0;
        return right.normalized;
    }
    

   
    private void DoJump(InputAction.CallbackContext obj)
    {
        if(isGrounded)
        {
            forceDirection += Vector3.up * jumpForce;
        }
    }

    //test
   
}
