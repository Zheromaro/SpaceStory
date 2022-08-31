using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUIvisual : MonoBehaviour
{
    private const float DAMAGED_HEALTH_SHRINK_TIMER_MAX = 1f;

    private Image barImage;
    private Image damagedBarImage;
    private float damagedHealthShrinkTimer;

    private void Awake()
    {
        barImage = transform.Find("HealthBar").GetComponent<Image>();
        damagedBarImage = transform.Find("HealthBarDamage").GetComponent<Image>();
    }
    void Start()
    {
        barImage.fillAmount = 1f;
        damagedBarImage.fillAmount = barImage.fillAmount;
    }

    void Update()
    {
        damagedHealthShrinkTimer -= Time.deltaTime;
        if (damagedHealthShrinkTimer < 0)
        {
            if(barImage.fillAmount < damagedBarImage.fillAmount)
            {
                float shrinkSpeed = 1f;
                damagedBarImage.fillAmount -= shrinkSpeed * Time.deltaTime;
            }
        }
    }

    public void Healed(float healAmount)
    {
        barImage.fillAmount = healAmount;
        damagedBarImage.fillAmount = barImage.fillAmount;
    }

    public void Damaged(float damageAmount)
    {
        barImage.fillAmount = damageAmount;
        damagedHealthShrinkTimer = DAMAGED_HEALTH_SHRINK_TIMER_MAX;
    }
}
