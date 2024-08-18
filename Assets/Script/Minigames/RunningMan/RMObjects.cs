using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RMObjects : MonoBehaviour
{
    public float speed;
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        rb.velocity = new Vector3(speed, 0f, 0f);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {

        RMGameManager.instance.StopInvoke();
        UI.instance.GameOver();
        }
    }
}
