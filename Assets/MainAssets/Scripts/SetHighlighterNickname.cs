using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class SetHighlighterNickname : MonoBehaviour
{
    public Text usernameTxt;
    public PhotonView photonView;

    // set usernames above player's GameObjects heads
    void Awake()
    {
        if (photonView.IsMine)
            usernameTxt.text = PhotonNetwork.LocalPlayer.NickName;
        else
            usernameTxt.text = photonView.Owner.NickName;
    }
}
