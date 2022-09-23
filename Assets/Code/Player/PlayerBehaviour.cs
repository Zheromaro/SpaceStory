using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    private float health;

    private void Start()
    {
        transform.position = GameManager.gameManager.lastCheckPointPos;

        GameManager.gameManager._PlayerHealth.Target = transform;
        health = GameManager.gameManager._PlayerHealth.Health;
    }

    private void Update()
    {
        EffectsForHealth();
    }

    private void EffectsForHealth()
    {
        if (health > GameManager.gameManager._PlayerHealth.Health)
        {
            GameManager.gameManager.DoStopMotion(0.1f);
            StartCoroutine(WaitForTimeToBack());
            health = GameManager.gameManager._PlayerHealth.Health;
        }
        else if (health < GameManager.gameManager._PlayerHealth.Health)
        {
            health = GameManager.gameManager._PlayerHealth.Health;
        }

        if (GameManager.gameManager._PlayerHealth.Health <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private IEnumerator WaitForTimeToBack()
    {
        while (GameManager.gameManager.waiting == true)
            yield return null;

        GameManager.gameManager.DoSlowMotion();
    }

}
