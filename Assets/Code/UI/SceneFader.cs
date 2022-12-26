using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using SpaceGame.Core;
using SpaceGame.Core.SaveSystem;


namespace SpaceGame.UI
{
    public class SceneFader : MonoBehaviour
    {
        [Header("Fide")]
        [SerializeField] private Animator animator;
        [SerializeField] private int WaitFor;

        public void FadeIn()
        {
            Time.timeScale = 1f;
        }

        public void FadeOut(string scene)
        {
            StartCoroutine(FadeOutMangement(scene));
        }

        public void FadeOut(int i)
        {
            StartCoroutine(FadeOutMangement(i));
        }

        private IEnumerator FadeOutMangement(string scene)
        {
            animator.SetTrigger("FadeOut");
            DataPersistatenceManager.dataPersistatence.SaveGame();

            yield return new WaitForSeconds(1);

            GameManager.gameManager._PlayerHealth.Health = 100;
            Time.timeScale = 1f;

            yield return new WaitForSeconds(WaitFor);

            SceneManager.LoadSceneAsync(scene);
        }

        private IEnumerator FadeOutMangement(int i)
        {
            animator.SetTrigger("FadeOut");
            DataPersistatenceManager.dataPersistatence.SaveGame();

            yield return new WaitForSeconds(1);

            GameManager.gameManager._PlayerHealth.Health = 100;
            Time.timeScale = 1f;

            yield return new WaitForSeconds(WaitFor);

            SceneManager.LoadSceneAsync(i);
        }
    }
}
