using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class FlyMovement : MonoBehaviour
{
    public float jumpForce = 1.0f;
    public float horizontalSpeed = 100.0f;
    private Rigidbody rb;
    private bool jump = false;
    public int jumpsRemaining = 30; 

    [SerializeField]
    private ParticleSystem spaceParticle;

    [SerializeField] 
    private TMP_Text jumpsCounterText;



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jumpsCounterText.text = "Jumps: " + jumpsRemaining.ToString();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpsRemaining > 0)
        {
            jump = true;
            jumpsRemaining--;
            jumpsCounterText.text = "Jumps: " + jumpsRemaining.ToString(); 
            spaceParticle.Play();
        }

        if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector3(horizontalSpeed, rb.velocity.y, rb.velocity.z);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector3(-horizontalSpeed, rb.velocity.y, rb.velocity.z);
        }
        else
        {
            rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);
        }
    }

    void FixedUpdate()
    {
        if (jump && rb.velocity.y < 1.5f)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        jump = false;
    }
    public void IncrementJumps(int amount)
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpsRemaining > 0)
        {
            if(amount>0)
            {
                jumpsRemaining+=amount;
            }
            jump = true;
            jumpsRemaining--;
            jumpsCounterText.text = "Jumps: " + jumpsRemaining.ToString();
            spaceParticle.Play();
        }
    }


}
