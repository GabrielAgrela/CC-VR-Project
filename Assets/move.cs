using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class move : MonoBehaviour
{
    public Rigidbody Rigid;
    public PhotonView photonView;
    public MeshRenderer mr;
    public Transform cameraT;
    public Animator animator;
    public GameObject manager;
    public GameObject visionBlocker;
    public bool firstTime = true;
    public float speed = 0.1f;
    public int target = 60;
    public GameObject nickNameInputField;
    // If this script is running on the clients own player GameObject, unrender it's mesh, enable VR settings and set manager.
    void Start()
    {

        if (photonView.IsMine)
        {
            mr.enabled = false;
            UnityEngine.XR.InputTracking.disablePositionalTracking = true;
            manager = GameObject.FindWithTag("Manager");
            nickNameInputField = GameObject.FindWithTag("Canvas");
            if (PhotonNetwork.IsMasterClient == true)
            {
                Application.targetFrameRate = target;
                this.GetComponent<Rigidbody>().useGravity = false;
                this.GetComponent<CapsuleCollider>().enabled = false;
            }
        }
    }

    
    void LateUpdate()
    {
        if (photonView.IsMine)
        {
            //If moderator client then fps max 60
            if (PhotonNetwork.IsMasterClient == true)
            {
                if (Application.targetFrameRate != target)
                    Application.targetFrameRate = target;
            }
            
            // If the moderator is changing map and this client is not the moderator, block it's vision by activating the black block GameObject (visionBlocker)
            if (manager.GetComponent<GameManager2>().changingMap == true && PhotonNetwork.IsMasterClient == false)// 
            {
                visionBlocker.SetActive(true);
                if (firstTime)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y + 15f, transform.position.z);
                    firstTime = false;
                }
            }

            // If moderator is not changing map or the map changing protocol as ended, unblock it's view
            if (manager.GetComponent<GameManager2>().changingMap == false && PhotonNetwork.IsMasterClient == false) //
            {
                visionBlocker.SetActive(false);
                firstTime = true;
            }

            transform.Rotate(0.0f, cameraT.eulerAngles.y - transform.eulerAngles.y, 0.0f, Space.World); // rotate the GameObject matching the camera rotation (which is the headset's rotation), but only the Y axis.
            cameraT.position = new Vector3(transform.position.x, transform.position.y+1f, transform.position.z); // match the camera's position to the position of the player GameObject (and a unit up, since the camera should be head level).

            // Move or rotate to position depending on keys pressed
            if (PhotonNetwork.IsMasterClient == false || nickNameInputField.GetComponent<InputField>().isFocused == false )
            {
                Rigid.MovePosition(transform.position + (transform.forward * Input.GetAxis("Vertical") * .05f) + (transform.right * Input.GetAxis("Horizontal") * .05f));
                if (Input.GetKey(KeyCode.LeftShift))
                    transform.position = new Vector3(transform.position.x, transform.position.y + speed, transform.position.z);
                if (Input.GetKey(KeyCode.LeftControl))
                    transform.position = new Vector3(transform.position.x, transform.position.y - speed, transform.position.z);
                if (Input.GetKeyDown(KeyCode.E))
                    cameraT.Rotate(0.0f, 45.0f, 0.0f, Space.World);
                if (Input.GetKeyDown(KeyCode.Q))
                    cameraT.Rotate(0.0f, -45.0f, 0.0f, Space.World);
            }
                

            // On W pressed start/end running animation
            if (Input.GetKey(KeyCode.W))
                animator.SetBool("isRunning", true);  
            else
                animator.SetBool("isRunning", false);
                
        }   
    }
}
