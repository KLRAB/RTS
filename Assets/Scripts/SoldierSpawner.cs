using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class SoldierSpawner : MonoBehaviour
{
    [Header("What to spawn")]
    public GameObject soldierPrefab;
    public Transform spawnPoint;
    [Header("Cost per spawn (optional)")]
    public List<ResourceAmount> spawnCost;
    [Header("Timing")]
    public float spawnCooldown = 2f;
    float cd;
    [Header("Placement")]
    public float forwardOffset = 2.0f;
    public float sampleRadius = 3.0f; 
    public bool alignToSpawnerForward = true;
    void Update() { if (cd > 0f) cd -= Time.deltaTime; }
    public bool TrySpawn()
    {
        if (!soldierPrefab) { Debug.LogWarning("SoldierSpawner: soldierPrefab not set"); return false; }
        if (cd > 0f) return false;
        if (ResourceManager.Instance != null && spawnCost != null && spawnCost.Count > 0)
        {
            if (!ResourceManager.Instance.Spend(spawnCost))
            {
                Debug.Log("Not enough resources to spawn soldier!");
                return false;
            }
        }
        Vector3 basePos = spawnPoint
            ? spawnPoint.position
            : transform.TransformPoint(Vector3.forward * forwardOffset);
        if (NavMesh.SamplePosition(basePos, out var hit, sampleRadius, NavMesh.AllAreas))
            basePos = hit.position;
        else
            basePos = new Vector3(basePos.x, transform.position.y, basePos.z);
        Quaternion rot = alignToSpawnerForward ? Quaternion.LookRotation(transform.forward, Vector3.up) : Quaternion.identity;
        var go = Instantiate(soldierPrefab, basePos, rot);
        cd = spawnCooldown;
        return true;
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 0.7f, 1f, 0.35f);
        Vector3 p = spawnPoint ? spawnPoint.position : transform.TransformPoint(Vector3.forward * forwardOffset);
        Gizmos.DrawSphere(p, 0.2f);
    }
}
