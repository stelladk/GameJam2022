using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    [SerializeField] float speed = 20f;
    public int spellDamage = 40;
    public GameObject damageEffect;
    Rigidbody2D rigidbody;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        rigidbody.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D hit)
    {
        // Avoid friendly fire
        Player player = hit.GetComponent<Player>();
        if(player != null) return;

        Debug.Log("Spell hit "+ hit.name);

        // Damage enemy
        Enemy enemy = hit.GetComponent<Enemy>();
        if(enemy != null) enemy.TakeDamage(spellDamage);

        // Impact Effect
        Instantiate(damageEffect, transform.position, transform.rotation);

        // End Spell
        Destroy(gameObject);
    }
}
