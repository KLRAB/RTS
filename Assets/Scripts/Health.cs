using UnityEngine;
public class Health : MonoBehaviour
{
    public int maxHP = 100;
    public bool isTownCenter = false;
    int hp;
    void Start()
    {
        hp = maxHP;
    }
    public void Take(int dmg)
    {
        hp -= dmg;
        if (hp <= 0) Die();
    }
    void Die()
    {
        if (isTownCenter)
        {
            Debug.Log("TownCenter destroyed — YOU LOSE (placeholder)");
            // Time.timeScale = 0f;
        }
        Destroy(gameObject);
    }
}
