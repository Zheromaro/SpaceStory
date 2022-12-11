using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;

public class itemFollow : MonoBehaviour
{
    [SerializeField] private float MinModifier;
    [SerializeField] private float MaxModifier;
    [SerializeField] private bool heal;
    [SerializeField] private bool stamina;

    Vector2 _velocity = Vector2.zero;
    private Transform target;

    private void Start()
    {
        target = GameManager.gameManager._PlayerHealth.Target;
    }

    void FixedUpdate()
    {
        transform.position = Vector2.SmoothDamp(transform.position, target.position, ref _velocity, Time.deltaTime * Random.Range(MinModifier, MaxModifier));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (stamina == true)
            {
                GameManager.gameManager._PlayerStamina.RegenStamina(33f);
            }

            if (heal == true)
            {
                GameManager.gameManager._PlayerHealth.HealUnit(20);
            }
            gameObject.SetActive(false);
        }
    }
}