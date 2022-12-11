using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;
using Player;

public class EffectTheSpeed : MonoBehaviour //, IDontDestroy
{
    [SerializeField] private float AddToSpeed;
    private static float AreInColision = 0;

    private void Start()
    {
        Debug.Log("DontForgetMe");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerMovement.backToNormalSpeed = false;
            if (AreInColision < 1)
            {
                PlayerMovement.theTrueSpeed += AddToSpeed;
            }

            AreInColision += 1;
        }
    }

    private IEnumerator OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AreInColision -= 1;

            if(AreInColision < 0)
            {
                AreInColision = 0;
            }

            if (AreInColision == 0)
            {
                yield return new WaitForSeconds(0.1f);
                PlayerMovement.backToNormalSpeed = true;
            }
        }
    }

}
