using UnityEngine;

namespace GameEvent
{
    [CreateAssetMenu(fileName = "New Void Event", menuName = "GameEvent/VoidEvent")]
    public class VoidEvent : BaseGameEvent<Void>
    {
        public void Raise() => Raise(new Void());
    }
}
