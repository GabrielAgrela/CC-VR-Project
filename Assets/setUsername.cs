using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class setUsername : MonoBehaviour
{
    public Text usernameTxt;
    public PhotonView photonView;
    // Start is called before the first frame update
    void Awake()
    {
        if (photonView.IsMine)
            usernameTxt.text = PhotonNetwork.LocalPlayer.NickName;
        else
            usernameTxt.text = photonView.Owner.NickName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
