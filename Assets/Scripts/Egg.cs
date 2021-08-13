using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{

    public bool isGrabed = false;
    public bool canBeGrabed = true;

    private GameManager gameManager;

    private Vector3 initialPosition;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        initialPosition = transform.position;
    }

    private void Update()
    {
        if (isGrabed)
        {
            gameManager.IncrementChrono();
        }
    }

    public void Reset()
    {
        transform.position = initialPosition;

        gameObject.SetActive(true);

        isGrabed = false;
        canBeGrabed = true;
    }
}
