using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float interval = 15f;
    public int countPerWave = 5;
    public Transform target;

    float t;

    void Update()
    {
        t += Time.deltaTime;
        if (t < interval) return;
        t = 0f;

        for (int i = 0; i < countPerWave; i++)
        {
            Vector3 pos = transform.position + Random.insideUnitSphere * 2f;
            pos.y = transform.position.y;

            var e = Instantiate(enemyPrefab, pos, Quaternion.identity);

            var unit = e.GetComponent<Unit>();
            if (unit != null) unit.isEnemy = true;

            var ai = e.GetComponent<EnemyAI>();
            if (ai != null && target != null) ai.target = target;
        }
    }
}
