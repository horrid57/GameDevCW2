using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed = 0.05f;
    public float detectionRange = 5.0f;
    public float attackRange = 0.2f;
    private GameObject player;
    private int health = 1;
    private bool isPlayerInRange = true;
    private Animator animator;

    public int attackDamage = 1;
    public float attackRate = 1.0f;
    private float lastAttackTime;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (isPlayerInRange)
        {
            FollowPlayer();
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        if (Time.time - lastAttackTime >= attackRate && IsPlayerInRange())
        {
            AttackPlayer(player);
            lastAttackTime = Time.time;
            Debug.Log("Enemy attacked!");
        }
    }

    bool IsPlayerInRange()
    {
        return Vector3.Distance(transform.position, player.transform.position) < attackRange;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            isPlayerInRange = false;
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            isPlayerInRange = true;
        }
    }

    void FollowPlayer()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

        // Update animator parameters
        animator.SetBool("isMoving", true);
        animator.SetFloat("Horizontal 0", direction.x);
        animator.SetFloat("Vertical", direction.y);

    }

    public void AttackPlayer(GameObject player)
    {   
        player.GetComponent<PlayerController>().TakeDamage(attackDamage);
        Debug.Log("Enemy attacked!");
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        animator.SetTrigger("Die");
        Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length); // Waits for the animation to finish
        Debug.Log("Enemy died!");
    }
}

