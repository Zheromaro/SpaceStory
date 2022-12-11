using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

namespace Core
{
    public class UIManager : MonoBehaviour
    {
        [HideInInspector] public static bool gameIsPaused = false;

        [Header("Fide")]
        [SerializeField] private Animator animator;
        [SerializeField] private int WaitFor;

        [Header("Menus")]
        [SerializeField] private GameObject gameOverMenu;
        [SerializeField] private GameObject pauseMenu;
        [SerializeField] private GameObject WinMenu;

        [Header("UI")]
        [SerializeField] private GameObject healthVisual;

        [Header("Checkers")]
        [SerializeField] private bool thereIsPause = true;

        //--------------------------------------------------------------------------------
        private void Start()
        {
            FadeIn();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && thereIsPause)
            {
                if (gameIsPaused)
                {
                    pauseMenu.SetActive(false);

                    healthVisual.SetActive(true);

                    Time.timeScale = 1f;
                    gameIsPaused = false;
                }
                else
                {
                    pauseMenu.SetActive(true);

                    healthVisual.SetActive(false);

                    Time.timeScale = 0f;
                    gameIsPaused = true;
                }
            }

            if (GameManager.gameManager._PlayerHealth.Health == 0)
            {
                GameOver();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                Win();
            }
        }

        //--------------------------------------------------------------------------------
        private void Win()
        {
            WinMenu.SetActive(true);
        }

        private void GameOver()
        {
            if (GameManager.gameManager._PlayerHealth.Health <= 0)
            {
                healthVisual.SetActive(false);
                gameOverMenu.SetActive(true);
            }

        }

        #region Fade

        private void FadeIn()
        {
            pauseMenu.SetActive(false);
            healthVisual.SetActive(true);
            Time.timeScale = 1f;
            gameIsPaused = false;
        }

        private IEnumerator FadeOut(string scene)
        {
            animator.SetTrigger("FadeOut");
            GameManager.gameManager._PlayerHealth.Health = 100;
            Time.timeScale = 1f;

            yield return new WaitForSeconds(WaitFor);

            SceneManager.LoadScene(scene);
        }

        private IEnumerator FadeOut(int i)
        {
            animator.SetTrigger("FadeOut");
            GameManager.gameManager._PlayerHealth.Health = 100;
            Time.timeScale = 1f;

            yield return new WaitForSeconds(WaitFor);

            SceneManager.LoadScene(i);
        }

        #endregion

        #region Buttons Action

        public void Restart()
        {
            StartCoroutine(FadeOut(SceneManager.GetActiveScene().name));
        }

        public void NextLevel()
        {
            StartCoroutine(FadeOut(SceneManager.GetActiveScene().buildIndex + 1));
        }

        public void Select(int i)
        {
            StartCoroutine(FadeOut("Level " + i));
        }

        public void BackToMainMenu()
        {
            StartCoroutine(FadeOut("Menu"));
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        #endregion

    }
}