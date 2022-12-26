namespace SpaceGame.Core.GameEvent
{
    public interface IGameEventListener<T>
    {
        void OnEventRaised(T item);
    }
}
