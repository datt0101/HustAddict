//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PlayerMovement : MonoBehaviour
//{
//    // Start is called before the first frame update
    
//    [SerializeField] private float moveSpeed = 3f;
//    [SerializeField] private float rotateSpeed = 50f;
//    [SerializeField] private float gravity = -9.81f;
//    [SerializeField] private float jumpHeight = 0.5f;

//    //private CharacterController controller;
//    private Vector3 moveDirection;
//    private Vector3 move;
//    private bool isGrounded;

//    void Start()
//    {
        
//    }

//    void Update()
//    {
//        Moving();
//        Turning();
//        Jumping();
//    }
//    public void Moving()
//    {
//        isGrounded = PlayerManager.instance.playerController.isGrounded;

//        if (isGrounded && moveDirection.y < 0)
//        {
//            moveDirection.y = 0f;
//        }

//        float horizontalInput = Input.GetAxis("Horizontal");
//        float verticalInput = Input.GetAxis("Vertical");
//        move = PlayerManager.instance.playerController.transform.right * horizontalInput + PlayerManager.instance.playerController.transform.forward * verticalInput;
//        PlayerManager.instance.playerController.Move(move * moveSpeed * Time.deltaTime);
//        moveDirection.y += gravity * Time.deltaTime;
//        PlayerManager.instance.playerController.Move(moveDirection * Time.deltaTime);
//    }
//    public void Turning()
//    {
//        if (move != Vector3.zero)
//        {
//            Quaternion newRotation = Quaternion.LookRotation(move);
//            PlayerManager.instance.playerController.transform.rotation = Quaternion.RotateTowards(PlayerManager.instance.playerController.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
//        }
//    }
//    public void Jumping()
//    {
//        if (Input.GetButtonDown("Jump") && isGrounded)
//        {
//            moveDirection.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
//        }
//    }
//}

