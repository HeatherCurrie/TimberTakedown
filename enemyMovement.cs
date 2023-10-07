using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement : MonoBehaviour
{
    public float speed;
    public Transform target;
    public Rigidbody2D myRigidBody;

    // Start is called before the first frame update
    void Start()
    {
        // Set target of movement to the player (if player exists)
        if (GameObject.Find("Player") == true)
        {
            target = GameObject.Find("Player").transform;
        } else
        {
            target = null;

            // Disables the collider
            GetComponent<Collider2D>().enabled = false;

            // Disables the script
            this.enabled = false;

            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Move enemy towards player
        if (target != null)
        {
            Vector3 position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            myRigidBody.MovePosition(position);
        }
    }
}
