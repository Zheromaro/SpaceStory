using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class HideInRunTime : MonoBehaviour
    {
        private SpriteRenderer forWiningLine;

        private void Start()
        {
            forWiningLine = GetComponent<SpriteRenderer>();

            Color tmp = forWiningLine.color;
            tmp.a = 0f;

            forWiningLine.color = tmp;
        }
    }
}
