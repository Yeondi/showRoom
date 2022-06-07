using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnSignal : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject[] gameObjects;
    [Tooltip("������Ʈ�� ���� ��ȣ || ��ũ��Ʈ ����")]
    // ���̾��Ű���� ��ܺ��� ���ʴ�� ���� �Ǿ��.
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
            Debug.Log("�̰� �³�?");
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
        Debug.Log("�̰� �³�?2222222");
        //eventData.pointerEnter
        //eventData.pointerId
        //eventData.pointerCurrentRaycast
    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }
}
