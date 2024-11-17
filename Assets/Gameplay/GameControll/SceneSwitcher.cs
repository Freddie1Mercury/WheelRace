
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField] private Slider _loadingScreenSlider;

    [SerializeField] private GameObject _loadingScreen;

   public void SwitchScene(string sceneName)
    {
        StartCoroutine(LoadAsyncScene(sceneName));
    }

    private IEnumerator LoadAsyncScene(string sceneName)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        _loadingScreen.SetActive(true);

        while (!asyncOperation.isDone)
        {
            _loadingScreenSlider.value = asyncOperation.progress;
            yield return null;
        }
    }
}
