using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneInstaller : MonoBehaviour
{
    public static SceneInstaller Instance{get; private set;}
    [SerializeField] private Rigidbody _rbPlayer;
    private InputHandlerBase _inputHandler;
    private PlayerControllerBase _playerController;
    private GameStateController _gameStateController;
    private DIContainer _container;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        _container = Bootstrap.Instance.Container;
        CreateInstances();
        RegisterDependencies();
        InjectDependencies();
        InitializeComponents();

        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void Update()
    {
        _container.TickAll();
    }

    private void CreateInstances()
    {
        _inputHandler = new InputHandlerTouch();
        _playerController = new PlayerControllerNormal();
        _gameStateController = new GameStateController();
    }

    private void RegisterDependencies()
    {
        _container.BindInstance(_inputHandler);
        _container.BindInstance(_playerController);
        _container.BindInstance(_gameStateController);
    }

    private void InjectDependencies()
    {
        _container.InjectInto(_playerController);
    }

    private void InitializeComponents()
    {
        _playerController.SetPlayer(_rbPlayer);
        _gameStateController.SetPlayer(_rbPlayer);
        _playerController.Init();
        _gameStateController.Init();
    }

    public void InjectInto(IInjectable target)
    {
        _container.InjectInto(target);
        target.Init();
    }
    
    private void OnSceneUnloaded(Scene scene)
    {
        if (Bootstrap.Instance != null)
        {
            Bootstrap.Instance.Container.DisposeAll();
        }
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }
}