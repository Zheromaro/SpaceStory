using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkWin : MonoBehaviour
{
    public bool PlayerIsHere = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerIsHere = true;
        }
    }
}
