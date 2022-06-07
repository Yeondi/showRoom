using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("이동관련")]
    [SerializeField]
    private float moveVelocity = 5.0f;
    [SerializeField]
    private float _jumpSpeed = 50.0f;
    [SerializeField]
    private float lookSensitivity = 18;
    [SerializeField]
    private float lookSensitivity_X = 2.2f;

    [Space]
    [Header("시선처리")]
    [SerializeField]
    private float cameraRotationLimit = 90f;
    private float currentCameraRotationX = 0;


    [SerializeField]
    private Camera cam;

    private Rigidbody myRigid;

    [Space]
    [SerializeField]
    public int jumpCount = 1;

    GameObject playerVirtualHand;
    bool isPicking = false;

    bool isGrounded = true;


    public bool mouseHold = false;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        myRigid = GetComponent<Rigidbody>();
    }


    public void PickUp(GameObject item)
    {
        SetEquip(item, true);

        isPicking = true;
    }

    void SetEquip(GameObject item,bool isEquip)
    {
        Collider[] itemColliders = item.GetComponents<Collider>();
        Rigidbody itemRigidbody = item.GetComponent<Rigidbody>();
        foreach(Collider itemCollider in itemColliders)
            itemCollider.enabled = !isEquip;

        itemRigidbody.isKinematic = isEquip;
    }
    void Update()
    {
        if (!mouseHold)
        {
            Move();
            CameraRotation();
            CharacterRotation();
        }
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount > 0)
            Jump();
        //raycastHit();

        Vector3 rayOrigin = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        Vector3 rayDir = cam.transform.forward;
        Debug.DrawRay(rayOrigin, rayDir * 2.5f, Color.red);
    }

    private void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }

    private void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        bool isMove = new Vector2(horizontal, vertical).magnitude != 0;

        if (isMove)
        {
            Vector3 _moveHorizontal = transform.right * horizontal;
            Vector3 _moveVertical = transform.forward * vertical;

            Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * moveVelocity;

            myRigid.MovePosition(transform.position + _velocity * Time.deltaTime);
        }
        //Vector3 move = transform.right * horizontal + transform.forward * vertical;
        //myRigid.MovePosition(move * moveVelocity * Time.deltaTime);
    }

    private void Jump()
    {
        jumpCount--;
        myRigid.AddForce(new Vector3(0, 2.0f, 0) * _jumpSpeed,ForceMode.Impulse);
        isGrounded = false;
    }


    private void CharacterRotation()
    {
        // 좌우 캐릭터 회전
        float _yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 _characterRotationY = new Vector3(0f, _yRotation, 0f) * lookSensitivity * Time.deltaTime;
        myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(_characterRotationY));
    }

    private void CameraRotation()
    {
        // 상하 카메라 회전
        float _xRotation = Input.GetAxisRaw("Mouse Y");
        float _cameraRotationX = _xRotation * (lookSensitivity * lookSensitivity_X) * Time.deltaTime;
        currentCameraRotationX -= _cameraRotationX;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

        cam.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "ball")
        {
            Vector3 vDir = transform.position - collision.gameObject.transform.position;

            vDir = vDir.normalized * 1000f;
            Debug.Log(vDir);

            collision.gameObject.GetComponent<Rigidbody>().AddForce(vDir);
        }

        if(collision.collider.name == "GALLERY FLOOR")
        {

        }
    }



    //private void raycastHit()
    //{
    //    Vector3 rayOrigin = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
    //    Vector3 rayDir = cam.transform.forward;

    //    Debug.DrawRay(rayOrigin, rayDir * distanceOfRaycast, Color.red);

    //    RaycastHit hit;

    //    if (Input.GetKeyDown(KeyCode.Mouse0))
    //    {
    //        if (heldObejct == null)
    //        {
    //            if (Physics.Raycast(rayOrigin, rayDir, out hit, distanceOfRaycast, layerMask))
    //            {
    //                GameObject rayHit = hit.collider.gameObject;
    //                Debug.Log("hit");

    //                moveTarget = rayHit.transform;
    //                targetDistance = hit.distance;

    //                //moveTarget.transform.position += new Vector3()
    //            }
    //        }
    //    }
    //}


}
