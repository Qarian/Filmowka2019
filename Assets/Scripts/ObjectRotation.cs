using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotation : MonoBehaviour
{
    public float xRotationSpeed = 1;
    public float yRotationSpeed = 1;
    public float zRotationSpeed = 1;
    void Update()
    {
        transform.Rotate(new Vector3(xRotationSpeed,yRotationSpeed,zRotationSpeed) * Time.deltaTime);
    }
}
