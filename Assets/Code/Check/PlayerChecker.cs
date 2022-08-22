using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChecker : MonoBehaviour
{
    [SerializeField] private float visionDistant;
    [SerializeField] private GameObject[] enemyArray;
    [SerializeField] private bool PlayerAbilites = false;

    private LineRenderer lineOfSight;

    private void Start()
    {
        lineOfSight = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        lineOfSight.SetPosition(0, transform.position);

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right, visionDistant);
        if (hitInfo.collider != null)
        {
            Debug.DrawLine(transform.position, hitInfo.point, Color.yellow);
            lineOfSight.SetPosition(1, hitInfo.point);

            if (hitInfo.collider.tag == "Player")
            {
                foreach (GameObject enemy in enemyArray)
                {
                    enemy.SetActive(true);
                }

                if (PlayerAbilites)
                {

                }
            }

        }
        else
        {
            Debug.DrawLine(transform.position, transform.position + transform.right * visionDistant, Color.green);
            lineOfSight.SetPosition(1, transform.position + transform.right * visionDistant);
        }
    }
}
