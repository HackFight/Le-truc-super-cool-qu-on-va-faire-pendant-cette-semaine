using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBase : MonoBehaviour
{

    public bool isMusicActive = true;

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("DataBase");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
}
