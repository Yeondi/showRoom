using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class moveCamera : MonoBehaviour
{
    GameObject btnLeft;
    GameObject btnRight;

    [Space]
    [SerializeField]
    bool isFirstPlace = false;
    bool isFourthPlace = false;

    [SerializeField]
    Animator anim;

    public Button btnGo; //right
    public Button btnBack; //left
    public Button btnExit;

    bool once = true;

    GameObject btn4Guide;
    GameObject exitShowRoom;

    GameObject panel;
    GameObject title;
    GameObject gdHome;
    GameObject email;
    GameObject svg;

    void Start()
    {
        btnLeft = GameObject.FindWithTag("left");
        btnRight = GameObject.FindWithTag("right");
        btn4Guide = GameObject.FindWithTag("btn4Guide");
        exitShowRoom = GameObject.FindWithTag("exit");

        btnGo.onClick.AddListener(delegate { btnPressed(2); });
        btnBack.onClick.AddListener(delegate { btnPressed(1); });
        btnExit.onClick.AddListener(delegate { btnPressed(3); });
    }

    void Update()
    {
        if(isFirstPlace)
        {
            isFirstPlace = false;
            btnRight.GetComponent<UnityEngine.UI.Button>().interactable = true;
            btn4Guide.GetComponent<UnityEngine.UI.Image>().enabled = true;
            btn4Guide.GetComponentInChildren<UnityEngine.UI.Text>().enabled = true;
        }
        else if(isFourthPlace)
        {
            isFourthPlace = false;
            btn4Guide.GetComponent<UnityEngine.UI.Image>().enabled = true;
            GameObject temp = btn4Guide.transform.Find("end").gameObject;
            if(btn4Guide.transform.Find("end"))
            {
                temp.GetComponent<UnityEngine.UI.Text>().enabled = true;
                Invoke("endShowRoom", 2.0f);
                
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "startPoint")
        {
            btnLeft.GetComponent<UnityEngine.UI.Image>().enabled = true;
            btnRight.GetComponent<UnityEngine.UI.Image>().enabled = true;

        }
        else if (other.gameObject.name == "firstPlace")
        {
            switchType();
            isFirstPlace = true;
        }
        else if(other.gameObject.name == "fourthPlace")
        {
            isFourthPlace = true;
        }
    }

    public void btnPressed(int type) // 1 = left , 2 = right
    {
        if (type == 1)
        {
            anim.SetTrigger("go");
        }
        else if (type == 2)
        {
            anim.SetTrigger("back");
        }
        else if(type == 3)
        {
            anim.SetTrigger("end");
        }
    }

    void switchType()
    {
        if(once)
        {
            once = false;
            btnGo.onClick.RemoveAllListeners();
            btnBack.onClick.RemoveAllListeners();

            btnGo.onClick.AddListener(delegate { btnPressed(1); });
            btnBack.onClick.AddListener(delegate { btnPressed(2); });
        }
    }

    void endShowRoom()
    {
        btn4Guide.transform.Find("end").GetComponent<UnityEngine.UI.Text>().enabled = false;
        btn4Guide.GetComponent<UnityEngine.UI.Image>().enabled = false;
        btn4Guide.GetComponentInChildren<UnityEngine.UI.Text>().enabled = false;

        exitShowRoom.GetComponent<UnityEngine.UI.Image>().enabled = true;
        exitShowRoom.GetComponentInChildren<UnityEngine.UI.Text>().enabled = true;
    }

    public void back2Home()
    {
        //Application.Quit();
        anim.SetTrigger("go");
        panel.GetComponent<UnityEngine.UI.Image>().enabled = false;
        title.GetComponent<UnityEngine.UI.Text>().enabled = false;
        email.GetComponent<UnityEngine.UI.Text>().enabled = false;
        gdHome.GetComponent<UnityEngine.UI.Text>().enabled = false;
        svg.GetComponent<Unity.VectorGraphics.SVGImage>().enabled = false;
    }

    public void showPanel()
    {
        exitShowRoom.GetComponent<UnityEngine.UI.Image>().enabled = false;
        exitShowRoom.GetComponentInChildren<UnityEngine.UI.Text>().enabled = false;

        panel = GameObject.FindGameObjectWithTag("panel");
        panel.GetComponent<UnityEngine.UI.Image>().enabled = true;

        title = panel.transform.GetChild(0).gameObject;
        title.GetComponent<UnityEngine.UI.Text>().enabled = true;

        email = panel.transform.GetChild(1).gameObject;
        email.GetComponent<UnityEngine.UI.Text>().enabled = true;

        gdHome = panel.transform.GetChild(2).gameObject;
        gdHome.GetComponent<UnityEngine.UI.Text>().enabled = true;

        svg = panel.transform.GetChild(3).gameObject;
        svg.GetComponent<Unity.VectorGraphics.SVGImage>().enabled = true;
    }
}
