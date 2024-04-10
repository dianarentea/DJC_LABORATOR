using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScaler : MonoBehaviour
{
    
    // Start is called before the first frame update
    [SerializeField]
    private float _ScaleFactor;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.P))
        {
            transform.localScale += Vector3.one * _ScaleFactor * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.O))
        {
            transform.localScale -= Vector3.one * _ScaleFactor * Time.deltaTime;
        }

    }
}
