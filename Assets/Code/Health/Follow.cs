using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField] private float MinModifier;
    [SerializeField] private float MaxModifier;
    [SerializeField] private bool heal;
    [SerializeField] private bool stamina;

    Vector2 _velocity = Vector2.zero;
    bool _isFollowing = false;
    private GameObject player;
    private Transform target;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform;
    }

    void FixedUpdate()
    {
        if (_isFollowing)
        {
            transform.position = Vector2.SmoothDamp(transform.position, target.position, ref _velocity, Time.deltaTime * Random.Range(MinModifier, MaxModifier));
        }     
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (stamina == true)
            {
                PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
                playerMovement.BackStamina();
                Destroy(transform.gameObject);
            }

            if (heal == true)
            {
                PlayerStatsSystem playerStats = player.GetComponent<PlayerStatsSystem>();
                playerStats.Heal(20);
                Destroy(transform.gameObject);
            }
        }
    }

    public void StartFollowing()
    {
        _isFollowing = true;
    }
}
