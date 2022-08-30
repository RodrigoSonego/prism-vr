using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Cylinder : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(transform.localScale.x, 1.0f, transform.localScale.x);
    }
}
