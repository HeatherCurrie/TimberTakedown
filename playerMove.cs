using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class playerMove : MonoBehaviour
{
    public float speed;
    private Rigidbody2D myRigidBody;
    private Vector2 movement;
    private playerCombat playerCombat; 
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerCombat = GetComponent<playerCombat>();
    }

    // Update is called once per frame
    void Update()
    {
        // Setting x and y of movement vector
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        // Change position
        myRigidBody.MovePosition(myRigidBody.position + movement.normalized * speed * Time.fixedDeltaTime);

        // Animate players movement, stops movement animation if the player is attacking
        if (playerCombat.GetMidAttack() == false)
        {
            anim.SetFloat("moveX", Mathf.Abs(movement.x));
            anim.SetFloat("moveY", Mathf.Abs(movement.y));
        } else
        {
            anim.SetFloat("moveX", 0);
            anim.SetFloat("moveY", 0);
        }
    }
}
