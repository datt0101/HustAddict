using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInput : MonoBehaviour
{
    PlayerLocomotion player;
    Animator animator;
    public bool jumpAble = false;
    private void Awake()
    {
        player = FindObjectOfType<PlayerLocomotion>();
        animator = player.GetComponent<Animator>();
    }
    public void OnFDown()
    {
       
        player.movementSpeed *= 2;
        if (Mathf.Abs(player.rb.velocity.x) <= Mathf.Epsilon && Mathf.Abs(player.rb.velocity.z) <= Mathf.Epsilon)
            return;
        else
        {

            animator.SetBool("isRunning", true);
        }
    }
    public void OnFUp()
    {
        
        player.movementSpeed /= 2;
        animator.SetBool("isRunning", false);
    }
    public void DoJump()
    {
        Debug.Log("Jump");
        if(jumpAble)
        {
            jumpAble = false;
            animator.SetBool("isJumping", true);
            player.isJumping = true;
        }
    }
}
