using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotator : MonoBehaviour
{
    [SerializeField]
    private float _RotationSpeed;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.X))
        {
            transform.Rotate(Vector3.right * _RotationSpeed);
        }
        if (Input.GetKey(KeyCode.Y))
        {
            transform.Rotate(Vector3.up * _RotationSpeed);
        }
        if (Input.GetKey(KeyCode.Z))
        {
            transform.Rotate(Vector3.forward * _RotationSpeed);
        }

    }
}
