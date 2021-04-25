using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class move : MonoBehaviour
{
    public Rigidbody Rigid;
    public PhotonView photonView;
    public MeshRenderer mr;
    public Transform cameraT;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        if (photonView.IsMine)
        {
            mr.enabled = false;
            UnityEngine.XR.InputTracking.disablePositionalTracking = true;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (photonView.IsMine)
        {
            
            transform.Rotate(0.0f, cameraT.eulerAngles.y - transform.eulerAngles.y, 0.0f, Space.World);
            cameraT.position = new Vector3(transform.position.x, transform.position.y+1f, transform.position.z);
            Rigid.MovePosition(transform.position + (transform.forward * Input.GetAxis("Vertical") * .05f) + (transform.right * Input.GetAxis("Horizontal") * .05f));
            if (Input.GetKeyDown(KeyCode.E))
                cameraT.Rotate(0.0f, 45.0f, 0.0f, Space.World);
            if (Input.GetKeyDown(KeyCode.Q))
                cameraT.Rotate(0.0f, -45.0f, 0.0f, Space.World);
            if (Input.GetKey(KeyCode.W))
            {
                animator.SetBool("isRunning", true);
            }
                
            else
            {
                animator.SetBool("isRunning", false);
            }
                
        }   
    }
}
