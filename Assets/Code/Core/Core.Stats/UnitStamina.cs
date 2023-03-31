using UnityEngine;
using SpaceGame.Core.GameEvent;
using System.Collections;

namespace SpaceGame.Core.Stats
{
    public class UnitStamina
    {
        // Fields
        float _currentStamina;
        float _currentMaxStamina;

        public VoidEvent _onStaminaChange;
        public VoidEvent _onStaminaStabel;
        public VoidEvent _onStaminaEnded;

        Coroutine coroutine;
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
        public void startUseStaminaByTime(float lossStaminaRate)
        {
            coroutine = StatsManager.statsManager.StartCoroutine(useStaminaByTimeCouritine(lossStaminaRate));
        }
        public void stopUseStaminaByTime()
        {
            if (coroutine != null)
                StatsManager.statsManager.StopCoroutine(coroutine);

            _onStaminaStabel.Raise();
        }

        private IEnumerator useStaminaByTimeCouritine(float staminaAmount)
        {
            _onStaminaChange.Raise();

            while (_currentStamina > 0)
            {
                _currentStamina -= staminaAmount * Time.deltaTime;
                yield return null;
            }

            _onStaminaEnded.Raise();

        }

        public void UseStamina(float staminaAmount)
        {
            if (_currentStamina > 0)
            {
                _currentStamina -= staminaAmount;
                _onStaminaChange.Raise();
            }
            else
            {
                _currentStamina = 0;
                _onStaminaEnded.Raise();
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

            _onStaminaChange.Raise();
        }
    }
}