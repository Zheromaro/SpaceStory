using UnityEngine;
using UnityEngine.InputSystem;
using SpaceGame.Core;

namespace SpaceGame.UI
{
    public class UIManager : MonoBehaviour
    {
        private InputAction pause;
        private InputAction resume;

        [Header("Fide")]
        [SerializeField] private Animator animator;
        [SerializeField] private int WaitFor;

        [Header("Menus")]
        [SerializeField] private GameObject gameOverMenu;
        [SerializeField] private GameObject pauseMenu;
        [SerializeField] private GameObject WinMenu;

        [Header("UI")]
        [SerializeField] private GameObject healthVisual;

        //--------------------------------------------------------------------------------

        private void OnEnable()
        {
            pause = InputManager.inputActions.Player.UI_Pause;
            resume = InputManager.inputActions.UI.UI_Resume;

            pause.performed += Pause;
            resume.performed += Resume;
        }

        private void OnDisable()
        {
            pause.performed -= Pause;
            resume.performed -= Resume;
        }

        //--------------------------------------------------------------------------------

        private void Pause(InputAction.CallbackContext obj)
        {
            pauseMenu.SetActive(true);
            healthVisual.SetActive(false);

            InputManager.ToggeleActionMap(InputManager.inputActions.UI);
            Time.timeScale = 0f;
        }

        private void Resume(InputAction.CallbackContext obj)
        {
            pauseMenu.SetActive(false);
            healthVisual.SetActive(true);

            InputManager.ToggeleActionMap(InputManager.inputActions.Player);
            Time.timeScale = 1f;
        }

        public void GameOver()
        {
            healthVisual.SetActive(false);
            gameOverMenu.SetActive(true);
        }

        private void Win()
        {
            WinMenu.SetActive(true);
        }

    }
}