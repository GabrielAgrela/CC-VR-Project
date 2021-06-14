using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{
	public GameObject policePrefab;
	public GameObject cowBoyPrefab;
	public GameObject redWomanPrefab;
	public GameObject blueWomanPrefab;
	public static GameObject chosenPrefab;
	public GameObject EnterUsername;
	public GameObject Loading;
	
	public void Awake()
    {
		
	}
	
	// On button clicked, select a player prefab model
	public void selectPolice()
	{
		chosenPrefab = policePrefab;
		EnterUsername.SetActive(true);
	}
	public void selectCowBoy()
	{
		chosenPrefab = cowBoyPrefab;
		EnterUsername.SetActive(true);
	}
	public void selectRedWoman()
	{
		chosenPrefab = redWomanPrefab;
		EnterUsername.SetActive(true);
	}
	public void selectBlueWoman()
	{
		chosenPrefab = blueWomanPrefab;
		EnterUsername.SetActive(true);
	}

	// On unfocusing the text input, connect to the PhotonNetwork and set this user's nickname
	public void setUsername(string username)
	{
		PhotonNetwork.ConnectUsingSettings();
		PhotonNetwork.NickName = username;
		//if (connectedToMaster == true)
			//OnConnectedToMaster();
	}

	// Once connected join the room, if the room doesn't exist yet, create it as an admin/mod (technically called master client) and set it's properties.
	public override void OnConnectedToMaster()
	{
		Loading.SetActive(true);
		Debug.Log("JOINING LOBBY");
		PhotonNetwork.JoinLobby(TypedLobby.Default);
	}

	// Load scene on join
	public override void OnJoinedLobby()
	{
		Debug.Log("CONNECTED TO LOBBY");
		RoomOptions roomOptions = new RoomOptions();
		roomOptions.MaxPlayers = 5;
		if (chosenPrefab == policePrefab)
			PhotonNetwork.CreateRoom("UMA");
		else
			PhotonNetwork.JoinRoom("UMA");
		Debug.Log("JOINING ROOM");
	}

	// Load scene on join
	public override void OnJoinedRoom()
	{
		Debug.Log("JOINED ROOM");
		PhotonNetwork.LoadLevel("samplescene2");
	}
}
