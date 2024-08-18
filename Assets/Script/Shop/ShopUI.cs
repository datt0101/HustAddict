using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    static public ShopUI instance;

    public TMP_Text money;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);
    }
    public void UpdateMoney()
    {
        money.text = PlayerManager.instance.playerProfile.PlayerCredit.ToString();

    }
}
