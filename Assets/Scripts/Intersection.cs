using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Intersection : MonoBehaviour
{
    public GameObject IntersectionCanvas;

    [Space]
    [Header("Objects")]
    public GameObject welcome;
    public GameObject selectTourMode;
    public GameObject selfTourMode_Guide;
    public GameObject AutoTourMode;
    public GameObject Exit_Guide;
    public GameObject Switch_Guide;
    public GameObject remote;
    public GameObject quickMenu;

    [Space]
    [Header("Free Mode")]
    public GameObject freeCamera;
    public GameObject temporaryPoint;
    public GameObject Canvas;

    [Space]
    [Header("Only Once")]
    public bool b_welcome = false;

    [Space]
    [Header("Raycast")]
    public RayCastForVR rayVR;
    public GameObject LaserPoint;



    private void OnTriggerEnter(Collider other)
    {
        if (!b_welcome && other.name == "Mod_OVRPlayerController")
        {
            welcome.SetActive(true);
            b_welcome = true;

            rayVR.enabled = true;
            LaserPoint.SetActive(true);
        }


    }

    public void clickFreeMode()
    {
        IntersectionCanvas.SetActive(false);
    }

    public void clickAutoPilot()
    {
        freeCamera.GetComponent<moveCamera>().enabled = true;
        temporaryPoint.SetActive(true);
        Canvas.SetActive(true);

        IntersectionCanvas.SetActive(false);


    }
}
