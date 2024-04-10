using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float _xScaleFactor;
    [SerializeField] 
    private float _yScaleFactor;
    void Start()
    {
        _xScaleFactor = 2.0f;
        Debug.Log(_xScaleFactor);
        Debug.Log(_yScaleFactor);
        transform.localScale = new Vector3(_xScaleFactor, _yScaleFactor, 1.0f);
        transform.position = Vector3.zero;
        transform.Rotate(90, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(0, 90, 0);


    }
}
