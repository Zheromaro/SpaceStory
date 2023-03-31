using UnityEngine;
using System.Collections.Generic;
using TMPro;
using SpaceGame.Core.Stats;

namespace SpaceGame.UI
{
    public class HealthUIvisual : MonoBehaviour
    {
        [SerializeField] private List<GameObject> healthPoints;
        [SerializeField] private Transform healthPointsParent;
        [SerializeField] protected GameObject healthPoint;
        [SerializeField] private TextMeshProUGUI healthText;

        private int healthPointsNum; 

        private void Start()
        {
            healthPointsNum = healthPoints.Count;
            if (healthPointsNum < StatsManager.statsManager._PlayerHealth.Health)
            {
                AddHealthPoint();
            }
            else if (healthPointsNum > StatsManager.statsManager._PlayerHealth.Health)
            {
                RemoveHealthPoint();
            }
        }

        public void RemoveHealthPoint()
        {
            healthText.text = DisplayHealthPercentage();

            int difference = healthPointsNum - StatsManager.statsManager._PlayerHealth.Health;
            for (int i = 0; i < difference; i++)
            {
                GameObject oldObject = healthPoints[0].gameObject; // get an object in the list
                Destroy(oldObject); // destroy the last object
                healthPoints.Remove(oldObject);
                healthPointsNum--;
            }
        }

        public void AddHealthPoint()
        {
            healthText.text = DisplayHealthPercentage();

            int difference = StatsManager.statsManager._PlayerHealth.Health - healthPoints.Count;
            for (int i = 0; i < difference; i++)
            {
                GameObject newObject = Instantiate(healthPoint, healthPointsParent);
                healthPoints.Add(newObject); // add a copy of the first element to the list
                healthPointsNum++;
            }
        }

        string DisplayHealthPercentage()
        {
            float totalPercentage = (StatsManager.statsManager._PlayerHealth.Health * 100) / StatsManager.statsManager._PlayerHealth.MaxHealth;
            return "%" + totalPercentage;
        }
    }
}