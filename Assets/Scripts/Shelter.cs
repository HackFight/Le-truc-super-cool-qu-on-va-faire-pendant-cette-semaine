using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shelter : MonoBehaviour
{

    public int ShelterLifes = 30;

    private void Update()
    {
        if(ShelterLifes <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            ShelterLifes--;
            Destroy(collision.gameObject);
        }
    }
}
