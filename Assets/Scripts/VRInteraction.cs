//#define Zoom
#define Zoom2
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VRInteraction : MonoBehaviour
{
    public GameObject welcome;
    public GameObject selectTourMode;
    public GameObject selfTourMode_Guide;
    public GameObject AutoTourMode;
    public GameObject Exit_Guide;
    public GameObject Switch_Guide;
    public GameObject remote;
    public GameObject quickMenu;

    [SerializeField]
    [Tooltip("0 is Self Tour / 1 is Auto Tour")]
    public int currentState = 99;

    [Space]
    [Header("Remote Controller")]
    public Animator animator;
    public GameObject remote_ZoomIn;
    public GameObject remote_ZoomOut;
    [Space]
    [Header("Quick Menu")]
    public GameObject btn_Home;
    public GameObject btn_Leave;
    public GameObject btn_Info;
    public GameObject btn_Switch;

    [Header("btn_Switch's child button")]
    public GameObject btn_Switch_child;


    [Space]
    [Header("Camera and Hands")]
    public GameObject cam;
    public GameObject rightHand;
    public GameObject laserPointer;

    [Space]
    [Header("FOR ZOOM IO")]
    public GameObject ZoomCam;
    public GameObject ZoomCam2;
    public GameObject CenterEye;

    [Space]
    [Header("Zoom Status")]
    public bool ZoomIn = false;

    // On click Function
    public void Welcome()
    {
        welcome.SetActive(false);
        selectTourMode.SetActive(true);

        cam.GetComponent<Animator>().enabled = false;
        // 레이저 안정화 용도 ( 테스트 )
        //rightHand.GetComponent<LineRenderer>().enabled = false;
        rightHand.GetComponent<LineRenderer>().enabled = true;
        laserPointer.GetComponent<LineRenderer>().enabled = true;



    }

    public void selectMode(int modeNumber) // 1 == selfTour , 2 == autoTour
    {
        // 모드 셀렉트
        if(modeNumber == 1)
        {
            selfTourMode_Guide.SetActive(true);


            if (cam.GetComponent<Rigidbody>().useGravity == false)
            {
                cam.GetComponent<Rigidbody>().useGravity = true;
                cam.GetComponent<CapsuleCollider>().isTrigger = false;
            }

            if(currentState == 2)
            {
                Switch_Guide.SetActive(false);
            }
            currentState = 1;
        }
        else if(modeNumber == 2)
        {
            AutoTourMode.SetActive(true);
            currentState = 2;
        }
        selectTourMode.SetActive(false);
        quickMenu.SetActive(true);
        btn_Switch.GetComponentInChildren<Button>().interactable = true;
    }

    public void selfTourGuide()
    {
        selfTourMode_Guide.SetActive(false);
        cam.GetComponent<Rigidbody>().isKinematic = false;
        GameObject.Find("Mod_OVRPlayerController").GetComponent<Animator>().enabled = false;


    }

    public void autoTour()
    {
        AutoTourMode.SetActive(false);
        remote.SetActive(true);
        cam.GetComponent<Animator>().enabled = true;
        cam.GetComponent<Rigidbody>().useGravity = false;
        cam.GetComponent<Rigidbody>().isKinematic = true;
        //cam.GetComponent<CapsuleCollider>().isTrigger = true;


        rightHand.GetComponent<LineRenderer>().enabled = true;
        laserPointer.GetComponent<LineRenderer>().enabled = false;
    }

    public void exitGuide(int selectNumber)
    {
        if (selectNumber == 1) // Leave
        {
            Application.Quit();
        }
        else
        {
            Exit_Guide.SetActive(false);
        }
    }

    public void switchGuide(int selectNumber)
    {
        if (selectNumber == 1) // Switch
        {
            //현재 상태 num가져와서
            if (currentState == 1)
            {
                selectMode(2);
                remote.SetActive(true);
            }
            else if (currentState == 2)
            {
                selectMode(1);
                remote.SetActive(false);
            }

            Switch_Guide.SetActive(false);

        }
        else
        {
            Switch_Guide.SetActive(false);
        }
    }

    public void remote_Moving(int dir)
    {
        //1 == left , 2 == right
        if (dir == 1)
        {
            if (ZoomIn) //줌인 상태일때 이동시 줌 아웃 후 이동
                remote_Zoom(2);

            if (cam.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.name == "Start"
                || cam.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.name == "end")
            {
                animator.enabled = true;
                animator.SetTrigger("go");
            }
            else
                animator.SetTrigger("back");
        }
        else if(dir==2)
        {
            if (cam.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.name == "Start"
                || cam.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.name == "end")
            {
                animator.enabled = true;
                animator.SetTrigger("back");
            }
            else
                animator.SetTrigger("go");
        }
    }

    public void remote_Zoom(int sig)
    {
        //1 == in, 2 == out
        if (sig == 1)
        {
            remote_ZoomIn.SetActive(true);
            remote_ZoomOut.SetActive(false);
#if Zoom
            ZoomCam.SetActive(true);
#elif Zoom2
            //ZoomCam2.SetActive(true);
            ZoomCam2.GetComponent<Zoom2>().pressZoom();
            ZoomCam2.GetComponent<Zoom2>().zoomOut = false;
            //CenterEye.SetActive(false);
#endif
            ZoomIn = true;
        }
        else if (sig == 2)
        {
            remote_ZoomIn.SetActive(false);
            remote_ZoomOut.SetActive(true);

#if Zoom
            ZoomCam.SetActive(false);
#elif Zoom2
            ZoomCam2.GetComponent<Zoom2>().zoom = !(ZoomCam2.GetComponent<Zoom2>().zoom);
            ZoomCam2.transform.localPosition = Vector3.zero;
            //ZoomCam2.SetActive(false);
            //CenterEye.SetActive(true);
#endif
            ZoomIn = false;
        }
    }

    ///
    ///
    //

    public void Quick_Home(bool sw)
    {
        Welcome();
        Debug.Log("return to home");
    }       

    public void Quick_Leave()
    {
        Exit_Guide.SetActive(true);
        //Application.Quit();
    }
    public void Quick_Info()
    {
        if (currentState == 1)
            selfTourMode_Guide.SetActive(true);
        else if (currentState == 2)
            AutoTourMode.SetActive(true);
    }

    public void Quick_Switch()
    {
        Switch_Guide.SetActive(true);
    }

}
