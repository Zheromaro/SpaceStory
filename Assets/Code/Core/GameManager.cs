using System.Collections;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager { get; private set; }

    public UnitHealth _PlayerHealth = new UnitHealth(100, 100);
    public UnitStamina _PlayerStamina = new UnitStamina(99f, 99f);

    [HideInInspector] public bool waiting;
    [HideInInspector] public CinemachineVirtualCamera virtualCamera;


    [Header("CheckPoints Management")]
    public Vector2 lastCheckPointPos;

    [Header("SlowDown")]
    [SerializeField] private float slowdownFactor = 0.05f;
    [SerializeField] private float slowdownLength = 2f;


    //--------------------------------------------------------------------

    private void Awake()
    {
        if (gameManager != null && gameManager != this)
        {
            Destroy(gameObject);
        }
        else
        {
            gameManager = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        SceneManager.sceneLoaded += restart;
    }

    private void Update()
    {
        if (!waiting && !UIManager.gameIsPaused)
        {
            Time.timeScale += (1f / slowdownLength) * Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
        }
    }

    public void DoSlowMotion()
    {
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.35f;
    }

    public void DoStopMotion(float duration)
    {
        if (waiting)
            return;
        Time.timeScale = 0.0f;
        StartCoroutine(Wait(duration));
    }

    public void runCoroutine(IEnumerator cor)
    {
        StartCoroutine(cor);
    }

    private IEnumerator Wait(float duration)
    {
        waiting = true;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1.0f;  
        waiting = false;
    }

    private void restart(Scene scene, LoadSceneMode loadSceneMode)
    {
        _PlayerHealth.Health = 100;
    }

}
