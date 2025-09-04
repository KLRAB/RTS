using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public class Unit : MonoBehaviour
{
    [Header("Flags")]
    public bool isEnemy = false;
    [Header("Stats")]
    public int maxHP = 60;
    public int damage = 6;
    public float attackRate = 1f;
    public float attackRange = 1.6f;
    public float moveSpeed = 3.2f;
    [HideInInspector] public Health health;
    NavMeshAgent agent;
    Transform target;
    Health targetHealth;
    float atkCD;
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
        agent.stoppingDistance = Mathf.Clamp(attackRange * 0.8f, 0.1f, Mathf.Max(0.1f, attackRange - 0.05f));
        health = GetComponent<Health>();
        if (!health) health = gameObject.AddComponent<Health>();
        health.maxHP = maxHP;
    }
    public void SetTarget(Transform t)
    {
        target = t;
        targetHealth = GetHealthFrom(t);
    }
    void Update()
    {
        if (!target) return;
        agent.SetDestination(target.position);
        Vector3 myPos = transform.position;
        Vector3 hitPos = GetClosestPointOnTarget(myPos);
        float dist = Vector3.Distance(new Vector3(myPos.x, 0, myPos.z), new Vector3(hitPos.x, 0, hitPos.z));
        if (dist <= attackRange)
        {
            agent.ResetPath();
            atkCD -= Time.deltaTime;
            if (atkCD <= 0f)
            {
                atkCD = 1f / Mathf.Max(0.01f, attackRate);
                var h = targetHealth ?? GetHealthFrom(target);
                if (h == null)
                {
                    Collider[] cols = Physics.OverlapSphere(transform.position, attackRange + 0.5f);
                    foreach (var c in cols)
                    {
                        h = c.GetComponent<Health>() ?? c.GetComponentInParent<Health>() ?? c.GetComponentInChildren<Health>();
                        if (h != null) { targetHealth = h; break; }
                    }
                }
                if (h != null)
                {
                    h.Take(damage);
                    if (h.isTownCenter)
                        Debug.Log($"{name} hits TOWN CENTER for {damage}");
                }
            }
        }
    }
    Health GetHealthFrom(Transform t)
    {
        if (!t) return null;
        return t.GetComponent<Health>() ?? t.GetComponentInParent<Health>() ?? t.GetComponentInChildren<Health>();
    }
    Vector3 GetClosestPointOnTarget(Vector3 fromPos)
    {
        if (targetHealth != null)
        {
            var col = targetHealth.GetComponent<Collider>() ?? targetHealth.GetComponentInChildren<Collider>();
            if (col != null) return col.ClosestPoint(fromPos);
        }
        var tc = target.GetComponent<Collider>() ?? target.GetComponentInChildren<Collider>() ?? target.GetComponentInParent<Collider>();
        if (tc != null) return tc.ClosestPoint(fromPos);
        return target.position;
    }
}
