using UnityEngine;

namespace SpaceGame.Core.GameEvent
{
    [CreateAssetMenu(fileName = "New Void Event", menuName = "GameEvent/VoidEvent")]
    public class VoidEvent : BaseGameEvent<Void>
    {
        public void Raise() => Raise(new Void());
    }
}
