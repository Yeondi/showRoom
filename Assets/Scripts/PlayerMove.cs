using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float velocity = 3.0f;
    public float rotSpeed = 200.0f;
    public GameObject cameraRig;
    public Transform myCharacter;
    public Animator anim;

    Rigidbody myRigid;

    void Start()
    {
        myRigid = GetComponent<Rigidbody>();

        if(gameObject.name == "OVRPlayerController")
        {
            if(cameraRig == null)
                cameraRig = gameObject;
            if(myCharacter == null)
                myCharacter = gameObject.transform;
            if(anim == null)
                anim = GetComponent<Animator>();
        }
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        float horizontal = ARAVRInput.GetAxis("Horizontal");
        float vertical = ARAVRInput.GetAxis("Vertical");

        bool isMove = new Vector2(horizontal, vertical).magnitude != 0;

        if (isMove)
        {
            Vector3 _moveHorizontal = transform.right * horizontal;
            Vector3 _moveVertical = transform.forward * vertical;

            Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * velocity;

            myRigid.MovePosition(transform.position + _velocity * Time.deltaTime);
        }
        float rotH = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.RTouch).x;

        cameraRig.transform.eulerAngles += new Vector3(0, rotH, 0) * rotSpeed * Time.deltaTime;
    }

    void Rotate()
    {
    }
}
