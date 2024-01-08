using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerController : MonoBehaviour
{

    bool hasDoll = false;
    bool hasLight = false;
    bool hasGem = false;
    public bool sageMissionStarted = false;
    public bool hasMap = false;
    int orbsHeld = 0;
    int peopleStolenFrom = 0;
    int peopleHelped = 0;
    Animator animator;
    SpriteRenderer spriteRenderer;
    int money = 0;

    public float speed = 10;

    bool idleThisFrame = true;
    bool idleLastFrame = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Light2D globalLight;

    public int maxHealth = 3;
    public int currentHealth;
    public float attackRange = 1.0f;
    public LayerMask enemyLayers;
    public int attackDamage = 1;

    public EndingManager endingManager;

    private void Start() {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
        endingManager = GameObject.Find("EndingManager").GetComponent<EndingManager>();
    }

    private void Update() {
        idleLastFrame = idleThisFrame;
        Vector2 directionVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (directionVector.magnitude > 1) {
            directionVector.Normalize();
        }
        rb.velocity = directionVector * speed;

        idleThisFrame = directionVector.magnitude == 0;

        animator.SetBool("Idle", idleThisFrame && idleLastFrame);
        animator.SetFloat("Vertical", directionVector.y);
        animator.SetFloat("Horizontal", directionVector.x);

        spriteRenderer.flipX = ((Input.mousePosition.x / Screen.width) - 0.5f < 0 && animator.GetBool("IsAttacking") ) || directionVector.x < 0;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack();

            // Set the attacking flag
            animator.SetBool("IsAttacking", true);

            // Optionally, use the player's current direction for the attack direction
            animator.SetFloat("AttackX", (Input.mousePosition.x / Screen.width) * 2 - 1);
            animator.SetFloat("AttackY", (Input.mousePosition.y / Screen.height) * 2 - 1);

            

            StartCoroutine(ResetAttack());

            Debug.Log("Player attacked!");
        }

    }

    IEnumerator ResetAttack()
    {
        // Wait for a set amount of time
        yield return new WaitForSeconds(0.1f); // Adjust time as needed

        // Turn off the attacking flag
        animator.SetBool("IsAttacking", false);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        StartCoroutine(FlashSprite(spriteRenderer, 1.0f, 0.5f));
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    IEnumerator FlashSprite(SpriteRenderer sprite, float duration, float blinkTime)
    {
        float endTime = Time.time + duration;
        while (Time.time < endTime)
        {
            sprite.color = Color.red; // Or any color to indicate damage
            yield return new WaitForSeconds(blinkTime);
            sprite.color = Color.white; // Restore original color
            yield return new WaitForSeconds(blinkTime);
        }
    }

    void Die()
    {
        //UnityEditor.EditorApplication.isPlaying = false;
        Debug.Log("Player died!");
        Time.timeScale = 0;
        endingManager.ShowEnding(4);
    }

    public void Attack()
    {
        // Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayers);

        // Apply damage to those enemies
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyAI>().TakeDamage(attackDamage);
        }

    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Doll")) {
            GetComponent<DialogueTrigger>().TriggerDialogue(0); // picking up doll message
            other.gameObject.SetActive(false);
            hasDoll = true;
        }
        if (other.CompareTag("Orb")) {
            other.gameObject.SetActive(false);
            orbsHeld += 1;
        }
        if (other.CompareTag("Cave")) {
            GetComponent<Light2D>().enabled = true;
            globalLight.enabled = false;
        }
        if (other.CompareTag("Gem")) {
            hasGem = true;
            other.gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Cave")) {
            GetComponent<Light2D>().enabled = false;
            globalLight.enabled = true;
        }
    }

    public void GiveLight() {
        hasLight = true;
        peopleHelped += 1;
        GetComponent<Light2D>().pointLightOuterRadius = 3.5f;
    }

    public void TakeLight() {
        hasLight = false;
        GetComponent<Light2D>().pointLightOuterRadius = 1;
    }


    public bool HasDoll() {
        return hasDoll;
    }
    public void GiveDoll() {
        if (hasDoll) {
            hasDoll = false;
            peopleHelped += 1;
            hasMap = true;
            if (sageMissionStarted) {
                FindFirstObjectByType<OrbManager>().StartWaypoints();
            }
        }
    }

    public int OrbsHeld() {
        return orbsHeld;
    }

    public bool HasGem() {
        return hasGem;
    }

    public void GiveGem() {
        if (hasGem) {
            hasGem = false;
            peopleHelped += 1;
        }
    }

    public void AddMoney(int value) {
        peopleStolenFrom += 1;
        money += value;
        FindFirstObjectByType<CoinUI>().UpdateCount(money);
    }

    public int GetPeopleHelped() {
        return peopleHelped;
    }

    public int getPeopleStolenFrom() {
        return peopleStolenFrom;
    }
}
