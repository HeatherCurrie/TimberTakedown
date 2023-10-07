using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    private Transform target;
    private int damage;
    public float speed = 4f;

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            Vector2 direction = target.position - transform.position;
            float distanceThisFrame = speed * Time.deltaTime;

            // projectile hits target
            if (direction.magnitude <= distanceThisFrame)
            {
                Hit();
            }

            // move bullet
            transform.Translate (direction.normalized * distanceThisFrame, Space.World);

        } else
        {
            Destroy(gameObject);
            return;
        }
    }

    // Set target to targetEnemy from tower combat class
    public void Chase(Transform targetEnemy)
    {
        target = targetEnemy;
    }

    // Damage enemy, damage from tower combat class
    public void Damage(int towerDamage)
    {
        damage = towerDamage;
    }

    // Destroys the bullet and damages the enemy
    void Hit()
    {
        Destroy(gameObject);
        target.GetComponent<enemyCombat>().TakeDamage(damage);
    }
}
