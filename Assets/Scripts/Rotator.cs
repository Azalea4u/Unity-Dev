using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] [Range(-360, 360)] float angle;
    //[SerializeField] [Range(2, 20)] float speed = 5.0f;

    void Update()
    {
        transform.rotation *= Quaternion.AngleAxis(angle * Time.deltaTime, Vector3.up);
        
    }
}
