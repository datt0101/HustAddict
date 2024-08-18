using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feet : MonoBehaviour
{
    public ThirdPersonController player;
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ground")
        {
            player.isGrounded = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Ground")
        {
            player.isGrounded = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ground")
        {
            player.isGrounded = false;
        }
    }
}
