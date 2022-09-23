using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEff : MonoBehaviour
{
    private void FixedUpdate()
    {
        StartCoroutine(Destroy());
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
}
