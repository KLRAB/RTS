using UnityEngine;
public class Tower : MonoBehaviour
{
    public float range = 10f;
    public int damage = 6;
    public float rate = 1.2f;
    float cd;
    void Update()
    {
        cd -= Time.deltaTime;
        if (cd > 0f) return;
        cd = 1f / Mathf.Max(0.01f, rate);
        Collider[] cols = Physics.OverlapSphere(transform.position, range);
        Transform nearest = null;
        float best = float.MaxValue;
        foreach (var c in cols)
        {
            var u = c.GetComponentInParent<Unit>();
            if (u == null || !u.isEnemy) continue;
            float d = (c.transform.position - transform.position).sqrMagnitude;
            if (d < best) { best = d; nearest = c.transform; }
        }
        if (nearest != null)
        {
            var h = nearest.GetComponentInParent<Health>();
            if (h) h.Take(damage);
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
