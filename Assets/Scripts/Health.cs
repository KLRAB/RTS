using System;
using UnityEngine;
public class Health : MonoBehaviour
{
    public int maxHP = 100;
    public bool isTownCenter = false;
    int hp;
    public event Action<Health> Died;
    void OnGUI()
    {
        if (isTownCenter)
            GUI.Label(new Rect(10, 100, 300, 30), $"TownCenter HP: {hp}/{maxHP}");
    }
    void Start()
    {
        hp = maxHP; 
    }
    public void Take(int dmg)
    {
        hp -= dmg;
        if (isTownCenter)
            Debug.Log($"TOWN CENTER took {dmg} -> {hp}/{maxHP}");
        if (hp <= 0) Die();
    }
    void Die()
    {
        Died?.Invoke(this);
        if (isTownCenter)
        {
            if (GameManager.Instance != null) GameManager.Instance.Lose();
            else Debug.Log("TownCenter destroyed — YOU LOSE (no GameManager)");
        }
        Destroy(gameObject);
    }
}
