using SpaceGame.Core.SaveSystem;
using SpaceGame.NotImportant;
using UnityEngine;
using Cinemachine;

namespace SpaceGame
{
    public class roomManager : MonoBehaviour, IDataPersistence, Iuntouchable
    {
        public static CinemachineVirtualCamera VirtualCamera;
        [SerializeField] private GameObject CM_Camera;
        [SerializeField] private Transform RespawnPoint;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                CM_Camera.SetActive(true);
                VirtualCamera = CM_Camera.GetComponent<CinemachineVirtualCamera>();
                EffectsManager.effectsManager.originalCameraView = VirtualCamera.m_Lens.OrthographicSize;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                CM_Camera.SetActive(false);
            }
        }

        public void LoadData(GameData data)
        {
            // Nothing... 
        }

        public void SaveData(GameData data)
        {
            if(RespawnPoint != null)
                data.playerPosition = RespawnPoint.position;
        }
    }
}
