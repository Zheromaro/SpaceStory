using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using SpaceGame.Core;

namespace SpaceGame.UI
{
    public class UIManager : MonoBehaviour
    {
        private InputAction pause;
        private InputAction resume;
        bool gameIsPaused = false;

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

            pause.performed += onInput;
            resume.performed += onInput;
            SceneManager.sceneLoaded += onLoaded;
        }

        private void OnDisable()
        {
            pause.performed -= onInput;
            resume.performed -= onInput;
            SceneManager.sceneLoaded -= onLoaded;
        }

        private void onLoaded(Scene scene, LoadSceneMode arg1)
        {
            if (scene.name == "Menu") { return; }
            Resume();
        }

        private void onInput(InputAction.CallbackContext obj)
        {
            if (gameIsPaused == false)
            {
                Pause();
            }
            else
            {
                Resume();
            }

        }

        //--------------------------------------------------------------------------------

        private void Pause()
        {
            pauseMenu.SetActive(true);
            healthVisual.SetActive(false);

            InputManager.ToggeleActionMap(InputManager.inputActions.UI);
            Time.timeScale = 0f;
            gameIsPaused = true;
        }

        private void Resume()
        {
            gameOverMenu.SetActive(false);
            pauseMenu.SetActive(false);
            WinMenu.SetActive(false);
            healthVisual.SetActive(true);

            InputManager.ToggeleActionMap(InputManager.inputActions.Player);
            Time.timeScale = 1f;
            gameIsPaused = false;
        }

        public void GameOver()
        {
            healthVisual.SetActive(false);
            gameOverMenu.SetActive(true);
        }

        public void Win()
        {
            healthVisual.SetActive(false);
            WinMenu.SetActive(true);
        }
    }
}