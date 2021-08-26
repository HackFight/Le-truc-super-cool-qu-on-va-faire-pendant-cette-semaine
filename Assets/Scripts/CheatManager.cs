using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatManager : MonoBehaviour
{
    // Cache 
    private PlayerManager playerManager;

    void Start()
    {
        
    }

    void Update()
    {
        if (playerManager == null)
        {
            playerManager = FindObjectOfType<PlayerManager>();
        }

        // Add player
        if (Input.GetKeyDown(KeyCode.A))
        {
            playerManager.CheatCreatePlayer();
        }

        // Kill player
        if (Input.GetKeyDown(KeyCode.K))
        {
            playerManager.CheatKillPlayer();
        }

        // Turn player into a monster
        if (Input.GetKeyDown(KeyCode.M))
        {
            playerManager.CheatTurnPlayerIntoMonster();
        }
    }
}
