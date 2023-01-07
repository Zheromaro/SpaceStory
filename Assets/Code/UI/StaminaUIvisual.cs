using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using SpaceGame.Core;

namespace SpaceGame.UI
{
    public class StaminaUIvisual : MonoBehaviour
    {
        private InputAction Input_Sprint;

        [SerializeField] private float DisapearTime = 0.5f;
        private const float USED_STAMINA_SHRINK_TIMER_MAX = 1f;

        private Image staminaImage;
        private Image usedStaminaImage;
        private float usedStaminaShrinkTimer;

        public float stamina;
        private float lerpSpeed;

        private Animator animator;

        private void Awake()
        {
            staminaImage = transform.Find("Stamina").GetComponent<Image>();
            usedStaminaImage = transform.Find("UsedStamina").GetComponent<Image>();
        }

        private void Start()
        {
            usedStaminaImage.fillAmount = staminaImage.fillAmount;
            stamina = GameManager.gameManager._PlayerStamina.Stamina;
            animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            Input_Sprint = InputManager.inputActions.Player.Move_Sprint;

            Input_Sprint.performed += onSprintPerformed; ;
            Input_Sprint.canceled += onSprintCanceled; ;
        }

        private void OnDisable()
        {
            Input_Sprint.performed -= onSprintPerformed;
            Input_Sprint.canceled -= onSprintCanceled;
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

            UseAndGetStamina();
        }

        private void onSprintPerformed(InputAction.CallbackContext obj)
        {
            animator.SetBool("FadeIn", true);
        }

        private void onSprintCanceled(InputAction.CallbackContext obj)
        {
            animator.SetBool("FadeIn", false);
        }

        private void UseAndGetStamina()
        {
            if (stamina > GameManager.gameManager._PlayerStamina.Stamina)
            {
                UseStamina();
                stamina = GameManager.gameManager._PlayerStamina.Stamina;
            }
            else if (stamina < GameManager.gameManager._PlayerStamina.Stamina)
            {
                GetStamina();
                stamina = GameManager.gameManager._PlayerStamina.Stamina;
            }
        }
        
        #region GetUseStamina

        public void UseStamina()
        {
            staminaImage.fillAmount = Mathf.Lerp(staminaImage.fillAmount, GetStaminaNormalized(), lerpSpeed); ;
            usedStaminaShrinkTimer = USED_STAMINA_SHRINK_TIMER_MAX;

        }

        public void GetStamina()
        {
            staminaImage.fillAmount = Mathf.Lerp(staminaImage.fillAmount, GetStaminaNormalized(), lerpSpeed); ;
            usedStaminaImage.fillAmount = staminaImage.fillAmount;
        }

        private float GetStaminaNormalized()
        {
            return (float)GameManager.gameManager._PlayerStamina.Stamina / GameManager.gameManager._PlayerStamina.MaxStamina;
        }
        #endregion

        //-------------------------------------------------------------------------------------------

        public void FadeInOut()
        {
            StartCoroutine(FadeInOutCoroutine());
        }

        private IEnumerator FadeInOutCoroutine()
        {
            animator.SetBool("FadeIn", true);
            yield return new WaitForSeconds(DisapearTime);
            animator.SetBool("FadeIn", false);
        }

    }
}