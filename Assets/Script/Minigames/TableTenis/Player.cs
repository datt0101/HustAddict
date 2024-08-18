using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform aimTarget; // the target where we aim to land the ball
    [SerializeField] private float speed = 3f; // tốc độ di chuyển 
    [SerializeField] private float forceX = 4f; // lực đánh phương X
    [SerializeField] private float forceY = 3f; // lực đánh phương Y
    [SerializeField] private float forceZ = 13f; // lực đánh phương Z
    [SerializeField] private InputManagerMinigame inputManagerMinigame;
    private float h;
    void Update()
    {
       Moving();

    }
    void Moving()
    {
        h = inputManagerMinigame.horizontalInput;
        float v = inputManagerMinigame.verticalInput;
        transform.Translate(new Vector3(h, v, 0) * speed * Time.deltaTime); 
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ball"))
        {
            Vector3 dir = aimTarget.position - transform.position;
            collision.gameObject.GetComponent<Rigidbody>().velocity = dir.normalized * forceZ + new Vector3(h * forceX* 0.7f, forceY, forceZ);
        }
    }
}
