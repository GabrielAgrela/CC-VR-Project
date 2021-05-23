using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowStartScript : MonoBehaviour
{
    // Programatically change this weather element rotation
    void Update()
    {
        transform.Rotate(90.0f, 0f, 0.0f, Space.World);
    }
}
