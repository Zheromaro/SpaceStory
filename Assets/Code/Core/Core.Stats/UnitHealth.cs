using UnityEngine;
using SpaceGame.Core.GameEvent;
using SpaceGame.Core.Cooldown;

namespace SpaceGame.Core.Stats
{
    public class UnitHealth: IHasCooldown
    {
        // Fields
        int _currentHealth, _maxHealth;
        bool _isDead, _doDefence;

        public System_Cooldown _cooldownSystem;
        public int _id;
        public float _cooldownDuration;

        public VoidEvent _onPlayerDied;
        public VoidEvent _onPlayerHealed;
        public GameObjectEvent _onPlayerDamaged;

        // Properties
        public int Health
        {
            get
            {
                return _currentHealth;
            }
            set
            {
                _currentHealth = value;
            }
        }

        public int MaxHealth
        {
            get
            {
                return _maxHealth;
            }
            set
            {
                _maxHealth = value;
            }
        }

        public bool DoDefence
        {
            get
            {
                return _doDefence;
            }
            set
            {
                _doDefence = value;
            }
        }

        public int Id => _id;
        public float CooldownDuration => _cooldownDuration;


        // Constructor
        public UnitHealth(int health, int maxHealth,float cooldownDuration)
        {
            _cooldownDuration = cooldownDuration;
            _currentHealth = health;
            _maxHealth = maxHealth;
            _doDefence = false;
            _isDead = false;
        }

        // Methods
        public void DmgUnit(int dmgAmount, GameObject gameObject)
        {
            if (_isDead || _doDefence || _cooldownSystem.IsOnCooldown(_id)) { return; }

            _cooldownSystem.PutOnCooldown(this);
            _currentHealth -= dmgAmount;
            _onPlayerDamaged.Raise(gameObject);

            if (_currentHealth <= 0)
            {
                _onPlayerDied.Raise();
                _isDead = true;
                GameObject.FindGameObjectWithTag("Player").SetActive(false);
            }
        }

        public void HealUnit(int healAmount)
        {
            if (_currentHealth < _maxHealth)
            {
                _currentHealth += healAmount;
                _onPlayerHealed.Raise();
            }

            if (_currentHealth > _maxHealth)
            {
                _currentHealth = _maxHealth;
            }
        }

    }
}