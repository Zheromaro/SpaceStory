using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    public void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        GameObject bullet = ObjectPool.instance.GetPooledObject();

        if (bullet != null)
        {
            bullet.transform.rotation = firePoint.rotation;
            bullet.transform.position = firePoint.position;
            bullet.SetActive(true);
        }
    }
}
