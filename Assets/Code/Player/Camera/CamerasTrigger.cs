using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerasTrigger : MonoBehaviour
{
    public event EventHandler playerIsHere;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIsHere?.Invoke(this, EventArgs.Empty);
        }
    }
}
