using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToCollect : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        
        if ((other.tag == "Player" || other.tag == "PlayerInteract") && GetComponentInParent<CollectObjectsParent>().quest.QuestProgress == QuestProgress.Accepted)
        {
            Debug.Log(gameObject.name);
            GetComponentInParent<CollectObjectsParent>().Collected();
            GetComponentInChildren<ParticleSystem>().Play();
            AudioManager.instance.PlayCollectItem();
            //GetComponent<MeshRenderer>().enabled = false;
            gameObject.SetActive(false);
        }
    }
    //private void Respawn()
    //{
    //    gameObject.SetActive(true);
    //}
}
