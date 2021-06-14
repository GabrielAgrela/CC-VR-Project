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
	}

	// Once connected join the room, if the room doesn't exist yet, create it as an admin/mod (technically called master client) and set it's properties.
	public override void OnConnectedToMaster()
	{
		Loading.SetActive(true);
		PhotonNetwork.JoinLobby(TypedLobby.Default);
		Debug.Log("connected");
		RoomOptions roomOptions = new RoomOptions();
		roomOptions.MaxPlayers=5;
		PhotonNetwork.JoinOrCreateRoom("test123",roomOptions,TypedLobby.Default);
		Debug.Log("JOINING");
	}

	// Load scene on join
	public override void OnJoinedRoom()
	{
		Debug.Log("JOINED");
		PhotonNetwork.LoadLevel("samplescene2");
	}
}
