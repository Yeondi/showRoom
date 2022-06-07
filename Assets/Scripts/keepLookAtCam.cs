using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keepLookAtCam : MonoBehaviour
{
    public GameObject Cam1;
    public GameObject Cam2;
    void Update()
    {
        try
        {
            transform.LookAt(Cam1.transform);
            transform.rotation *= Quaternion.Euler(0, 180, 0);
        }
        catch(System.NullReferenceException)
        {
            transform.LookAt(Cam2.transform);
        }
    }
}
