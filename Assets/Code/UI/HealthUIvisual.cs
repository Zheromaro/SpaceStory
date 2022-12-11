using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Core;

namespace UI
{
    public class HealthUIvisual : MonoBehaviour
    {
        [SerializeField] private Image[] healthPoints;
        [SerializeField] private TextMeshProUGUI healthText;

        void Update()
        {
            healthText.text = "%" + GameManager.gameManager._PlayerHealth.Health;
            HealthBarFillter();
        }

        public void HealthBarFillter()
        {
            for (int i = 0; i < healthPoints.Length; i++)
            {
                healthPoints[i].enabled = !DisplayHealthPoint(GameManager.gameManager._PlayerHealth.Health, i);
            }
        }

        bool DisplayHealthPoint(float _health, int pointNumber)
        {
            return ((pointNumber * 10) >= _health);
        }
    }
}