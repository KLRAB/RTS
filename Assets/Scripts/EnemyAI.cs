using UnityEngine;
public class EnemyAI : MonoBehaviour
{
    public Transform target;
    Unit u;
    void Awake() { u = GetComponent<Unit>(); }
    void Start()
    {
        if (target == null)
        {
            var all = GameObject.FindObjectsOfType<Health>();
            foreach (var h in all) { if (h.isTownCenter) { target = h.transform; break; } }
            if (target == null)
            {
                var tc = GameObject.Find("TownCenter");
                if (tc) target = tc.transform;
            }
        }
        if (u != null && target != null) u.SetTarget(target);
    }
}
