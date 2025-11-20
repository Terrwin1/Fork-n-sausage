using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneInstaller : MonoBehaviour
{
    public static SceneInstaller Instance{get; private set;}
    [SerializeField] private Rigidbody _rbPlayer;
    [SerializeField] private CoinsViewGame _coinsViewGame;
    private InputHandlerBase _inputHandler;
    private PlayerControllerBase _playerController;
    private GameStateController _gameStateController;
    private CoinsController _coinsController;
    private SavingsLoader _savingsLoader;
    private Saver _saver;
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
        _coinsController = new CoinsController();
        _savingsLoader = new SavingsLoader();
        _saver = new Saver();
    }

    private void RegisterDependencies()
    {
        _container.BindInstance(_inputHandler);
        _container.BindInstance(_playerController);
        _container.BindInstance(_gameStateController);
        _container.BindInstance(_coinsController);
        _container.BindInstance(_savingsLoader);
        _container.BindInstance(_saver);
        _container.BindInstance(_coinsViewGame);
    }

    private void InjectDependencies()
    {
        _container.InjectInto(_playerController);
        _container.InjectInto(_saver);
        _container.InjectInto(_coinsViewGame);
        _container.InjectInto(_coinsController);
    }

    private void InitializeComponents()
    {
        _playerController.SetPlayer(_rbPlayer);
        _gameStateController.SetPlayer(_rbPlayer);
        _playerController.Init();
        _gameStateController.Init();
        _coinsController.Init();
        _saver.Init();
        _coinsViewGame.Init();
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