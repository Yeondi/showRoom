using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom2 : MonoBehaviour
{
    //[SerializeField]
    //float _Velocity = 3.0f;

    public bool zoom = false;
    public bool zoomOut = false;

    public GameObject OVRController;

    public GameObject CenterEye;
    public Vector3 OVRControllerPos;

    float currentPosZ;
    float destPosZ;


    public float distance = 3;
    public float SmoothTime = 0.5f;
    private Vector3 velocity = Vector3.zero;
    private float __velocity = 1f;

    private float target;
    private float targetOff;

    // 1. CenterEye�� ���� Zoom���� FOV���� ������ �ȵǴ°�?
    // 2. ZoomCam�� �������ѳ��� OVRController�� position�� rotation�� keep follow�س��� �� �ʿ�ÿ��� zoom�� �Ǵ°�? ���ٵ�

    private void Start()
    {
        if (gameObject.name == "Mod_OVRPlayerController")
        {
            OVRController = gameObject;
        }
    }
    public void pressZoom()
    {
        zoom = !zoom;

        target = transform.localPosition.z + 1f; 
        targetOff = transform.localPosition.z - 1f;
    }

    void Update()
    {
        if (zoom)
        {
            ZoomIn();
        }
        else
        {
            target = transform.localPosition.z + 1f;
            targetOff = transform.localPosition.z - 1f;
        }

        if (zoomOut)
        {
            transform.localPosition = Vector3.zero;
        }
    }

    void ZoomIn()
    {
        // �� Ȯ���Ұ�
        float newPosition = Mathf.SmoothDamp(transform.localPosition.z, target, ref __velocity, SmoothTime);
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, newPosition);

    }

    void ZoomOut()
    {
        float newPosition = Mathf.SmoothDamp(transform.position.z, target, ref __velocity, SmoothTime);
        transform.position = new Vector3(transform.position.x, transform.position.y, newPosition);
    }

    public void setTarget(float temp)
    {
        target = temp;
    }

    public void setTargetOff(float temp)
    {
        targetOff = temp;
    }
}
