using System;
using System.Collections.Generic;
using UnityEngine;
public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance;
    [Serializable]
    public class ResourceStart
    {
        public ResourceType type;
        public int amount;
    }
    [Header("Start resources")]
    public List<ResourceStart> startResources = new List<ResourceStart>
    {
        new ResourceStart{ type = ResourceType.Wood, amount = 200 },
        new ResourceStart{ type = ResourceType.Stone, amount = 100 },
        new ResourceStart{ type = ResourceType.Food, amount = 100 },
        new ResourceStart{ type = ResourceType.Gold, amount = 100 },
    };
    private Dictionary<ResourceType, int> stock = new Dictionary<ResourceType, int>();
    public static event Action OnChanged;
    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        stock.Clear();
        foreach (var s in startResources)
            stock[s.type] = s.amount;
    }
    public int Get(ResourceType t) => stock.TryGetValue(t, out var v) ? v : 0;
    public void Add(ResourceType t, int amount)
    {
        stock[t] = Get(t) + amount;
        OnChanged?.Invoke();
    }
    public bool CanAfford(List<ResourceAmount> cost)
    {
        if (cost == null) return true;
        foreach (var c in cost)
            if (Get(c.type) < c.amount) return false;
        return true;
    }
    public bool Spend(List<ResourceAmount> cost)
    {
        if (!CanAfford(cost)) return false;
        foreach (var c in cost)
            stock[c.type] = Get(c.type) - c.amount;
        OnChanged?.Invoke();
        return true;
    }
}
