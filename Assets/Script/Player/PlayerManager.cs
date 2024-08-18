using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public PlayerProfile playerProfile;
    [SerializeField] private PlayerInteract playerInteract;
    [SerializeField] private StateManager stateManager;
    [SerializeField] private PlayerStatsManager playerStatsManager;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
        }
    }
 
}

    


