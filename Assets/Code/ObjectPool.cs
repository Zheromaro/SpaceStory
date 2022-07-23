using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;

    private List<GameObject> pooledObjects = new List<GameObject>();
    private List<GameObject> pooledEffects = new List<GameObject>();
    private int amountToPool = 20;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject EffectPrefab;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        for (int i = 0; i < amountToPool; i++) {
            GameObject obj = Instantiate(bulletPrefab);
            GameObject eff = Instantiate(EffectPrefab);
            obj.SetActive(false);
            eff.SetActive(false);
            pooledObjects.Add(obj);
            pooledEffects.Add(eff);
        }
    }

    public GameObject GetPooledObject()
    {
        for ( int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        return null;
    }
    public GameObject GetPooledEffect()
    {
        for (int i = 0; i < pooledEffects.Count; i++)
        {
            if (!pooledEffects[i].activeInHierarchy)
            {
                return pooledEffects[i];
            }
        }

        return null;
    }
}
