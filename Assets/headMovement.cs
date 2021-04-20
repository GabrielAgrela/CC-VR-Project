using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class headMovement : MonoBehaviour
{
    public PhotonView photonView;
    public Transform parent;
    public Rigidbody Rigid;
    public float MouseSensitivity;


    void Update()
    {
        if (photonView.IsMine)
        {
            //Debug.Log("cameraT Y: " + cameraT.eulerAngles.y + "transmormY: " + transform.eulerAngles.y + "mouse x" + Input.GetAxis("Mouse X"));

            //Rigid.MoveRotation(Rigid.rotation * Quaternion.Euler(new Vector3(Input.GetAxis("Mouse X") * MouseSensitivity, 0, Input.GetAxis("Mouse Y") * MouseSensitivity)));
            transform.localEulerAngles = new Vector3(parent.transform.eulerAngles.y, -parent.transform.eulerAngles.z, -parent.transform.eulerAngles.x);
            //transform.localRotation.localEulerAngles = new Vector3(transform.eulerAngles.x, cameraT.eulerAngles.y, transform.eulerAngles.z);
            //Rigid.MovePosition(transform.position + (transform.forward * Input.GetAxis("Vertical") * MoveSpeed) + (transform.right * Input.GetAxis("Horizontal") * MoveSpeed));
            //if (Input.GetKeyDown("space"))
            //Rigid.AddForce(transform.up * JumpForce);
        }
    }
}
