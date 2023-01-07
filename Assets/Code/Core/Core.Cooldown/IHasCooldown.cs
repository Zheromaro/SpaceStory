using UnityEngine;

namespace SpaceGame.Core.Cooldown
{
    public interface IHasCooldown
    {
        int Id { get; }
        float CooldownDuration { get; }
    }
}
