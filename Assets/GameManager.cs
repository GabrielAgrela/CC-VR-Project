using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{
	public GameObject PlayerPrefab1;
	public GameObject PlayerPrefab2;
	public GameObject PlayerPrefab3;
	public GameObject PlayerPrefab4;
	public static GameObject PlayerPrefab;

	public void selectPolice()
	{
		PlayerPrefab = PlayerPrefab1;
	}
	public void selectCowBoy()
	{
		PlayerPrefab = PlayerPrefab2;
	}
	public void selectRedWoman()
	{
		PlayerPrefab = PlayerPrefab3;
	}
	public void selectBlueWoman()
	{
		PlayerPrefab = PlayerPrefab4;
	}

	public void setUsername(string username)
	{
		PhotonNetwork.ConnectUsingSettings();
		PhotonNetwork.NickName = username;
	}



	public override void OnConnectedToMaster()
	{
		PhotonNetwork.JoinLobby(TypedLobby.Default);
		Debug.Log("connected");
		RoomOptions roomOptions = new RoomOptions();
		roomOptions.MaxPlayers=5;
		PhotonNetwork.JoinOrCreateRoom("test123",roomOptions,TypedLobby.Default);
		Debug.Log("JOINING");
	}
	public override void OnJoinedRoom()
	{
		Debug.Log("JOINED");
		PhotonNetwork.LoadLevel("samplescene2");
	}

    // Update is called once per frame
    void Update()
    {

    }
}
