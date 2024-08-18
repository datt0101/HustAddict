using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class NpcController : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 3f;
    public float gravity = 9.81f;
    private CharacterController controller;
    private Transform target;
    Vector3 moveDirection;
    [SerializeField] private NavMeshAgent agent;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        target = pointA;
    }

    void FixedUpdate()
    {
        AImoving();
        //Moving();
        //Turning();
    }

    public void Moving()
    {
        Vector3 targetDirection = target.position - transform.position;
       
        if (Distance(target.position,transform.position) < 2f && target == pointA)
        {
           target = pointB;
        }
        if (Distance(target.position, transform.position) < 2f && target == pointB)
        {
            target = pointA;
        }

        moveDirection = new Vector3(targetDirection.x, 0f, targetDirection.z).normalized * speed * Time.deltaTime;
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection);
    }
    public float Distance(Vector3 A, Vector3 B)
    {
        return Mathf.Abs((A.x - B.x) + (A.z - B.z));
    }
    public void Turning()
    {
        if (moveDirection.magnitude > 0.1f)
        {
            Quaternion newRotation = Quaternion.LookRotation(moveDirection);
            newRotation = Quaternion.Euler(0, newRotation.eulerAngles.y, 0);
            controller.transform.rotation = Quaternion.Slerp(controller.transform.rotation, newRotation, Time.deltaTime * 10f);
        }
    }
    public void AImoving()
    {
        if (Distance(target.position, transform.position) < 2f && target == pointA)
        {
            target = pointB;
        }
        if (Distance(target.position, transform.position) < 2f && target == pointB)
        {
            target = pointA;
        }

        agent.destination = target.position;
    }
}