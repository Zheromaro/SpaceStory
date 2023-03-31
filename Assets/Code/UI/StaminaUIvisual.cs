using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using SpaceGame.Core.Stats;

namespace SpaceGame.UI
{
    public class StaminaUIvisual : MonoBehaviour
    {
        private const float USED_STAMINA_SHRINK_TIMER_MAX = 1f;

        private Image staminaImage;
        private Image usedStaminaImage;
        private float usedStaminaShrinkTimer;

        private float stamina;
        private float lerpSpeed;

        private Animator animator;
        private Coroutine coroutine;

        private void Start()
        {
            // Get/Set UI
            staminaImage = transform.Find("Stamina").GetComponent<Image>();
            usedStaminaImage = transform.Find("UsedStamina").GetComponent<Image>();
            usedStaminaImage.fillAmount = staminaImage.fillAmount;

            // Get/Set animator
            animator = GetComponent<Animator>();
            animator.SetBool("FadeIn", false);

            stamina = StatsManager.statsManager._PlayerStamina.Stamina;
        }

        private IEnumerator ChangeStaminaUI()
        {
            while (true)
            {
                float GetStaminaNormalized()
                {
                    return (float)StatsManager.statsManager._PlayerStamina.Stamina / StatsManager.statsManager._PlayerStamina.MaxStamina;
                }

                if (stamina > StatsManager.statsManager._PlayerStamina.Stamina)
                {
                    // ----- Use Stamina ---------------------------------------
                    staminaImage.fillAmount = Mathf.Lerp(staminaImage.fillAmount, GetStaminaNormalized(), lerpSpeed);
                    usedStaminaShrinkTimer = USED_STAMINA_SHRINK_TIMER_MAX;

                    stamina = StatsManager.statsManager._PlayerStamina.Stamina;
                }
                else if (stamina < StatsManager.statsManager._PlayerStamina.Stamina)
                {
                    // ----- Get Stamina ---------------------------------------
                    staminaImage.fillAmount = Mathf.Lerp(staminaImage.fillAmount, GetStaminaNormalized(), lerpSpeed); ;
                    usedStaminaImage.fillAmount = staminaImage.fillAmount;

                    stamina = StatsManager.statsManager._PlayerStamina.Stamina;
                }

                // ----- Used stamina shrink effect ----------------------------
                usedStaminaShrinkTimer -= Time.deltaTime;

                if (usedStaminaShrinkTimer > 0)
                {
                    if (staminaImage.fillAmount < usedStaminaImage.fillAmount)
                    {
                        usedStaminaImage.fillAmount -= 1f * Time.deltaTime;
                    }
                }

                lerpSpeed = 3f * Time.deltaTime;

                yield return null;
            }
        }

        public void FadeIn()
        {
            animator.SetBool("FadeIn", true);
            coroutine = StartCoroutine(ChangeStaminaUI());
        }

        public void FadeOut()
        {
            StopCoroutine(coroutine);
            animator.SetBool("FadeIn", false);
        }

    }
}