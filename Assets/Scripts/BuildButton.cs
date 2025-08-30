using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Button))]
public class BuildButton : MonoBehaviour
{
    public BuildingData building;
    BuildingPlacementController _placer;
    void Start()
    {
        _placer = FindObjectOfType<BuildingPlacementController>();
        GetComponent<Button>().onClick.AddListener(() => _placer.SelectBuilding(building));
    }
}
