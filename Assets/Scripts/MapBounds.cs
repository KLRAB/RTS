using UnityEngine;
public class MapBounds : MonoBehaviour
{
    public static MapBounds Instance;
    public Renderer fromRenderer;
    public float padding = 0.5f;
    public Vector2 centerXZ = Vector2.zero;
    public Vector2 sizeXZ = new Vector2(100, 100);
    float minX, maxX, minZ, maxZ;
    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        Recalculate();
    }
    void OnValidate()
    {
        if (Application.isPlaying) return;
        Recalculate();
    }
    public void Recalculate()
    {
        if (fromRenderer != null)
        {
            var b = fromRenderer.bounds;
            minX = b.min.x + padding;
            maxX = b.max.x - padding;
            minZ = b.min.z + padding;
            maxZ = b.max.z - padding;
        }
        else
        {
            minX = centerXZ.x - sizeXZ.x * 0.5f + padding;
            maxX = centerXZ.x + sizeXZ.x * 0.5f - padding;
            minZ = centerXZ.y - sizeXZ.y * 0.5f + padding;
            maxZ = centerXZ.y + sizeXZ.y * 0.5f - padding;
        }
    }
    public bool ContainsXZ(Vector3 pos)
    {
        return pos.x >= minX && pos.x <= maxX && pos.z >= minZ && pos.z <= maxZ;
    }
    public Vector3 ClampXZ(Vector3 pos)
    {
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.z = Mathf.Clamp(pos.z, minZ, maxZ);
        return pos;
    }
    public bool ContainsRectXZ(Vector3 center, Vector2 size)
    {
        float hx = size.x * 0.5f;
        float hz = size.y * 0.5f;
        return (center.x - hx) >= minX && (center.x + hx) <= maxX
            && (center.z - hz) >= minZ && (center.z + hz) <= maxZ;
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Vector3 c = new Vector3((minX + maxX) * 0.5f, 0.05f, (minZ + maxZ) * 0.5f);
        Vector3 s = new Vector3(Mathf.Abs(maxX - minX), 0.05f, Mathf.Abs(maxZ - minZ));
        Gizmos.DrawWireCube(c, s);
    }
}
