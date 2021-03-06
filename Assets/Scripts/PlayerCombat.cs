using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] Transform attackPoint;
    [SerializeField] float meleeAttackRange = 0.5f;
    // [SerializeField] Transform rangedAttackPoint;
    // [SerializeField] float rangedAttackRange = 1f;
    [SerializeField] GameObject attackSprite;
    [SerializeField] LayerMask enemyLayer;

    InputHandler inputHandler;
    Animator animator;
    Player player;

    void Awake()
    {
        inputHandler = GetComponent<InputHandler>();
        animator = GetComponent<Animator>();
        player = GetComponent<Player>();
    }
    

    void Update()
    {
        handleAttacks();
    }

    void handleAttacks()
    {
        if (inputHandler.isAttackPressed) {
            MeleeAttack();
            animator.SetTrigger("MeleeAttack");
            inputHandler.isAttackPressed = false;
        }
        if (inputHandler.isRangedAttackPressed) {
            RangedAttack();
            animator.SetTrigger("RangedAttack");
            inputHandler.isRangedAttackPressed = false;
        }
    }

    public void MeleeAttack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, meleeAttackRange, enemyLayer);
        foreach(Collider2D enemy in hitEnemies){
            Debug.Log("Hit enemy melee "+ enemy.name);
            enemy.GetComponent<Enemy>().TakeDamage(player.meleeDamage);
        }
    }

    public void RangedAttack()
    {
        bool hasPowers = GameManager.Instance.GetPowers();
        if (hasPowers) {
            Instantiate(attackSprite, attackPoint.position, attackPoint.rotation);
        }

        // Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(rangedAttackPoint.position, rangedAttackRange, enemyLayer);
        // foreach(Collider2D enemy in hitEnemies){
        //     Debug.Log("Hit enemy ranged "+ enemy.name);
        // }
    }

    void OnDrawGizmosSelected()
    {
        if(attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint. position, meleeAttackRange);
    }
}
