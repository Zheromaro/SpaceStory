using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemFollow : MonoBehaviour
{
    [SerializeField] private float MinModifier;
    [SerializeField] private float MaxModifier;
    [SerializeField] private bool heal;
    [SerializeField] private bool stamina;

    Vector2 _velocity = Vector2.zero;
    private GameObject player;
    private Transform target;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform;
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
                PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
                playerMovement.BackStamina();
            }

            if (heal == true)
            {
                PlayerBehaviour playerStats = player.GetComponent<PlayerBehaviour>();
                GameManager.gameManager._PlayerHealth.HealUnit(20);
            }
            gameObject.SetActive(false);
        }
    }
}