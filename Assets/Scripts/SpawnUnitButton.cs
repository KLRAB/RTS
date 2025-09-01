using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Button))]
public class SpawnUnitButton : MonoBehaviour
{
    public SoldierSpawner spawner;
    void Start()
    {
        if (spawner == null) spawner = FindObjectOfType<SoldierSpawner>();
        GetComponent<Button>().onClick.AddListener(() =>
        {
            if (spawner) spawner.TrySpawn();
            else Debug.LogWarning("SpawnUnitButton: no spawner assigned/found.");
        });
    }
}
