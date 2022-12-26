using UnityEngine;

namespace SpaceGame.Core
{
    public class UnitStamina
    {
        // Fields
        float _currentStamina;
        float _currentMaxStamina;
        bool _isSprint;
        bool _fadeOut;
        bool _fadeIn;

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

        public bool IsSprint
        {
            get
            {
                return _isSprint;
            }
            set
            {
                _isSprint = value;
            }
        }

        public bool FadeOut
        {
            get
            {
                return _fadeOut;
            }
            set
            {
                _fadeOut = value;
            }
        }

        public bool FadeIn
        {
            get
            {
                return _fadeIn;
            }
            set
            {
                _fadeIn = value;
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
                FadeIn = true;
                FadeOut = false;
            }
        }

        public void UseStamina(float staminaAmount)
        {
            if (_currentStamina > 0)
            {
                _currentStamina -= staminaAmount;
                FadeIn = true;
                FadeOut = false;
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
            FadeIn = true;
            FadeOut = false;
        }
    }
}