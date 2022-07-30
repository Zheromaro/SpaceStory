using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaUIvisual : MonoBehaviour
{
    private const float USED_STAMINA_SHRINK_TIMER_MAX = 1f;

    private Image staminaImage;
    private Image usedStaminaImage;
    private float usedStaminaShrinkTimer;
    private PlayerMovement playerMovement;

    private void Awake()
    {
        staminaImage = transform.Find("Stamina").GetComponent<Image>();
        usedStaminaImage = transform.Find("UsedStamina").GetComponent<Image>();
        playerMovement = transform.parent.GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        playerMovement.Stamina(3);
        SetStamina(playerMovement.GetStaminaNormalized());
        usedStaminaImage.fillAmount = staminaImage.fillAmount;

        playerMovement.OnDash += PlayerMovement_OnDash;
        playerMovement.OnDashBack += PlayerMovement_OnDashBack;
    }

    private void Update()
    {
        usedStaminaShrinkTimer = Time.deltaTime;
        if (usedStaminaShrinkTimer < 0)
        {
            if (staminaImage.fillAmount < usedStaminaImage.fillAmount)
            {
                usedStaminaImage.fillAmount -= 1f * Time.deltaTime;
            }
        }
    }
    private void PlayerMovement_OnDash(object sender, System.EventArgs e)
    {
        usedStaminaShrinkTimer = USED_STAMINA_SHRINK_TIMER_MAX;
        SetStamina(playerMovement.GetStaminaNormalized());
    }
    private void PlayerMovement_OnDashBack(object sender, System.EventArgs e)
    {
        SetStamina(playerMovement.GetStaminaNormalized());
        usedStaminaImage.fillAmount = staminaImage.fillAmount;
    }

    private void SetStamina(float StaminaNormalized)
    {
        staminaImage.fillAmount = StaminaNormalized;
    }
}
