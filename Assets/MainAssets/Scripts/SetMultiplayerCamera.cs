using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SetMultiplayerCamera : MonoBehaviour
{
    public PhotonView photonView;
    public GameObject PlayerCamera;

    // Active it's camera only if this GameObject is the client's
    void Awake()
    {
        if (photonView.IsMine)
        {
            PlayerCamera.SetActive(true);
        }
    }
}
