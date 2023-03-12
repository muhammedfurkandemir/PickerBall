using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotaterPallet : MonoBehaviour
{
    [SerializeField] private float RotateDirection;
    bool startRotate;

    public void RotateIsStart()
    {
        startRotate = true;
    }
    void Update()
    {
        if (startRotate)
        {
            transform.Rotate(0,0,RotateDirection, Space.Self);
        }
        
    }
}
