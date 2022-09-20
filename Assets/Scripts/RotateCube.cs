
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCube : MonoBehaviour
{
    public float speed = 150f;

    // Update is called once per frame
    

    void OnMouseDrag()
    {
        float rotX = Input.GetAxis("Mouse X") * speed * Mathf.Deg2Rad;

        transform.Rotate(Vector3.forward, rotX);
        
        
    }
}
