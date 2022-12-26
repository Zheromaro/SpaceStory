using UnityEngine;

namespace SpaceGame.Core.ObjectPooling
{
    public interface IPool<T>
    {
        T Pull();

        void Puch(T t);
    }
}
