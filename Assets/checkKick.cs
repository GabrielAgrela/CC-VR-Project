using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkKick : MonoBehaviour
{
    public GameObject player;
    public GameObject canvasKicked;
    public bool flagIn = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null && flagIn == false)
        {
            flagIn = true;
            Debug.Log("test1 " + player);
            Instantiate(canvasKicked);
        }
            
        else{ Debug.Log("test2 " + player); }
        

    }
}
