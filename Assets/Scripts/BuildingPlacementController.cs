using UnityEngine;
public class BuildingPlacementController : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] GameObject ghostPrefab;
    BuildingData _selected;
    GameObject _ghost;
    void Start()
    {
        if (!cam) cam = Camera.main;
    }
    public void SelectBuilding(BuildingData bd)
    {
        _selected = bd;
        SpawnGhost();
    }
    void SpawnGhost()
    {
        if (_ghost) Destroy(_ghost);
        if (_selected == null || ghostPrefab == null) return;
        _ghost = Instantiate(ghostPrefab);
    }
    void Update()
    {
        if (_selected == null || _ghost == null) return;
        if (!RayToGround(out var pos)) return;
        var snap = GridSystem.Snap(pos);
        _ghost.transform.position = snap;
        bool can = CanPlace(snap, _selected);
        var pg = _ghost.GetComponent<PlacementGhost>();
        if (pg) pg.SetValid(can);
        if (can && Input.GetMouseButtonDown(0))
        {
            if (ResourceManager.Instance == null ||
                ResourceManager.Instance.Spend(_selected.cost))
            {
                var go = Instantiate(_selected.prefab, snap, Quaternion.identity);
                if (!go.GetComponent<BuildingMarker>()) go.AddComponent<BuildingMarker>();
            }
            else
            {
                Debug.Log("Not enough resources!");
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            _selected = null;
            if (_ghost) Destroy(_ghost);
        }
    }
    bool RayToGround(out Vector3 hitPos)
    {
        hitPos = Vector3.zero;
        var ray = cam.ScreenPointToRay(Input.mousePosition);
        var hits = Physics.RaycastAll(ray, 500f);
        if (hits == null || hits.Length == 0) return false;
        System.Array.Sort(hits, (a, b) => a.distance.CompareTo(b.distance));
        foreach (var h in hits)
        {
            if (h.collider.GetComponent<BuildingMarker>() != null) continue;
            if (h.collider.GetComponentInParent<PlacementGhost>() != null) continue;
            hitPos = h.point;
            return true;
        }
        return false;
    }
    bool CanPlace(Vector3 center, BuildingData bd)
    {
        Vector3 half = new Vector3(bd.footprint.x, 2f, bd.footprint.y) * 0.5f;
        var cols = Physics.OverlapBox(center + Vector3.up, half, Quaternion.identity);
        foreach (var c in cols)
        {
            if (c.GetComponent<BuildingMarker>() != null)
                return false;
        }
        return true;
    }
    void OnDrawGizmosSelected()
    {
        if (_selected == null || _ghost == null) return;
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(
            _ghost.transform.position + Vector3.up,
            new Vector3(_selected.footprint.x, 2f, _selected.footprint.y)
        );
    }
}
