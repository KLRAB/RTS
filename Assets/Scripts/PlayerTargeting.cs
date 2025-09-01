using UnityEngine;
public class PlayerTargeting : MonoBehaviour
{
    public float radius = 12f;
    public float refreshInterval = 0.33f;
    Unit unit;
    float t;
    void Awake()
    {
        unit = GetComponent<Unit>();
        if (!unit) unit = gameObject.AddComponent<Unit>();
        unit.isEnemy = false;
    }
    void Update()
    {
        t += Time.deltaTime;
        if (t < refreshInterval) return;
        t = 0f;
        Collider[] cols = Physics.OverlapSphere(transform.position, radius);
        Transform nearest = null;
        float best = float.MaxValue;
        foreach (var c in cols)
        {
            var u = c.GetComponentInParent<Unit>();
            if (u == null || !u.isEnemy) continue;
            float d = (c.transform.position - transform.position).sqrMagnitude;
            if (d < best) { best = d; nearest = u.transform; }
        }
        if (nearest != null)
            unit.SetTarget(nearest);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
