using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaUIvisual : MonoBehaviour
{
    private const float USED_STAMINA_SHRINK_TIMER_MAX = 1f;

    private Image staminaImage;
    private Image usedStaminaImage;
    private CanvasGroup canvasGroup;
    private float usedStaminaShrinkTimer;

    private void Awake()
    {
        staminaImage = transform.Find("Stamina").GetComponent<Image>();
        usedStaminaImage = transform.Find("UsedStamina").GetComponent<Image>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        usedStaminaImage.fillAmount = staminaImage.fillAmount;
        canvasGroup.alpha = 0f;
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
        StartCoroutine(FadeInOut());
    }

    public void UseStamina(float StaminaNormalized)
    {
        staminaImage.fillAmount = StaminaNormalized;
        usedStaminaShrinkTimer = USED_STAMINA_SHRINK_TIMER_MAX;

    }

    public void StaminaBack(float StaminaNormalized)
    {
        staminaImage.fillAmount = StaminaNormalized;
        usedStaminaImage.fillAmount = staminaImage.fillAmount;
    }

    private IEnumerator FadeInOut()
    {
        if (GameManager.gameManager._PlayerStamina.FadeIn == true)
        {
            if (canvasGroup.alpha < 1)
            {
                canvasGroup.alpha += 4f * Time.deltaTime;
                if (canvasGroup.alpha >= 1)
                {
                    GameManager.gameManager._PlayerStamina.FadeIn = false;
                    GameManager.gameManager._PlayerStamina.FadeOut = true;
                }
            }
        }

        if (GameManager.gameManager._PlayerStamina.FadeOut == true)
        {
            yield return new WaitForSeconds(0.5f);
            if (canvasGroup.alpha >= 0)
            {
                canvasGroup.alpha -= 4f * Time.deltaTime;
                if(canvasGroup.alpha == 0)
                {
                    GameManager.gameManager._PlayerStamina.FadeOut = false;
                }
            }
        }
    }

}
