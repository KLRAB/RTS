using UnityEngine;
public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform target;
    [Header("Waves")]
    public float interval = 15f;
    public int countPerWave = 5;
    public int totalWaves = 5;
    float t;
    int waveIndex;
    bool finished;
    void Start()
    {
        if (GameManager.Instance) GameManager.Instance.RegisterSpawner();
    }
    void Update()
    {
        if (finished) return;
        t += Time.deltaTime;
        if (t < interval) return;
        t = 0f;
        waveIndex++;
        if (GameManager.Instance) GameManager.Instance.NotifyWaveSpawned();
        for (int i = 0; i < countPerWave; i++)
        {
            Vector3 pos = transform.position + Random.insideUnitSphere * 2f;
            pos.y = transform.position.y;
            var e = Instantiate(enemyPrefab, pos, Quaternion.identity);
            var unit = e.GetComponent<Unit>();
            if (unit != null) unit.isEnemy = true;
            var ai = e.GetComponent<EnemyAI>();
            if (ai != null && target != null) ai.target = target;
            var h = e.GetComponent<Health>();
            if (GameManager.Instance != null && h != null) GameManager.Instance.RegisterEnemy(h);
        }
        if (waveIndex >= totalWaves)
        {
            finished = true;
            if (GameManager.Instance) GameManager.Instance.NotifySpawnerFinished();
        }
    }
}
