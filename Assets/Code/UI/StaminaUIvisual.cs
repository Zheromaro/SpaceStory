using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SpaceGame.Core;

namespace SpaceGame.UI
{
    public class StaminaUIvisual : MonoBehaviour
    {
        [SerializeField] private float DisapearTime = 0.5f;
        private const float USED_STAMINA_SHRINK_TIMER_MAX = 1f;

        private Image staminaImage;
        private Image usedStaminaImage;
        private CanvasGroup canvasGroup;
        private float usedStaminaShrinkTimer;

        private float stamina;
        private float lerpSpeed;

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
            stamina = GameManager.gameManager._PlayerStamina.Stamina;
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

            lerpSpeed = 3f * Time.deltaTime;

            UseAndBackStamina();

            StartCoroutine(FadeInOut());
        }

        private void UseAndBackStamina()
        {
            if (stamina > GameManager.gameManager._PlayerStamina.Stamina)
            {
                UseStamina();
                stamina = GameManager.gameManager._PlayerHealth.Health;
            }
            else if (stamina < GameManager.gameManager._PlayerStamina.Stamina)
            {
                StaminaBack();
                stamina = GameManager.gameManager._PlayerHealth.Health;
            }
        }

        public void UseStamina()
        {
            staminaImage.fillAmount = Mathf.Lerp(staminaImage.fillAmount, GetStaminaNormalized(), lerpSpeed); ;
            usedStaminaShrinkTimer = USED_STAMINA_SHRINK_TIMER_MAX;

        }

        public void StaminaBack()
        {
            staminaImage.fillAmount = Mathf.Lerp(staminaImage.fillAmount, GetStaminaNormalized(), lerpSpeed); ;
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
                yield return new WaitForSeconds(DisapearTime);
                if (canvasGroup.alpha >= 0)
                {
                    usedStaminaImage.fillAmount = staminaImage.fillAmount;
                    canvasGroup.alpha -= 4f * Time.deltaTime;
                    if (canvasGroup.alpha == 0)
                    {
                        GameManager.gameManager._PlayerStamina.FadeOut = false;
                    }
                }
            }
        }

        private float GetStaminaNormalized()
        {
            return (float)GameManager.gameManager._PlayerStamina.Stamina / GameManager.gameManager._PlayerStamina.MaxStamina;
        }

    }
}