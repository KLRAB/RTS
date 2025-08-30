using UnityEngine;
using UnityEngine.UI;
public class UIResources : MonoBehaviour
{
    public Text wood;
    public Text stone;
    public Text food;
    public Text gold;
    void OnEnable()
    {
        ResourceManager.OnChanged += Refresh;
    }
    void OnDisable()
    {
        ResourceManager.OnChanged -= Refresh;
    }
    void Start()
    {
        Refresh();
    }
    void Refresh()
    {
        if (ResourceManager.Instance == null) return;
        wood.text = ResourceManager.Instance.Get(ResourceType.Wood).ToString();
        stone.text = ResourceManager.Instance.Get(ResourceType.Stone).ToString();
        food.text = ResourceManager.Instance.Get(ResourceType.Food).ToString();
        gold.text = ResourceManager.Instance.Get(ResourceType.Gold).ToString();
    }
}
