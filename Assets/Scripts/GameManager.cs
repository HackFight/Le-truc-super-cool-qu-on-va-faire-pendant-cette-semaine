using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float chrono;
    private Egg eggScript;
    public float timeToOpenEgg = 30.0f;

    void Start()
    {

        

    }

    void Update()
    {
        if (chrono >= timeToOpenEgg)
        {
            Debug.Log("YAY!");
        }  
    }

    public void IncrementChrono()
    {
        chrono += Time.deltaTime;
    }
}
