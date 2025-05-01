using UnityEngine;

public class GameManager : MonoBehaviour
{
    private UIManager uiManager;
    public static bool isFirstLoading = true;
    public static GameManager instance;

    public PlayerController player { get; private set; }
    
    [SerializeField] private int currentWaveIndex = 0;

    private EnemyManager enemyManager;

    private void Awake()
    {
        instance = this;
        player = FindObjectOfType<PlayerController>();
        player.Init(this);
        
        uiManager = FindObjectOfType<UIManager>();
        
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
        currentWaveIndex = 1;
        uiManager.ChangeWave(currentWaveIndex);
        uiManager.SetPlayGame();
        StartNextWave();
    }
    
    void StartNextWave()
    {
        currentWaveIndex += 1;
        uiManager.ChangeWave(currentWaveIndex);
        enemyManager.StartWave(1 + currentWaveIndex / 2);
    }

    public void EndOfWave()
    {
        StartNextWave();
    }

    public void GameOver()
    {
        if (PlayerPrefs.GetInt("MaxWave") < currentWaveIndex)
        {
            PlayerPrefs.SetInt("MaxWave", currentWaveIndex);
        }
        enemyManager.StopWave();
        uiManager.SetGameOver();
    }
}