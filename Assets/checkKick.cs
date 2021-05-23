using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkKick : MonoBehaviour
{
    public GameObject player;
    public GameObject canvasKicked;
    public bool flagIn = false;

    // If moderator presses the kick button (unrelated with this script), GameObject "player" is destroyed, therefore "player" is null and should notify the user of the event
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
