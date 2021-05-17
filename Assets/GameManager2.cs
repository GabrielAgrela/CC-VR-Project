using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using System.Linq;

public class GameManager2 : MonoBehaviourPunCallbacks 
{
	public GameObject PlayerPrefab;
    public GameObject GameManager1;
    public GameObject SnowMap;
    public GameObject BeachMap;
    public GameObject MeadowMap;
    public GameObject DesertMap;
    public GameObject ForestMap;
    public GameObject Snow;
    public GameObject Rain;
    public GameObject Canvas;
    public PhotonView photonView;
    public InputField username;
    public bool changingMap = false;

    public static void kick(string usernameString) 
    {
        Debug.Log("entrou1 " + usernameString);
        foreach (KeyValuePair<int, Photon.Realtime.Player> player in PhotonNetwork.CurrentRoom.Players) 
        {
            Debug.Log("entrou1 "+ usernameString);
            Debug.Log(player.Value.NickName);
            if (!player.Value.IsLocal && usernameString.Equals(player.Value.NickName))
            {
                Debug.Log("entrou");
                PhotonNetwork.CloseConnection(player.Value); 
                break; 
            } 
        } 
    }

    // Start is called before the first frame update
    private void Awake()
    {
        if (PhotonNetwork.IsMasterClient == true)
        {
            PhotonNetwork.Instantiate(MeadowMap.name, new Vector3(0f, -0f, 17.18244f), Quaternion.identity, 0);
            Canvas.SetActive(true);
        }
        PlayerPrefab = GameManager.PlayerPrefab;
        PhotonNetwork.Instantiate(PlayerPrefab.name, new Vector3(18f, 10f, 40f), Quaternion.identity, 0);
    }

    // Update is called once per frame
    void Update()
    {
    }
    

    public void SpawnSnowMap()
    {
        if (PhotonNetwork.IsMasterClient == true)
        {
            ChangingMap();
            Invoke("SpawnSnowMapDelay", 1);
        }
    }
    public void SpawnSnowMapDelay()
    {
            PhotonNetwork.Destroy(GameObject.FindWithTag("Map"));
            PhotonNetwork.Instantiate(SnowMap.name, new Vector3(-30f, -10f, 40f), Quaternion.identity, 0);
    }


    public void SpawnBeachMap()
    {
        if (PhotonNetwork.IsMasterClient == true)
        {
            ChangingMap();
            Invoke("SpawnBeachMapDelay", 1);
        }
    }
    public void SpawnBeachMapDelay()
    {
        PhotonNetwork.Destroy(GameObject.FindWithTag("Map"));
        PhotonNetwork.Instantiate(BeachMap.name, new Vector3(0f, 7f, 17.18244f), Quaternion.identity, 0);
    }


    public void SpawnMeadowMap()
    {
        if (PhotonNetwork.IsMasterClient == true)
        {
            ChangingMap();
            PhotonNetwork.Destroy(GameObject.FindWithTag("Map"));
            PhotonNetwork.Instantiate(MeadowMap.name, new Vector3(0f, -0f, 17.18244f), Quaternion.identity, 0);
        }
    }
    public void SpawnDesertMap()
    {
        if (PhotonNetwork.IsMasterClient == true)
        {
            ChangingMap();
            PhotonNetwork.Destroy(GameObject.FindWithTag("Map"));
            PhotonNetwork.Instantiate(DesertMap.name, new Vector3(0f, 7f, 17.18244f), Quaternion.identity, 0);
        }
    }
    public void SpawnForestMap()
    {
        if (PhotonNetwork.IsMasterClient == true)
        {
            ChangingMap();
            PhotonNetwork.Destroy(GameObject.FindWithTag("Map"));
            PhotonNetwork.Instantiate(ForestMap.name, new Vector3(0f, -0f, 17.18244f), Quaternion.identity, 0);
        }
    }
    public void SpawnSnow()
    {
        if (PhotonNetwork.IsMasterClient == true)
        {
            PhotonNetwork.Destroy(GameObject.FindWithTag("Weather"));
            PhotonNetwork.Instantiate(Snow.name, new Vector3(0f, 20f, 12f), Quaternion.identity, 0);
        }
    }
    public void SpawnRain()
    {
        if (PhotonNetwork.IsMasterClient == true)
        {
            
            PhotonNetwork.Destroy(GameObject.FindWithTag("Weather"));
            PhotonNetwork.Instantiate(Rain.name, new Vector3(0f, 20f, 17.18244f), Quaternion.identity, 0);
        }
    }

    public void ChangingMap()
    {
        if (PhotonNetwork.IsMasterClient == true)
        {
            photonView.RPC("changeMap", RpcTarget.All);
        }
    }

    [PunRPC]
    void changeMap()
    {
        changingMap = true;
        Invoke("resetChangingMap", 5);
    }

    public void resetChangingMap()
    {
         if (PhotonNetwork.IsMasterClient == true)
         {
        photonView.RPC("resetChangeMap", RpcTarget.All);
        }
    }

    [PunRPC]
    void resetChangeMap()
    {
        changingMap = false;
    }



}
