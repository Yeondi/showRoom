using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class updateRay : MonoBehaviour
{
    public Transform rayOrigin;
    public OVRInputModule ovr;

    public bool stop = true;

    private void Start()
    {
        ovr = GetComponent<OVRInputModule>();
    }

    private void FixedUpdate()
    {
        if(stop)
        {
            if(ovr.transform.name != "CustomHandRight")
            {
                ovr.rayTransform = rayOrigin;
                stop = true;
            }
        }
    }
}
