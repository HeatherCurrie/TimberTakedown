using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;
using static UnityEngine.GraphicsBuffer;

public class towerCombat : MonoBehaviour
{
    public Transform attackPoint;
    public GameObject projectile;
    public Transform projectileSpawnpoint;
    public Transform target = null;

    public int attackDamage = 20;
    public float range = 3f;
    private float attackRate = 1.5f;

    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        attackPoint = transform.Find("TowerRange").transform;
    }

    // Update is called once per frame
    void Update()
    {
        FindClosestEnemy();

        if (target != null)
        {
            // Timer between shots
            if (timer < attackRate)
            {
                timer += Time.deltaTime;
            }
            else
            {
                Attack();
                timer = 0;
            }
        }
    }

    // Finds the closest enemy to the tower
    void FindClosestEnemy()
    {
        float distance = 0f;
        float shortestDistance = Mathf.Infinity;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closestEnemy = null;
        target = null;

        // Find distance to each enemy
        foreach (GameObject enemy in enemies)
        {
            distance = Vector3.Distance(transform.position, enemy.transform.position);

            if (distance < shortestDistance)
            {
                closestEnemy = enemy;
                shortestDistance = distance;
            }
        }

        // Set and look at target if its in turrets range
        if (shortestDistance <= range && closestEnemy != null)
        {
            target = closestEnemy.transform;
            transform.up = closestEnemy.transform.position - transform.position;
        }
    }

    // Shoots the enemy
    void Attack()
    {
        audioManager.instance.PlayEffect(audioManager.instance.bullet);

        // Create bullet
        GameObject proj = (GameObject)Instantiate(projectile, projectileSpawnpoint.position, projectileSpawnpoint.rotation);
        projectile projectileS = proj.GetComponent<projectile>();

        // Call functions in projectile class, to make bullet move towards enemy then damage them
        if (projectileS != null)
        {
            projectileS.Chase(target);
            projectileS.Damage(attackDamage);
        }
    }
    
    // Draw towers range
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, range);
    }
}
