using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disco : MonoBehaviour
{
    public Light discoLight = null;

    void Update()
    {
        discoLight.color = Random.ColorHSV();
    }
}
