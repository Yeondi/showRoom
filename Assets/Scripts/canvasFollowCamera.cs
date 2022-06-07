using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canvasFollowCamera : MonoBehaviour
{
    public GameObject centerEye;

    void Update()
    {
        gameObject.transform.position = new Vector3(centerEye.transform.position.x, centerEye.transform.position.y - 0.8f, centerEye.transform.position.z - 1.5f);
        //gameObject.transform.position = new Vector3(centerEye.transform.position.x, centerEye.transform.position.y, centerEye.transform.forward.z);

    }
}
