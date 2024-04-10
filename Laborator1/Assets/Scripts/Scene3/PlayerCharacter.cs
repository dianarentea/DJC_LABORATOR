using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public float speed = 5f;
    EnemyCharacter enemy;

    void Start()
    {
        //add gravity to the player
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
    }
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0f, vertical) * speed * Time.deltaTime;
        transform.Translate(movement);

        float upDownMovement = 0f;
        if (Input.GetKey(KeyCode.Q))
        {
            upDownMovement = -1f;
        }
        else if (Input.GetKey(KeyCode.E))
        { 
            upDownMovement = 1f; 
        }

        Vector3 upDown = new Vector3(0f, upDownMovement, 0f) * speed * Time.deltaTime;
        transform.Translate(upDown);

        if (Input.GetKeyDown(KeyCode.Space) && enemy!=null)
        {
            // check if the colliders are overlapping
            enemy.EnemyColision(1);
        }
    }

    //in colision
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyCharacter enemyScript = other.GetComponent<EnemyCharacter>();

            if (enemyScript != null)
            {
                enemy = enemyScript;
            }
        }
    }

    //no colision
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemy = null;
        }
    }

}
