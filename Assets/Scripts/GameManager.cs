using UnityEngine;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [Header("Win condition")]
    public int wavesToWin = 5;
    public EndgameUI endgameUI;
    int wavesSpawned;
    int spawnersTotal;
    int spawnersFinished;
    int enemiesAlive;
    bool over;
    public int CurrentWave => wavesSpawned;
    public int EnemiesAlive => enemiesAlive;
    public bool IsOver => over;
    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }
    public void RegisterSpawner() { spawnersTotal++; }
    public void NotifyWaveSpawned()
    {
        wavesSpawned++;
    }
    public void NotifySpawnerFinished()
    {
        spawnersFinished++;
        CheckWin();
    }
    public void RegisterEnemy(Health h)
    {
        if (h == null) return;
        enemiesAlive++;
        h.Died += OnEnemyDied;
    }
    void OnEnemyDied(Health h)
    {
        enemiesAlive = Mathf.Max(0, enemiesAlive - 1);
        h.Died -= OnEnemyDied;
        CheckWin();
    }
    void CheckWin()
    {
        if (over) return;
        if (spawnersFinished >= spawnersTotal && enemiesAlive <= 0 && wavesSpawned >= wavesToWin)
        {
            Win();
        }
    }
    public void Lose()
    {
        if (over) return;
        over = true;
        Time.timeScale = 0f;
        if (endgameUI) endgameUI.ShowLose(); else Debug.Log("YOU LOSE");
    }
    void Win()
    {
        over = true;
        Time.timeScale = 0f;
        if (endgameUI) endgameUI.ShowWin(); else Debug.Log("YOU WIN");
    }
}

