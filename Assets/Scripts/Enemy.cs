using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    
    // public float speed = 20f;
    // public float nextWaypointDistance = 3f;
    // Transform target;
    // AIPatrol patrolAgent;
    // NavMeshAgent agent;


    // Seeker seeker;
    // Rigidbody2D rb;

    void Start()
    {
    //     target = GameManager.Instance.player.transform;
        // patrolAgent = GetComponent<AIPatrol>();
        // agent = GetComponent<NavMeshAgent>();
        // seeker = GetComponent<Seeker>();
        // rb = GetComponent<Rigidbody2D>();

        // seeker.StartPath(rb.position, target.position, OnPathComplete);
        currentHealth = maxHealth;
    }


    void Update()
    {

    }


    public override void Die()
    {
        Debug.Log("Enemy died!");
        Instantiate(deathEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
