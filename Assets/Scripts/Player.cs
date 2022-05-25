using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        
    }

    public override void Die()
    {
        Debug.Log("YOU DIED");
        animator.SetTrigger("Death");
        GameManager.Instance.OnDeath();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.gameObject.tag);
        if (col.gameObject.tag == "Deadly")
        {
            TakeDamage(10);
        }
        if(col.gameObject.tag == "Toxic"){
            // TakeDamage(200);
            ToxicDeath();
        }
    }

    void ToxicDeath()
    {
        bool hasPowers = GameManager.Instance.GetPowers();
        if (hasPowers) return;
        GameManager.Instance.OnToxicDeath();
    }
}
