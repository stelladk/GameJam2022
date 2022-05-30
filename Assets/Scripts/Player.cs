using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    Animator animator;
    SpriteRenderer sprite;

    void Awake()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
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
            onHurt();
        }
        if(col.gameObject.tag == "Toxic"){
            // TakeDamage(200);
            ToxicDeath();
        }
    }

    public void onHurt()
    {
        animator.SetTrigger("isHurt");
        Color originalColor = sprite.color;
        sprite.color = new Color(1,0,0,1);
        StartCoroutine(resetColor(new Color(1,1,1,1)));
    }

    void ToxicDeath()
    {
        bool hasPowers = GameManager.Instance.GetPowers();
        if (hasPowers) return;
        GameManager.Instance.OnToxicDeath();
    }

    IEnumerator resetColor(Color originalColor)
    {
        yield return new WaitForSeconds(.5f);
        sprite.color = originalColor;
    }
}
