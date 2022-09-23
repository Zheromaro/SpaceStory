using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour , IDontDestroy
{
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.gameManager;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.lastCheckPointPos = transform.position;
        }
    }
}
