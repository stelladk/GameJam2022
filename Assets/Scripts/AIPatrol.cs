using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrol : MonoBehaviour
{
    [SerializeField] float lookRadius = 20f;
    public float stoppindDistance = 1f;
    [SerializeField] Transform attackPoint;
    [SerializeField] LayerMask playerLayer;
    public float attackRange = 0.5f;
    public float cooldown = 1f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float walkSpeed = 20f;
    public bool mustPatrol;

    Rigidbody2D rb;
    Collider2D collider;
    Animator animator;
    Enemy enemy;

    Transform target;

    private bool mustTurn;
    private bool canAttack;

    void Awake()
    {
        // Debug.Log(GameManager);
        target = GameManager.Instance.player.transform;
        // target = GameObject.FindWithTag("Player").transform;

        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        enemy = GetComponent<Enemy>();
    }

    void Start()
    {
        mustPatrol = true;
        mustTurn = false;
        canAttack = true;
    }

    void Update()
    {
        if (mustPatrol){
            Patrol();
        }

        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius){
            FaceTarget();
            mustPatrol = false;
            ChaseTarget();
            if(canAttack){
                StartCoroutine(Attack());
            }
        }else{
            mustPatrol = true;
        }
    }

    void FixedUpdate()
    {
        animator.SetBool("isWalking", mustPatrol);
        if(mustPatrol) {
            mustTurn = !Physics2D.OverlapCircle(groundCheck.position, 0.5f, groundLayer);
        }
    }

    void Patrol()
    {
        if (mustTurn || collider.IsTouchingLayers(groundLayer)){
            Turn();
        }
        rb.velocity = new Vector2(walkSpeed * Time.fixedDeltaTime, rb.velocity.y);
    }

    void Turn()
    {
        mustPatrol = false;
        // transform.Rotate(0f, 180f, 0f);
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        walkSpeed *= -1;
        mustPatrol = true;
    }

    IEnumerator Attack()
    {
        canAttack = false;
        animator.SetTrigger("Attack");
        // Sphere collider for melle
        Collider2D hitPlayer = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayer);
        Debug.Log("Hit player? "+ hitPlayer);
        if(hitPlayer){
            hitPlayer.GetComponent<Player>().TakeDamage(enemy.meleeDamage);
        }

        yield return new WaitForSeconds(cooldown);
        canAttack = true;
    }

    void FaceTarget()
    {
        if (((target.position.x > transform.position.x) && (transform.localScale.x < 0)) 
        || ((target.position.x < transform.position.x) && (transform.localScale.x > 0))){
            Turn();
        }
    }

    void ChaseTarget()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if(distance > stoppindDistance){
            rb.velocity = new Vector2(walkSpeed * Time.fixedDeltaTime, rb.velocity.y);
        }
    }
}
