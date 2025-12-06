using UnityEngine;

public class SeaMonsterHealth : MonoBehaviour
{
    public int health = 1;   // 1 hit kill

    public void TakeDamage(int amount)
    {
        health -= amount;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}