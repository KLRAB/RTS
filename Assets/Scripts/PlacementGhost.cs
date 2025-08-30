using UnityEngine;
public class PlacementGhost : MonoBehaviour
{
    [SerializeField] Renderer[] rends;
    public void SetValid(bool valid)
    {
        var col = valid ? new Color(0, 1, 0, 0.5f) : new Color(1, 0, 0, 0.5f);
        if (rends == null || rends.Length == 0)
        {
            var r = GetComponentInChildren<Renderer>();
            if (r != null) rends = new Renderer[] { r };
        }
        foreach (var r in rends)
        {
            if (r == null) continue;
            foreach (var m in r.materials)
            {
                if (m.HasProperty("_BaseColor")) m.SetColor("_BaseColor", col); 
                else if (m.HasProperty("_Color")) m.color = col;                
            }
        }
    }
}
