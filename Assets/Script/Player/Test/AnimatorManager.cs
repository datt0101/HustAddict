using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    Animator animator;
    PlayerLocomotion player;
    int horizontal;
    int vertical;
    private void Awake()
    {
        player = GetComponent<PlayerLocomotion>();
        animator = GetComponent<Animator>();
        horizontal = Animator.StringToHash("Horizontal");
        vertical = Animator.StringToHash("Vertical");
    }

    public void UpdateAnimatorValues(float horizontalMovement, float verticalMovement)
    {
        float snappedHorizontal;
        float snappedVertical;

        #region SnappedHorizontal
        if (horizontalMovement > 0 && horizontalMovement < 0.55f)
        {
            snappedHorizontal = 0.5f;
        }
        else if (horizontalMovement > 0.55f)
        {
            snappedHorizontal = 1;
        }
        else if (horizontalMovement < 0 && horizontalMovement > -0.55f)
        {
            snappedHorizontal = -0.5f;
        }
        else if (horizontalMovement < -0.55f)
        {
            snappedHorizontal = -1;
        }
        else
        {
            snappedHorizontal = 0;
        }
        #endregion
        #region SnappedVertical
        if (verticalMovement > 0 && verticalMovement < 0.55f)
        {
            snappedVertical = 0.5f;
        }
        else if (verticalMovement > 0.55f)
        {
            snappedVertical = 1;
        }
        else if (verticalMovement < 0 && verticalMovement > -0.55f)
        {
            snappedVertical = -0.5f;
        }
        else if (verticalMovement < -0.55f)
        {
            snappedVertical = -1;
        }
        else
        {
            snappedVertical = 0;
        }
        #endregion

        animator.SetFloat(horizontal,snappedHorizontal,0.1f,Time.deltaTime);
        animator.SetFloat(vertical,snappedVertical,0.1f,Time.deltaTime);
    }
    private void Update()
    {
        CheckFalling();
        CheckSpeed();
        animator.SetBool("isGrounded", player.isGrounded);
    }
    void CheckSpeed()
    {
        if (player.isGrounded && Mathf.Abs(player.rb.velocity.x) <= Mathf.Epsilon && Mathf.Abs(player.rb.velocity.z) <= Mathf.Epsilon)
            animator.SetBool("isRunning", false);
    }
    void CheckFalling()
    {
        if(!player.isGrounded && player.rb.velocity.y < -2f)
        {
            animator.SetBool("isFalling", true);
        }
    }
}
