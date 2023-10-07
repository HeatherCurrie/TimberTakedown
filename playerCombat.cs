using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCombat : MonoBehaviour
{
    public resources wood;
    public Transform attackPoint;
    public LayerMask enemyLayer;
    public int attackDamage = 30;

    private float attackRange = 0.5f;

    public float attackTime;
    private float time = 0.8f;
    private bool midAttack;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // If the player isnt attacking, allow an attack to occur
        if (midAttack == false)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                midAttack = true;
                attackTime = time;
                anim.SetBool("Attacking", true);
                Attack();
            }
        }

        // Add time to timer
        if (attackTime > 0)
        {
            attackTime -= Time.deltaTime;

        }
        // When attack is finished
        else if (attackTime <= 0)
        {
            midAttack = false;
            anim.SetBool("Attacking", false);
        }
    }

    // Attack enemies
    void Attack()
    {
        // Detect enemies
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        //Damage enemies
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<enemyCombat>().TakeDamage(attackDamage);
        }
    }

    public bool GetMidAttack()
    {
        return midAttack;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
