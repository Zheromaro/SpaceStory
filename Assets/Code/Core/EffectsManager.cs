using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame
{
    public class EffectsManager : MonoBehaviour
    {
        public static EffectsManager effectsManager;

        [Header("for Cameras")]
        [SerializeField] private float DefaultBlend;
        [SerializeField] private float reduseCameraView;
        [SerializeField] private float increaseCameraView;
        [HideInInspector] public float originalCameraView;

        private void Awake()
        {
            if (effectsManager != null && effectsManager != this)
            {
                Destroy(gameObject);
            }
            else
            {
                effectsManager = this;
            }
        }

        public IEnumerator ReduseCameraView()
        {
            if (roomManager.VirtualCamera == null)
                yield break;

            while(roomManager.VirtualCamera.m_Lens.OrthographicSize > reduseCameraView)
            {
                roomManager.VirtualCamera.m_Lens.OrthographicSize -= DefaultBlend * Time.deltaTime;
                yield return null;
            }

            roomManager.VirtualCamera.m_Lens.OrthographicSize = reduseCameraView;
        }

        public IEnumerator IncreaseCameraView()
        {
            if (roomManager.VirtualCamera == null)
                yield break;

            while (roomManager.VirtualCamera.m_Lens.OrthographicSize < increaseCameraView)
            {
                roomManager.VirtualCamera.m_Lens.OrthographicSize += DefaultBlend * Time.deltaTime;
                yield return null;
            }

            roomManager.VirtualCamera.m_Lens.OrthographicSize = increaseCameraView;
        }

        public IEnumerator NormalCameraView()
        {
            if (roomManager.VirtualCamera == null)
                yield break;

            if (roomManager.VirtualCamera.m_Lens.OrthographicSize < originalCameraView)
            {
                while (roomManager.VirtualCamera.m_Lens.OrthographicSize < originalCameraView)
                {
                    roomManager.VirtualCamera.m_Lens.OrthographicSize += DefaultBlend * Time.deltaTime;
                    yield return null;
                }
            }
            else if (roomManager.VirtualCamera.m_Lens.OrthographicSize > originalCameraView)
            {
                while (roomManager.VirtualCamera.m_Lens.OrthographicSize > originalCameraView)
                {
                    roomManager.VirtualCamera.m_Lens.OrthographicSize -= DefaultBlend * 3 * Time.deltaTime;
                    yield return null;
                }
            }

            roomManager.VirtualCamera.m_Lens.OrthographicSize = originalCameraView;
        }

    }
}
