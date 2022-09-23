using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class UIManager : MonoBehaviour
{
    [HideInInspector] public static bool gameIsPaused = false;

    [SerializeField] private GameObject panel;
    [SerializeField] private AnimationCurve curve;

    [Header("Menus")]
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject WinMenu;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI endResult;
    [SerializeField] private GameObject healthVisual;
    [SerializeField] private GameObject Timer;

    [Header("Checkers")]
    [SerializeField] private bool thereIsPause = true;

    private Image img;
    private bool doneLoading = false;
    
    //--------------------------------------------------------------------------------
    private void Start()
    {
        img = panel.GetComponent<Image>();
        StartCoroutine(FadeIn());

        SceneManager.sceneLoaded += FadeIn;
    }

    private void Update()
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
        Countdown countdown = Timer.GetComponent<Countdown>();
        countdown.timerActive = false;
        endResult.text = countdown.textBox.text;
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
    IEnumerator FadeOut(string scene)
    {
        panel.SetActive(true);
        Time.timeScale = 1f;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime;
            float a = curve.Evaluate(t);
            img.color = new Color(0, 0, 0, a);
            yield return 0;
        }

        SceneManager.LoadScene(scene);
    }
    IEnumerator FadeOut(int i)
    {
        panel.SetActive(true);
        Time.timeScale = 1f;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime;
            float a = curve.Evaluate(t);
            img.color = new Color(0, 0, 0, a);
            yield return 0;
        }

        SceneManager.LoadScene(i);
    }
    IEnumerator FadeIn()
    {
        pauseMenu.SetActive(false);
        healthVisual.SetActive(true);
        Timer.SetActive(true);
        Time.timeScale = 1f;
        gameIsPaused = false;

        panel.SetActive(true);
        float t = 1f;

        while (t > 0f)
        {
            t -= Time.deltaTime;
            float a = curve.Evaluate(t);
            img.color = new Color(0, 0, 0, a);
            yield return null;
        }

        panel.SetActive(false);
    }
    private void FadeIn(Scene scene, LoadSceneMode loadSceneMode)
    {
        StartCoroutine(FadeIn());

        GameManager.gameManager._PlayerHealth.Health = 100;
    }
    #endregion

    #region Buttons Action

    public void Restart()
    {
        StartCoroutine(FadeOut(SceneManager.GetActiveScene().name));

        if(doneLoading)
        { 
            StartCoroutine(FadeIn());
        }
    }

    public void NextLevel()
    {
        GameManager.gameManager.lastCheckPointPos = Vector2.zero;
        StartCoroutine(FadeOut(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void Select(int i)
    {
        GameManager.gameManager.lastCheckPointPos = Vector2.zero;
        StartCoroutine(FadeOut("Level " + i));
    }

    public void BackToMainMenu()
    {
        GameManager.gameManager.lastCheckPointPos = Vector2.zero;
        StartCoroutine(FadeOut("Menu"));
    }

    public void QuitGame()
    {
        GameManager.gameManager.lastCheckPointPos = Vector2.zero;
        Application.Quit();
    }

    #endregion
}