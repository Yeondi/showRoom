using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceContactor : MonoBehaviour
{
    [Space]
    [Header("Description For some Places")]
    public GameObject firstDesc;
    public GameObject secondDesc;
    public GameObject thirdDesc;
    public GameObject fourthDesc;

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.tag == "firstPlace" && other.name == "Mod_OVRPlayerController")
            firstDesc.SetActive(true);
        else if (gameObject.tag == "secondPlace" && other.name == "Mod_OVRPlayerController")
            secondDesc.SetActive(true);
        else if (gameObject.tag == "thirdPlace" && other.name == "Mod_OVRPlayerController")
            thirdDesc.SetActive(true);
        else if (gameObject.tag == "fourthPlace" && other.name == "Mod_OVRPlayerController")
        {
            GameObject[] gos = GameObject.FindGameObjectsWithTag("zoom");
            foreach(GameObject go in gos)
            {
                go.GetComponent<Button>().interactable = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (firstDesc.activeInHierarchy == true)
            firstDesc.SetActive(false);
        else if (secondDesc.activeInHierarchy == true)
            secondDesc.SetActive(false);
        else if (thirdDesc.activeInHierarchy == true)
            thirdDesc.SetActive(false);
        else if (fourthDesc.activeInHierarchy == true)
            fourthDesc.SetActive(false);
    }
}
