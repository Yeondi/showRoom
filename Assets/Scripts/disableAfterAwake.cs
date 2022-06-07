using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class disableAfterAwake : MonoBehaviour
{
    public GameObject selectTourMode;
    public GameObject selfTourModeGuide;
    public GameObject autoTourMode;
    public GameObject ExitGuide;
    public GameObject switchModeGuide;

    [Space]
    public GameObject Home;
    public GameObject Leave;
    public GameObject Info;
    public GameObject SwitchMode;
    private void Awake()
    {
        if (gameObject.name == "selectTourMode")
            Home.GetComponent<Button>().interactable = false;
        else if (gameObject.name == "Exit_Guide")
            Leave.GetComponent<Button>().interactable = false;
        else if (gameObject.name == "selfTourMode_Guide" || gameObject.name == "AutoTourMode")
            Info.GetComponent<Button>().interactable = false;
        else if (gameObject.name == "Switch_Guide")
            SwitchMode.GetComponent<Button>().interactable = false;

    }
    private void OnDisable()
    {
        if (gameObject.name == "selectTourMode")
            Home.GetComponent<Button>().interactable = true;
        else if (gameObject.name == "Exit_Guide")
            Leave.GetComponent<Button>().interactable = true;
        else if (gameObject.name == "selfTourMode_Guide" || gameObject.name == "AutoTourMode")
            Info.GetComponent<Button>().interactable = true;
        else if (gameObject.name == "Switch_Guide")
            SwitchMode.GetComponent<Button>().interactable = true;
    }
}
