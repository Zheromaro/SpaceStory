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
    private PlayerStatsSystem playerStatsSystem;
    [SerializeField] private GameObject player;

    private void Awake()
    {
        barImage = transform.Find("HealthBar").GetComponent<Image>();
        damagedBarImage = transform.Find("HealthBarDamage").GetComponent<Image>();
    }
    void Start()
    {
        playerStatsSystem = player.GetComponent<PlayerStatsSystem>();
        playerStatsSystem.Health(100);
        SetHealth(playerStatsSystem.GetHealthNormalized());
        damagedBarImage.fillAmount = barImage.fillAmount;

        playerStatsSystem.OnDamage += HealthSystem_OnDamage;
        playerStatsSystem.OnHealed += HealthSystem_OnHealed;
        playerStatsSystem.OnDead += HealthSystem_OnDead;
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

    private void HealthSystem_OnHealed(object sender, System.EventArgs e)
    {
        SetHealth(playerStatsSystem.GetHealthNormalized());
        damagedBarImage.fillAmount = barImage.fillAmount;
    }

    private void HealthSystem_OnDamage(object sender, System.EventArgs e)
    {
        damagedHealthShrinkTimer = DAMAGED_HEALTH_SHRINK_TIMER_MAX;
        SetHealth(playerStatsSystem.GetHealthNormalized());
    }
    private void HealthSystem_OnDead(object sender, System.EventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void SetHealth(float healthNormalized)
    {
        barImage.fillAmount = healthNormalized;
    }
}
