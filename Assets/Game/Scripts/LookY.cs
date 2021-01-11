using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookY : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float _sensitivity = 1.5f;
    [SerializeField]
    private float _inverted = -1;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float _mouseY = Input.GetAxis("Mouse Y");

        Vector3 newRotation = transform.localEulerAngles;
        newRotation.x += _mouseY * _sensitivity * _inverted;
        transform.localEulerAngles = newRotation;
    }
}
