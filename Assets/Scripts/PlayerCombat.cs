using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] Transform meleeAttackPoint;
    [SerializeField] float meleeAttackRange = 0.5f;
    [SerializeField] Transform rangedAttackPoint;
    [SerializeField] float rangedAttackRange = 1f;
    [SerializeField] Texture2D attackSprite;
    [SerializeField] LayerMask enemyLayer;
    

    void Update()
    {

    }

    public void MeleeAttack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(meleeAttackPoint.position, meleeAttackRange, enemyLayer);
        foreach(Collider2D enemy in hitEnemies){
            Debug.Log("Hit enemy melee "+ enemy.name);
        }
    }

    public void RangedAttack()
    {
        GameObject sprite = new GameObject();
        SpriteRenderer rend = sprite.AddComponent(typeof(SpriteRenderer)) as SpriteRenderer;
        rend.material.mainTexture = attackSprite;

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(rangedAttackPoint.position, rangedAttackRange, enemyLayer);
        foreach(Collider2D enemy in hitEnemies){
            Debug.Log("Hit enemy ranged "+ enemy.name);
        }
    }
}
