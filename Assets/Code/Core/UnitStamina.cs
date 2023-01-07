using UnityEngine;
using SpaceGame.Core.GameEvent;

namespace SpaceGame.Core
{
    public class UnitStamina
    {
        // Event
        public VoidEvent onStaminaChanged;

        // Fields
        float _currentStamina;
        float _currentMaxStamina;

        // Properties
        public float Stamina
        {
            get
            {
                return _currentStamina;
            }
            set
            {
                _currentStamina = value;
            }
        }

        public float MaxStamina
        {
            get
            {
                return _currentMaxStamina;
            }
            set
            {
                _currentMaxStamina = value;
            }
        }


        // Constructor
        public UnitStamina(float stamina, float maxStamina)
        {
            _currentStamina = stamina;
            _currentMaxStamina = maxStamina;
        }

        // Methods
        public void UseStaminaByTime(float staminaAmount)
        {
            if (_currentStamina > 0)
            {
                _currentStamina -= staminaAmount * Time.deltaTime;
            }
        }

        public void UseStamina(float staminaAmount)
        {
            if (_currentStamina > 0)
            {
                _currentStamina -= staminaAmount;
                onStaminaChanged.Raise();
            }
        }

        public void RegenStamina(float staminaRegenAmount)
        {
            if (_currentStamina < _currentMaxStamina)
            {
                _currentStamina += staminaRegenAmount;
            }

            if (_currentStamina > _currentMaxStamina)
            {
                _currentStamina = _currentMaxStamina;
            }

            onStaminaChanged.Raise();
        }
    }
}