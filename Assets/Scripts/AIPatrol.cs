using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrol : MonoBehaviour
{
    [SerializeField] float lookRadius = 20f;
    public float stoppindDistance = 1f;
    public float cooldown = 1f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float walkSpeed = 20f;
    public bool mustPatrol;

    Rigidbody2D rb;
    Collider2D collider;
    Animator animator;

    public Transform target;

    private bool mustTurn;

    void Awake()
    {
        // Debug.Log(GameManager);
        // target = GameManager.Instance.player.transform;

        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        mustPatrol = true;
        mustTurn = false;
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
            StartCoroutine(Attack());
        }else{
            mustPatrol = true;
        }
    }

    void FixedUpdate()
    {
        if(mustPatrol) {
            mustTurn = !Physics2D.OverlapCircle(groundCheck.position, 0.5f, groundLayer);
            Debug.Log(mustTurn);
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
        Debug.Log("Attack");
        yield return new WaitForSeconds(cooldown);
        //animator
        // Sphere collider for melle
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
