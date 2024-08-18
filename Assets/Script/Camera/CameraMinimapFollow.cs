using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMinimapFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    void Update()
    {
        MoveFollowPlayer();
    }
    void MoveFollowPlayer()
    {
        Vector3 newPosition = transform.position;

        // Cập nhật vị trí X và Z của camera theo vị trí của nhân vật 
        newPosition.x = player.position.x;
        newPosition.z = player.position.z;

        // Đặt vị trí mới cho camera
        transform.position = newPosition;
    }
}
