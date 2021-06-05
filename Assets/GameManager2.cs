using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.XR.Management;
using UnityEngine.SceneManagement;
public class GameManager2 : MonoBehaviourPunCallbacks 
{
	public GameObject chosenPrefab;
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
    

    public IEnumerator StartXR()
    {
        StopXR();
        Debug.Log("Initializing XR...");
        yield return XRGeneralSettings.Instance.Manager.InitializeLoader();

        if (XRGeneralSettings.Instance.Manager.activeLoader == null)
        {
            Debug.LogError("Initializing XR Failed. Check Editor or Player log for details.");
        }
        else
        {
            Debug.Log("Starting XR...");
            XRGeneralSettings.Instance.Manager.StartSubsystems();
        }
    }

    public void StopXR()
    {
        Debug.Log("Stopping XR...");

        XRGeneralSettings.Instance.Manager.StopSubsystems();
        XRGeneralSettings.Instance.Manager.DeinitializeLoader();
        Debug.Log("XR stopped completely.");
    }

    // On unfocusing text input, kick input's string named player from the lobby
    public static void kick(string usernameString) 
    {
        // With PhotonNetwork, players are stored as KeyValuePairs
        foreach (KeyValuePair<int, Photon.Realtime.Player> player in PhotonNetwork.CurrentRoom.Players) 
        {
            if (!player.Value.IsLocal && usernameString.Equals(player.Value.NickName))
            {
                PhotonNetwork.CloseConnection(player.Value); 
                break; 
            } 
        } 
    }

    // Spawn map for every player and spawn the player GameObject itself
    private void Awake()
    {

        if (PhotonNetwork.IsMasterClient == true)
        { 
            StartCoroutine(StartXR());
            PhotonNetwork.Instantiate(MeadowMap.name, new Vector3(0f, -0f, 17.18244f), Quaternion.identity, 0);
            Canvas.SetActive(true);
        }
        else
        {
            StartCoroutine(StartXR());
        }
        chosenPrefab = GameManager.chosenPrefab;
        PhotonNetwork.Instantiate(chosenPrefab.name, new Vector3(18f, 10f, 40f), Quaternion.identity, 0);
    }

    // if map button clicked by the moderator, send changeMap() as RPC, making every client know the map is changing, so they block their vision. After a second, instantiate the new map to every client, destroying the previous one.
    public void SpawnSnowMap()
    {
        if (PhotonNetwork.IsMasterClient == true)
        {
            photonView.RPC("changeMap", RpcTarget.All);
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
            photonView.RPC("changeMap", RpcTarget.All);
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
            photonView.RPC("changeMap", RpcTarget.All);
            PhotonNetwork.Destroy(GameObject.FindWithTag("Map"));
            PhotonNetwork.Instantiate(MeadowMap.name, new Vector3(0f, -0f, 17.18244f), Quaternion.identity, 0);
        }
    }
    public void SpawnDesertMap()
    {
        if (PhotonNetwork.IsMasterClient == true)
        {
            photonView.RPC("changeMap", RpcTarget.All);
            PhotonNetwork.Destroy(GameObject.FindWithTag("Map"));
            PhotonNetwork.Instantiate(DesertMap.name, new Vector3(0f, 7f, 17.18244f), Quaternion.identity, 0);
        }
    }
    public void SpawnForestMap()
    {
        if (PhotonNetwork.IsMasterClient == true)
        {
            photonView.RPC("changeMap", RpcTarget.All);
            PhotonNetwork.Destroy(GameObject.FindWithTag("Map"));
            PhotonNetwork.Instantiate(ForestMap.name, new Vector3(0f, -0f, 17.18244f), Quaternion.identity, 0);
        }
    }

    // On clicking a weather button, destroy the previous weather element and instantiate the new one to every client.
    public void SpawnSnow()
    {
        if (PhotonNetwork.IsMasterClient == true)
        {
            PhotonNetwork.Destroy(GameObject.FindWithTag("Weather"));
            PhotonNetwork.Instantiate(Snow.name, new Vector3(0f, 7.3f, 12f), Quaternion.identity, 0);
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

    // Set changingMap to true to every client
    [PunRPC]
    void changeMap()
    {
        changingMap = true;
        Invoke("resetChangingMap", 5);
    }

    // After 5 seconds of having their vision blocked, unblock them by sending an RPC setting changing map to false
    public void resetChangingMap()
    {
        if (PhotonNetwork.IsMasterClient == true)
        {
            photonView.RPC("resetChangeMap", RpcTarget.All);
        }
    }

    // Set changingMap to false to every client
    [PunRPC]
    void resetChangeMap()
    {
        changingMap = false;
    }



}
