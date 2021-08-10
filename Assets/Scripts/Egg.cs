using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{

    public bool isGrabed = false;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (isGrabed)
        {
            gameManager.IncrementChrono();
        }
    }
}
