using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookX : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float _sensitivity = 1.5f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float _mouseX = Input.GetAxis("Mouse X");
        
        Vector3 newRotation = transform.localEulerAngles;
        newRotation.y += _mouseX * _sensitivity;
        transform.localEulerAngles = newRotation;
    }
}
