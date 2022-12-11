namespace GameEvent
{
    public interface IGameEventListener<T>
    {
        void OnEventRaised(T item);
    }
}
