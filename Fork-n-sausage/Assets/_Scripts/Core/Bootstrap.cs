using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootstrap : MonoBehaviour
{
    public static Bootstrap Instance { get; private set; }
    private readonly DIContainer _container = new();
    public DIContainer Container => _container;
    [SerializeField] private string nextScene = "Menu";

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene("LoadingScene");

        LoadingSceneController.SceneToLoad = nextScene;
    }
}
