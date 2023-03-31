using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame.Core.GameEvent
{
    public class GameObjectListener : MonoBehaviour
    {
        [System.Serializable]
        class GameObjectListeners : BaseGameEventListener<GameObject, GameObjectEvent, UnityGameObjectEvent> {}

        [SerializeField] private GameObjectListeners[] voidListeners;

        private void OnEnable()
        {
            foreach (var listener in voidListeners)
            {
                listener.OnEnable();
            }
        }

        private void OnDisable()
        {
            foreach (var listener in voidListeners)
            {
                listener.OnDisable();
            }
        }
    }
}