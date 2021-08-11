using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shock : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.gameObject != transform.parent)
        {

            collision.GetComponent<PlayerMovement>().playerLifes--;

        }
    }
}
