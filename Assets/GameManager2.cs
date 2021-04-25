using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager2 : MonoBehaviourPunCallbacks 
{
	public GameObject PlayerPrefab;
    public GameObject SnowMap;
    public GameObject ForestMap;
    // Start is called before the first frame update
    private void Awake()
    {
        PhotonNetwork.Instantiate(SnowMap.name, new Vector3(1f, -26f, 45f), Quaternion.identity, 0);
        PhotonNetwork.Instantiate(PlayerPrefab.name,new Vector3(18f,3f,40f),Quaternion.identity,0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.F))
        {
            PhotonNetwork.Destroy(GameObject.Find("SnowMap(Clone)"));
            PhotonNetwork.Instantiate(ForestMap.name, new Vector3(24.71502f, -10f, 17.18244f), Quaternion.identity, 0);
        }
    }
}
