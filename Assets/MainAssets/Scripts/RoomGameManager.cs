using System.Collections;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.XR.Management;
using UnityEngine.SceneManagement;
public class RoomGameManager : MonoBehaviourPunCallbacks 
{
	public GameObject chosenPrefab;

    public GameObject Canvas;
    public PhotonView photonView;
    public InputField username;
    public bool changingMap = false;
    
    //VR initialization methods
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

    // On unfocusing text input, kick input's string named player from the room
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

    // Spawn map for every player and spawn the player GameObject itself, Also turns on XRVR depending on whether the client is a moderator
    private void Awake()
    {

        if (PhotonNetwork.IsMasterClient == true)
        {
            PhotonNetwork.Instantiate("DesertMap", new Vector3(0f, 7f, 17.18244f), Quaternion.identity, 0);
            Canvas.SetActive(true);
        }
        else
        {
            try
            {
                StartCoroutine(StartXR());
            }
            catch (Exception e)
            {
                print("error starting XRVR: "+e);
            }
            
        }
        chosenPrefab = LauncherGameManager.chosenPrefab;
        PhotonNetwork.Instantiate(chosenPrefab.name, new Vector3(18f, 10f, 40f), Quaternion.identity, 0);
    }

    // if map button clicked by the moderator, send changeMapFlag() as RPC, making every client know the map is changing, so they block their vision. After a second, instantiate the new map to every client, destroying the previous one.
    public void SpawnMap(string map)
    {
        if (PhotonNetwork.IsMasterClient == true)
        {
            photonView.RPC("changeMapFlag", RpcTarget.All);
            PhotonNetwork.Destroy(GameObject.FindWithTag("Map"));
            switch (map)
            {
                case "SnowMap":
                    PhotonNetwork.Instantiate(map, new Vector3(-30f, -10f, 40f), Quaternion.identity, 0);
                    break;
                case "MeadowMap":
                    PhotonNetwork.Instantiate(map, new Vector3(0f, -0f, 17.18244f), Quaternion.identity, 0);
                    break;
                case "ForestMap":
                    PhotonNetwork.Instantiate(map, new Vector3(0f, -0f, 17.18244f), Quaternion.identity, 0);
                    break;
                case "DesertMap":
                    PhotonNetwork.Instantiate(map, new Vector3(0f, 7f, 17.18244f), Quaternion.identity, 0);
                    break;
                case "BeachMap":
                    PhotonNetwork.Instantiate(map, new Vector3(0f, 7f, 17.18244f), Quaternion.identity, 0);
                    break;
                default:
                    print("Wrong map string");
                    break;
            }
        }
    }

    // On clicking a weather button, destroy the previous weather element and instantiate the new one to every client.
    public void SpawnWeather(string weather)
    {
        PhotonNetwork.Destroy(GameObject.FindWithTag("Weather"));
        switch (weather)
        {
            case "SnowWeather":
                PhotonNetwork.Instantiate(weather, new Vector3(0f, 7.31f, 12f), Quaternion.identity, 0);
                break;
            case "RainWeather":
                PhotonNetwork.Instantiate(weather, new Vector3(0f, 20f, 17.18244f), Quaternion.identity, 0);
                break;
            default:
                print("Wrong weather string");
                break;
        }
    }

    // On clicking a melody button, destroy the previous melody element and instantiate the new one to every client.
    public void SpawnMelody(string melody)
    {
        PhotonNetwork.Destroy(GameObject.FindWithTag("Melody"));
        switch (melody)
        {
            case "ForestSound":
                PhotonNetwork.Instantiate(melody, new Vector3(0f, 7.31f, 12f), Quaternion.identity, 0);
                break;
            case "ClassicalSound":
                PhotonNetwork.Instantiate(melody, new Vector3(0f, 20f, 17.18244f), Quaternion.identity, 0);
                break;
            default:
                print("Wrong melody string");
                break;
        }
    }

    // Set changingMap to true to every client
    [PunRPC]
    void changeMapFlag()
    {
        changingMap = true;
        Invoke("resetChangingMap", 5);
    }

    // After 5 seconds of having their vision blocked, unblock them by sending an RPC setting changingMap to false
    public void resetChangingMap()
    {
        if (PhotonNetwork.IsMasterClient == true)
        {
            photonView.RPC("resetchangeMapFlag", RpcTarget.All);
        }
    }

    // Set changingMap to false to every client
    [PunRPC]
    void resetchangeMapFlag()
    {
        changingMap = false;
    }



}
