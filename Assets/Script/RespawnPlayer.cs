using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPlayer : MonoBehaviour
{
    [SerializeField] private Transform respawnPointPosition;
    // Start is called before the first frame update
    [SerializeField] private GameObject map;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            map.SetActive(true);
            other.gameObject.transform.position = respawnPointPosition.position;
        }
    }
}
