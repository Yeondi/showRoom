using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnSignal : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject[] gameObjects;
    [Tooltip("오브젝트별 고유 번호 || 스크립트 참조")]
    // 하이어라키기준 상단부터 차례대로 정렬 되어들어감.
    // CERAGEM_CHAIR -> FURNITURES_ON_CUSHION - CERAGEM_CHAIR_02 -> ...
    public int numberOfGameobjects;
    void Start()
    {
        gameObjects = GameObject.FindGameObjectsWithTag("InteractableFurnitures");
    }

    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log("이게 맞나?");
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            gameObjects[numberOfGameobjects].GetComponent<Manual>().getSignal();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("이게 맞나?2222222");
        //eventData.pointerEnter
        //eventData.pointerId
        //eventData.pointerCurrentRaycast
    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }
}
