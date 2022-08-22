using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager { get; private set; }

    public UnitStats _PlayerHealth = new UnitStats(100, 100);

    #region UI && Win
    [Header("Menus")]
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject WinMenu;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI endResult;
    [SerializeField] private GameObject healthVisual;
    [SerializeField] private GameObject Timer;

    [Header("Checkers")]
    [SerializeField] private checkWin winFlage;
    [SerializeField] private bool thereIsPause = true;

    [Header("SceneFader")]
    [SerializeField] private SceneFader sceneFader;

    bool gameIsPaused = false;
    #endregion

    private void Awake()
    {
        if (gameManager != null && gameManager != this)
        {
            Destroy(this);
        }
        else
        {
            gameManager = this;
        }

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && thereIsPause)
        {
            if (gameIsPaused)
            {
                pauseMenu.SetActive(false);
                healthVisual.SetActive(true);
                Timer.SetActive(true);
                Time.timeScale = 1f;
                gameIsPaused = false;
            }
            else
            {
                pauseMenu.SetActive(true);
                healthVisual.SetActive(false);
                Timer.SetActive(false);
                Time.timeScale = 0f;
                gameIsPaused = true;
            }
        }

        if (winFlage.PlayerIsHere == true)
        {
            Win();
        }

        if (_PlayerHealth.Health == 0)
        {
            GameOver();
        }
    }

    #region privateSystems
    private void Win()
    {
        Countdown countdown = Timer.GetComponent<Countdown>();
        countdown.timerActive = false;
        endResult.text = countdown.textBox.text;
        WinMenu.SetActive(true);
    }

    private void GameOver()
    {
        if (_PlayerHealth.Health <= 0)
        {
            healthVisual.SetActive(false);
            gameOverMenu.SetActive(true);
        }

    }
    #endregion

    #region Buttons Action

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

    #endregion

}
