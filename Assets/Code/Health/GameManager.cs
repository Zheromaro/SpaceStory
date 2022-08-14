using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject healthVisual;
    [SerializeField] private bool thereIsPause = true;

    public SceneFader sceneFader;

    bool gameHasEnded = false;
    bool gameIsPaused = false;

    void Update()
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

    }

    public void EndGame()
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            healthVisual.SetActive(false);
            gameOver.SetActive(true);
        }

    }

    public void Restart()
    {
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void NextLevel()
    {
        sceneFader.FadeTo(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Select(int i)
    {
        sceneFader.FadeTo("Level " + i);
    }

    public void BackToMainMenu()
    {
        sceneFader.FadeTo("Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
