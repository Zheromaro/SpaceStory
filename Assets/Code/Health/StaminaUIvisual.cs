using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaUIvisual : MonoBehaviour
{
    [SerializeField] private float FadeTime = 1f;
    private const float USED_STAMINA_SHRINK_TIMER_MAX = 1f;

    private Image staminaImage;
    private Image usedStaminaImage;
    private Image BGStaminaImage;
    private float usedStaminaShrinkTimer;
    private PlayerMovement playerMovement;

    private void Awake()
    {
        staminaImage = transform.Find("Stamina").GetComponent<Image>();
        usedStaminaImage = transform.Find("UsedStamina").GetComponent<Image>();
        BGStaminaImage = transform.Find("Stamina BG").GetComponent<Image>();
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
        usedStaminaShrinkTimer -= Time.deltaTime;
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
        StartCoroutine(FadeInOut());
    }

    private void PlayerMovement_OnDashBack(object sender, System.EventArgs e)
    {
        SetStamina(playerMovement.GetStaminaNormalized());
        usedStaminaImage.fillAmount = staminaImage.fillAmount;
        StartCoroutine(FadeInOut());
    }

    private void SetStamina(float StaminaNormalized)
    {
        staminaImage.fillAmount = StaminaNormalized;
    }

    private IEnumerator FadeInOut()
    {
        staminaImage.color = new Color(staminaImage.color.r, staminaImage.color.g, staminaImage.color.b, 1);
        usedStaminaImage.color = new Color(usedStaminaImage.color.r, usedStaminaImage.color.g, usedStaminaImage.color.b, 1);
        BGStaminaImage.color = new Color(BGStaminaImage.color.r, BGStaminaImage.color.g, BGStaminaImage.color.b, 1);

        yield return new WaitForSeconds(FadeTime);

        staminaImage.color = new Color(staminaImage.color.r, staminaImage.color.g, staminaImage.color.b, 0);
        usedStaminaImage.color = new Color(usedStaminaImage.color.r, usedStaminaImage.color.g, usedStaminaImage.color.b, 0);
        BGStaminaImage.color = new Color(BGStaminaImage.color.r, BGStaminaImage.color.g, BGStaminaImage.color.b, 0);
    }
}
