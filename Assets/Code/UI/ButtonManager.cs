using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using SpaceGame.Core;
using SpaceGame.Core.SaveSystem;

namespace SpaceGame.UI
{
    public class ButtonManager : MonoBehaviour
    {
        private SceneFader sceneFader;

        [Header("UI")]
        [SerializeField] private GameObject pauseMenu;
        [SerializeField] private GameObject healthVisual;

        [Header("Buttons")]
        [SerializeField] private Button continueButton;
        [SerializeField] private Button[] ButtonsMenu;

        private void Start()
        {
            if (SceneManager.GetActiveScene().name == "Menu" && !DataPersistatenceManager.dataPersistatence.HasGameData())
            {
                continueButton.interactable = false;
            }

            sceneFader = GetComponent<SceneFader>();
            sceneFader.FadeIn();
        }

        public void Restart()
        {
            InputManager.ToggeleActionMap(InputManager.inputActions.Player);

            sceneFader.FadeOut(SceneManager.GetActiveScene().name);
        }

        public void NextLevel()
        {
            InputManager.ToggeleActionMap(InputManager.inputActions.Player);

            DisableButtons();
            sceneFader.FadeOut(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void Select(int i)
        {
            InputManager.ToggeleActionMap(InputManager.inputActions.Player);

            DisableButtons();
            OutMenu();
            sceneFader.FadeOut("Level " + i);
        }

        public void BackToMainMenu()
        {
            InputManager.ToggeleActionMap(InputManager.inputActions.UI);

            DisableButtons();
            sceneFader.FadeOut("Menu");
        }

        public void NewGame()
        {
            InputManager.ToggeleActionMap(InputManager.inputActions.Player);

            DisableButtons();

            // create a new game - which will initialize our game data
            DataPersistatenceManager.dataPersistatence.NewGame();

            // Load the gameplay scene - which will in turn save the game because of
            // OnSceneUnloaded() in the DataPersistatenceManager
            SceneManager.LoadSceneAsync("Level 1");
        }

        public void QuitGame()
        {
            DisableButtons();
            Application.Quit();
        }

        private void DisableButtons()
        {
            foreach (var Button in ButtonsMenu)
            {
                Button.interactable = false;
            }
        }
        private void OutMenu()
        {
            pauseMenu.SetActive(false);
            healthVisual.SetActive(true);
        }
    }
}
