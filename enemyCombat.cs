using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using static UnityEngine.GraphicsBuffer;

public class enemyCombat : MonoBehaviour
{
    public Transform attackPoint;
    public LayerMask playerLayer;
    public resources wood;
    public int currentHealth;
    public float attackDamage = 1f;
    public int reward;
    private float attackRange = 0.5f;
    private float timer = 0;
    private float attackRate = 1f;

    private float plankDamage = 10f;
    private float stumpDamage = 20f;
    private float treeDamage = 50f;

    private waves waveInstance;

    // Start is called before the first frame update
    void Start()
    {
        waveInstance = GameObject.FindGameObjectWithTag("GameManager").GetComponent<waves>();
        attackDamage = 1f;

        // Set damage for each type of enemy
        if (gameObject.name == "Plank(Clone)")
        {
            attackDamage = plankDamage + attackDamage * waveInstance.damageMultiplier;
        } else if (gameObject.name == "Stump(Clone)")
        {
            attackDamage = stumpDamage + attackDamage * waveInstance.damageMultiplier;
        } else
        {
            attackDamage = treeDamage + attackDamage * waveInstance.damageMultiplier;
        }

        attackPoint = transform.Find("EnemyAttackPoint").transform;
        wood = GameObject.FindObjectOfType(typeof(resources)) as resources;
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    // Enemy attacks the player
    public void Attack()
    {
        if (attackPoint != null)
        {
            // Detect player
            Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

            // Timer to ensure the enemies do not spam damage
            if (timer < attackRate)
            {
                timer += Time.deltaTime;
            }
            else
            {
                // Damages player
                foreach (Collider2D player in hitPlayer)
                {
                    player.GetComponent<health>().TakeDamage(attackDamage);
                }

                timer = 0;
            }
        }
    }

    // If this enemy is attacked
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Particle effect
        GetComponent<ParticleSystem>().Play();
        ParticleSystem.EmissionModule emmit = GetComponent<ParticleSystem>().emission;
        emmit.enabled = true;

        // Sound effect
        audioManager.instance.PlayEffect(audioManager.instance.damage);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Disables the collider
        GetComponent<Collider2D>().enabled = false;

        // Gives player wood on dealth
        wood.AddResources(reward);

        // Disables the script
        this.enabled = false;

        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}

