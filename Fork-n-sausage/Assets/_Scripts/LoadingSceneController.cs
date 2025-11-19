using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneController : MonoBehaviour
{
    public static string SceneToLoad;
    [SerializeField] private Slider _progressBar;

    private void Start()
    {
        StartCoroutine(LoadSceneAsync(SceneToLoad));
    }

    private IEnumerator LoadSceneAsync(string SceneName)
    {
        if (string.IsNullOrEmpty(SceneName))
        {
            Debug.LogError("No scene set to load!");
            yield break;
        }

        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneName);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            _progressBar.value = progress;

            if (progress >= 1f)
            {
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
        
    }
}
