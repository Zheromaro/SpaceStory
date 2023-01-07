using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame
{
    public class EffectManager : MonoBehaviour
    {
        public static EffectManager effectManager;

        [SerializeField] private ParticleSystem[] particles;

        private void Awake()
        {
            if (effectManager != null && effectManager != this)
            {
                Destroy(gameObject);
            }
            else
            {
                effectManager = this;
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
