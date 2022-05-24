using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public GameObject deathEffect;
    public int maxHealth = 100;
    public int meleeDamage = 40;
    protected int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if(currentHealth <= 0){
            Die();
        }
    }

    public abstract void Die();
}
