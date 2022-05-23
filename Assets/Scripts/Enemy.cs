using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    // [SerializeField] int maxHealth = 100;
    // int currentHealth;

    // void Start()
    // {
    //     currentHealth = maxHealth;
    // }

    // public void TakeDamage(int damage)
    // {
    //     currentHealth -= damage;

    //     if(currentHealth <= 0){
    //         Die();
    //     }
    // }

    public override void Die()
    {
        Debug.Log("Enemy died!");
    }
}
