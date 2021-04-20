using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class move : MonoBehaviour
{
    public Rigidbody Rigid;
    public PhotonView photonView;
    public MeshRenderer mr;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        if (photonView.IsMine)
        {
            mr.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            Rigid.MovePosition(transform.position + (transform.forward * Input.GetAxis("Vertical") * .05f) + (transform.right * Input.GetAxis("Horizontal") * .05f));
            if (Input.GetKeyDown(KeyCode.E))
                Rigid.MoveRotation(Rigid.rotation * Quaternion.Euler(new Vector3(0, transform.rotation.y + 45, 0)));
            if (Input.GetKeyDown(KeyCode.Q))
                Rigid.MoveRotation(Rigid.rotation * Quaternion.Euler(new Vector3(0, transform.rotation.y - 45, 0)));
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
