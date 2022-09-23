using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Animator ShootingAnim;
    [SerializeField] private Transform firePosition;
    [SerializeField] private KeyCode key;
    [SerializeField] private float fireRate;
    [SerializeField] private float DisappearRate;

    private float fireTime = 0f;
    private float DisappearTime = 0f;
    private PlayerMovement playerMovement;
    private ObjectPooler objectPooler;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        objectPooler = ObjectPooler.Instance;
    }

    void Update()
    {
        DisappearTime += Time.deltaTime * DisappearRate;

        ShootingAnim.SetFloat("Horizontal", playerMovement.movement.x);
        ShootingAnim.SetFloat("Vertical", playerMovement.movement.y);

        if (Input.GetKeyDown(key))
        {
            ShootingAnim.SetBool("Switch", !ShootingAnim.GetBool("Switch"));
        }

        fireTime += Time.deltaTime;
        if (Input.GetButtonDown("Fire1") && fireTime > fireRate)
        {
            fireTime = 0f;
            Shoot();
        }

        if(ShootingAnim.GetBool("Do waveAttack") == true)
        {
            DisappearTime = 0f;
        }

        ShootingAnim.SetFloat("Not Moving for", DisappearTime);
    }

    private void Shoot()
    {
        DisappearTime = 0f;

        ShootingAnim.SetBool("IsShooting", true);
        objectPooler.SpawnFromPool("bullet", firePosition.position, firePosition.rotation);
    }
}
