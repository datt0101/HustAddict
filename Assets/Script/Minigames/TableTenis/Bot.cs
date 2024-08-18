using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bot : MonoBehaviour
{
    [SerializeField] private float speed = 5; // moveSpeed

    [SerializeField] private Transform ball;
    [SerializeField] private Transform aimTarget; 

    [SerializeField] private Transform[] targets; 


    [SerializeField] private float forceZ = 10; // ball force z
    [SerializeField] private float forceY = 4; // ball  force z
    Vector3 targetPosition; // position to where the bot will want to move

   

    void Start()
    {
        targetPosition = transform.position; 
      
    }

    void Update()
    {
        Moving(); 
    }

    void Moving()
    {
        targetPosition.x = ball.position.x;
        targetPosition.y = ball.position.y;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }
    Vector3 PickTarget() 
    {
        int randomValue = Random.Range(0, targets.Length); 
        return targets[randomValue].position; 
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ball")) 
        {
            Debug.Log("Hit");
            Vector3 dir = PickTarget() - transform.position; 
            collision.gameObject.GetComponent<Rigidbody>().velocity = dir.normalized * forceZ + new Vector3(0, forceY, -forceZ); 

        }
    }
}
