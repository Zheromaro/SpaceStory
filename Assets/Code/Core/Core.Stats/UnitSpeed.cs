using UnityEngine;
using SpaceGame.Core.GameEvent;

namespace SpaceGame.Core.Stats
{
    public class UnitSpeed
    {
        // Fields
        float _originalSpeed;
        float _currentSpeed;
        bool _isSprinting;

        // Properties
        public float normalSpeed
        {
            get
            {
                return _originalSpeed;
            }
        }

        public float speed
        {
            get
            {
                return _currentSpeed;
            }
        }

        public bool isSprinting
        {
            get
            {
                return _isSprinting;
            }
        }

        // Constructor
        public UnitSpeed(float speed)
        {
            _currentSpeed = speed;
            _originalSpeed = speed;
            _isSprinting = false;
        }

        // Methods
        public void AddToSpeed(float plusAmount)
        {
            if (_currentSpeed != _originalSpeed)
                _currentSpeed = _originalSpeed;

            _currentSpeed += plusAmount;

            if (_currentSpeed > _originalSpeed)
            {
                StatsManager.statsManager.StartCoroutine(EffectsManager.effectsManager.ReduseCameraView());
                _isSprinting = true;
            }
            else
            {
                StatsManager.statsManager.StartCoroutine(EffectsManager.effectsManager.IncreaseCameraView());
            }
        }

        public void BackToOriSpeed()
        {
            _currentSpeed = _originalSpeed;

            StatsManager.statsManager.StopAllCoroutines();
            StatsManager.statsManager.StartCoroutine(EffectsManager.effectsManager.NormalCameraView());
            _isSprinting = false;
        }

    }
}