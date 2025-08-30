using System.Collections.Generic;
using UnityEngine;
public class Building : MonoBehaviour
{
    public BuildingData data;
    private float secondTimer;
    private Dictionary<ResourceType, float> carry = new Dictionary<ResourceType, float>();
    void Start()
    {
        if (data != null && data.incomePerMinute != null)
        {
            carry.Clear();
            foreach (var inc in data.incomePerMinute)
                if (!carry.ContainsKey(inc.type)) carry[inc.type] = 0f;
        }
    }
    void Update()
    {
        if (data == null || data.incomePerMinute == null || data.incomePerMinute.Count == 0) return;
        if (ResourceManager.Instance == null) return;
        secondTimer += Time.deltaTime;
        if (secondTimer < 1f) return;
        secondTimer = 0f;
        foreach (var inc in data.incomePerMinute)
        {
            float perSecond = inc.amount / 60f;
            carry[inc.type] += perSecond;
            int grant = Mathf.FloorToInt(carry[inc.type]);
            if (grant > 0)
            {
                ResourceManager.Instance.Add(inc.type, grant);
                carry[inc.type] -= grant;
            }
        }
    }
}
