using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cinemachine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager { get; private set; }

    #region System & valus
    [Header("SceneFader")]
    [SerializeField] private SceneFader sceneFader;


    [Header("Cameras")]
    [HideInInspector] public CinemachineVirtualCamera virtualCamera;

    [Header("SlowDown")]
    [SerializeField] private float slowdownFactor = 0.05f;
    [SerializeField] private float slowdownLength = 2f;

    public bool waiting;
    #endregion

    #region Player
    public UnitHealth _PlayerHealth = new UnitHealth(100, 100);
    public UnitStamina _PlayerStamina = new UnitStamina(99f, 99f);
    #endregion

    #region UI
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

        virtualCamera = transform.Find("Cameras").transform.Find("CM vcam").GetComponent<CinemachineVirtualCamera>();
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

        if (!waiting && !gameIsPaused)
        {
            Time.timeScale += (1f / slowdownLength) * Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
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

    #region publicSystems
    public void DoSlowMotion()
    {
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.3f;
    }

    public void DoStopMotion(float duration)
    {
        if (waiting)
            return;
        Time.timeScale = 0.0f;
        StartCoroutine(Wait(duration));
    }

    #endregion

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

    IEnumerator Wait(float duration)
    {
        waiting = true;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1.0f;  
        waiting = false;
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
