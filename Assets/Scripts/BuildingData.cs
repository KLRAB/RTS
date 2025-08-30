using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "u")]
public class BuildingData : ScriptableObject
{
    public string id;
    public GameObject prefab;
    public Vector2Int footprint = new Vector2Int(2, 2);
    [Header("Cost to build")]
    public List<ResourceAmount> cost;
    [Header("Income per minute (distributed every second)")]
    public List<ResourceAmount> incomePerMinute;
}
