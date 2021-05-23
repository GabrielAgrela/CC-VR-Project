using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerOnline : MonoBehaviour
{
    public PhotonView photonView;
    public GameObject PlayerCamera;
    public Transform cameraT;
    public Transform transformBox;

    public Rigidbody Rigid;
    public float MouseSensitivity;
    public float MoveSpeed = 2;
    public float JumpForce;

    // Active it's camera only if this GameObject is the client's
    void Awake()
    {
        if (photonView.IsMine)
        {
            PlayerCamera.SetActive(true);
            Debug.Log("MINE");
        }
    }

    // not sure
    void LateUpdate()
    {
        if (photonView.IsMine)
        {
            //Debug.Log("cameraT Y: " + cameraT.eulerAngles.y + "transmormY: " + transform.eulerAngles.y);
            
            //transform.Rotate(new Vector3(0, cameraT.eulerAngles.y, 0) * 1f);
            //transform.position = new Vector3(transformBox.position.x, transformBox.position.y, transformBox.position.z);
            Rigid.MovePosition(transform.position + (transform.forward * Input.GetAxis("Vertical") * MoveSpeed) + (transform.right * Input.GetAxis("Horizontal") * MoveSpeed));
            if (Input.GetKeyDown("space"))
                Rigid.AddForce(transform.up * JumpForce);
        }

    }

}
