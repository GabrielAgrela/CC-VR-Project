using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class followPlayer : MonoBehaviour
{
    public Transform transformPlayer;
    public PhotonView photonView;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (photonView.IsMine)
        {
            //transform.position = new Vector3(transformPlayer.position.x + 100, transform.position.y + 100, transformPlayer.position.z+100);
        }
    }
}
