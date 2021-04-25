using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager2 : MonoBehaviourPunCallbacks 
{
	public GameObject PlayerPrefab;
    public GameObject SnowMap;
    public GameObject ForestMap;
    public GameObject Canvas;
    // Start is called before the first frame update
    private void Awake()
    {
        if (PhotonNetwork.IsMasterClient == true)
        {
            PhotonNetwork.Instantiate(SnowMap.name, new Vector3(1f, -26f, 45f), Quaternion.identity, 0);
            Canvas.SetActive(true);
        }
        PhotonNetwork.Instantiate(PlayerPrefab.name, new Vector3(18f, 3f, 40f), Quaternion.identity, 0);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SpawnSnowMap()
    {
        if (PhotonNetwork.IsMasterClient == true)
        {
            PhotonNetwork.Destroy(GameObject.Find("SnowMap(Clone)"));
            PhotonNetwork.Instantiate(ForestMap.name, new Vector3(24.71502f, -15f, 17.18244f), Quaternion.identity, 0);
        }
    }
}
