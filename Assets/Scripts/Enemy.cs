using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public int points = 10;

    void Start()
    {
        currentHealth = maxHealth;
    }


    public override void Die()
    {
        Debug.Log("Enemy died!");
        GameManager.Instance.increaseScore(points);
        Instantiate(deathEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
