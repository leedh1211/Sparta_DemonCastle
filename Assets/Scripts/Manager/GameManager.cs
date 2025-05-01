using UnityEngine;

public class GameManager : MonoBehaviour
{
    private UIManager uiManager;
    public static bool isFirstLoading = true;
    public static GameManager instance;

    public PlayerController player { get; private set; }
    private ResourceController _playerResourceController;
    
    [SerializeField] private int currentWaveIndex = 0;

    private EnemyManager enemyManager;

    private void Awake()
    {
        instance = this;
        player = FindObjectOfType<PlayerController>();
        player.Init(this);
        
        uiManager = FindObjectOfType<UIManager>();
        
        _playerResourceController = player.GetComponent<ResourceController>();
        enemyManager = GetComponentInChildren<EnemyManager>();
        enemyManager.Init(this);
    }
    
    private void Start()
    {
        if (!isFirstLoading)
        {
            StartGame();
        }
        else
        {
            isFirstLoading = false;
        }
    }

    public void StartGame()
    {
        uiManager.SetPlayGame();
        StartNextWave();
    }
    
    void StartNextWave()
    {
        currentWaveIndex += 1;
        uiManager.ChangeWave(currentWaveIndex);
        enemyManager.StartWave(1 + currentWaveIndex / 5);
    }

    public void EndOfWave()
    {
        StartNextWave();
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        enemyManager.StopWave();
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }
    }
}