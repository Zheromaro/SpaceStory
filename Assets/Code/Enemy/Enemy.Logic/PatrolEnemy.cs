using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame.Enemy.Logic
{
    public class PatrolEnemy : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float waitTime;
        [SerializeField] private Transform[] patrolPoints;

        private int currentPointIndex;
        private bool once;

        private void Update()
        {
            if (transform.position != patrolPoints[currentPointIndex].position)
            {
                transform.position = Vector2.MoveTowards(transform.position, patrolPoints[currentPointIndex].position, speed * Time.deltaTime);
            }
            else
            {
                if (once == false)
                {
                    once = true;
                    StartCoroutine(Wait());
                }
            }
        }

        IEnumerator Wait()
        {
            yield return new WaitForSeconds(waitTime);
            if (currentPointIndex + 1 < patrolPoints.Length)
            {
                currentPointIndex++;
            }
            else
            {
                currentPointIndex = 0;
            }
            once = false;
        }

    }
}