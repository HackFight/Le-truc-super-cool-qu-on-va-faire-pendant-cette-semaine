using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float chrono;
    private Egg eggScript;
    void Start()
    {

        

    }

    void Update()
    {

        Debug.Log(chrono);
        
    }

    public void IncrementChrono()
    {
        chrono += Time.deltaTime;
    }
}
