using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int jumpsIncrement = 5; 

    private void OnTriggerEnter(Collider other)
    {
        FlyMovement flyMovement = other.GetComponent<FlyMovement>();
        if (flyMovement != null)
        {
            flyMovement.IncrementJumps(jumpsIncrement);
            Destroy(gameObject); 
        }
    }
}
