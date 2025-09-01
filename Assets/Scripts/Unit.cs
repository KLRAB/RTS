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
    float atkCD;
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
        health = GetComponent<Health>();
        if (!health) health = gameObject.AddComponent<Health>();
        health.maxHP = maxHP; 
    }
    void Update()
    {
        if (!target) return;
        agent.SetDestination(target.position);
        float dist = Vector3.Distance(transform.position, target.position);
        if (dist <= attackRange)
        {
            agent.ResetPath();
            atkCD -= Time.deltaTime;
            if (atkCD <= 0f)
            {
                atkCD = 1f / Mathf.Max(0.01f, attackRate);
                var h = target.GetComponent<Health>();
                if (h) h.Take(damage);
            }
        }
    }
    public void SetTarget(Transform t) { target = t; }
}
