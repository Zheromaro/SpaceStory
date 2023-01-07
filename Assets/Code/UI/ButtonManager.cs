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
        }

        public void NextLevel()
        {
            Time.timeScale = 1f;

            DisableButtons();
            sceneFader.FadeOut(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void Restart()
        {
            Time.timeScale = 1f;

            sceneFader.FadeOut(SceneManager.GetActiveScene().name);
        }

        public void BackToMainMenu()
        {
            Time.timeScale = 1f;

            DisableButtons();
            sceneFader.FadeOut("Menu");
        }

        public void NewGame()
        {
            DisableButtons();
            OutMenu();

            // create a new game - which will initialize our game data
            DataPersistatenceManager.dataPersistatence.NewGame();

            // Load the gameplay scene - which will in turn save the game because of
            // OnSceneUnloaded() in the DataPersistatenceManager
            sceneFader.FadeOut("Level 1");
        }

        public void Select(int i)
        {
            DisableButtons();
            OutMenu();
            sceneFader.FadeOut("Level " + i);
        }

        public void QuitGame()
        {
            DisableButtons();
            Application.Quit();
        }

        #region just for some help

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
        #endregion
    }
}
