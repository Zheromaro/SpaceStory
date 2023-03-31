using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame
{
    public class ParticalsManager : MonoBehaviour
    {
        public static ParticalsManager particalsManager;

        [SerializeField] private ParticleSystem[] particles;

        private void Awake()
        {
            if (particalsManager != null && particalsManager != this)
            {
                Destroy(gameObject);
            }
            else
            {
                particalsManager = this;
            }
        }

        private void Start()
        {
            for (int i = 0; i < particles.Length; i++)
            {
                particles[i].Stop();
            }
        }

        public void Play(string name)
        {
            for (int i = 0; i < particles.Length; i++)
            {
                if (particles[i].name == name)
                {
                    particles[i].Play();
                }
            }
        }

        public void Stop(string name)
        {
            for (int i = 0; i < particles.Length; i++)
            {
                if (particles[i].name == name)
                {
                    particles[i].Stop();
                }
            }
        }
    }
}
