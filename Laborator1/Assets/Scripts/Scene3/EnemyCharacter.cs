using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : MonoBehaviour
{
    private int Health = 3;

    public int GetHealth()
    {
        return Health;
    }

    public void EnemyColision(int damage)
    {
        Health -= damage;
        Debug.Log("Enemy Health: " + Health);
        if (Health <= 0)
        {
            Debug.Log("Enemy Health: " + Health);
            Destroy(gameObject);
        }
        transform.Translate(Vector3.back * 0.5f);
    }

    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
    }

    void Update()
    {
        Debug.Log("Enemy Health: " + Health);
    }
}
