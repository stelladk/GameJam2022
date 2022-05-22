using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] Transform meleeAttackPoint;
    [SerializeField] float meleeAttackRange = 0.5f;
    [SerializeField] LayerMask enemyLayer;
    

    void Update()
    {

    }

    public void MeleeAttack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(meleeAttackPoint.position, meleeAttackRange, enemyLayer);
        foreach(Collider2D enemy in hitEnemies){
            Debug.Log("Hit enemiy "+ enemy.name);
        }
    }
}
