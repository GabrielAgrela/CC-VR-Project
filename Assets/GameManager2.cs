using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager2 : MonoBehaviourPunCallbacks 
{
	public GameObject PlayerPrefab;
    // Start is called before the first frame update
    private void Awake()
    {
        PhotonNetwork.Instantiate(PlayerPrefab.name,new Vector3(30f,6f,30f),Quaternion.identity,0);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
