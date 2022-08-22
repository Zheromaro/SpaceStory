using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneFader : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private AnimationCurve curve;

    private Image img;

    private void Start()
    {
        img = panel.GetComponent<Image>();
        StartCoroutine(FadeIn());
    }

    public void FadeTo(string scene)
    {
        StartCoroutine(FadeOut(scene));
    }
    public void FadeTo(int i)
    {
        StartCoroutine(FadeOut(i));
    }
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
        panel.SetActive(true);
        float t = 1f;

        while (t > 0f)
        {
            t -= Time.deltaTime;
            float a = curve.Evaluate(t);
            img.color = new Color(0, 0, 0, a);
            yield return 0;
        }

        panel.SetActive(false);
    }
}
