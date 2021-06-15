using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowScript : MonoBehaviour
{
    // Programatically change this weather element rotation
    void Start()
    {
        transform.Rotate(90.0f, 0f, 0.0f, Space.World);
    }

}
