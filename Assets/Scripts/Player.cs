using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Die()
    {
        Debug.Log("YOU DIED");
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.gameObject.tag);
        if (col.gameObject.tag == "Deadly")
        {
            TakeDamage(10);
        }
        if(col.gameObject.tag == "Toxic"){
            TakeDamage(200);
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
