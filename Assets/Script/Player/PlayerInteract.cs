using System.Collections;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private GameObject interactCollider;
    public void ActiveInteractCollider()
    {
        interactCollider.SetActive(true);
        StartCoroutine(DeActiveInteractCollier());
    }

    IEnumerator DeActiveInteractCollier()
    {
        yield return new WaitForSeconds(1.0f);
        interactCollider.SetActive(false);   
    }
}

