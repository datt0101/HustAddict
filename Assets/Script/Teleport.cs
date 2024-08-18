using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Teleport : MonoBehaviour
{
    [SerializeField] private GameObject goOut;
    [SerializeField] private GameObject goIn;
    [SerializeField] Transform gateOut;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            goIn.SetActive(true);
            UIManager.instance.blackOut.SetActive(true);
            UIManager.instance.blackOut.GetComponent<Image>().color = new Color(0, 0, 0, 0f);
            UIManager.instance.blackOut.GetComponent<Image>().DOColor(new Color(0, 0, 0, 1), 1f)
                .OnComplete(() =>
                {
                    UIManager.instance.BlackIn();
                    other.transform.position = gateOut.position;
                    goOut.SetActive(false);
                });

            
        }
        
    }
}
