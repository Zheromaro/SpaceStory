using UnityEngine;

namespace SpaceGame.Core.GameEvent
{
    [CreateAssetMenu(fileName = "New Void Event", menuName = "GameEvent/GameObjectEvent")]
    public class GameObjectEvent : BaseGameEvent<GameObject> { }
}
