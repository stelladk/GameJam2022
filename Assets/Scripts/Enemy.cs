using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    [SerializeField] float lookRadius = 20f;
    Transform target;
    NavMeshAgent agent;

    void Start()
    {
        target = GameManager.Instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        ChaseTarget();
    }

    void ChaseTarget()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius){
            agent.SetDestination(target.position);

            if (distance <= agent.stoppingDistance){
                //Attack
                FaceTarget();
            }
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }


    public override void Die()
    {
        Debug.Log("Enemy died!");
        Instantiate(deathEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
