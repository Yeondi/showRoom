//#define PC
#define Oculus
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class dragEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,IPointerDownHandler,IPointerUpHandler,IPointerMoveHandler ,IBeginDragHandler, IEndDragHandler
{
    Vector3 prevPos = Vector3.zero;

    Quaternion originRot;

    bool onMouseDrag = false;

    Transform originalTransform;
    public bool check = false;


    Vector3 pos;

    public float _power = 10.0f;

    public GameObject desc;

    Ray ray;
    RaycastHit hit;
    public LayerMask mask;

    public GameObject temp;



    private void Start()
    {
#if PC
        if(!onMouseDrag)
            originRot = transform.rotation;
#elif Oculus
        //if (!onMouseDrag)
            //originRot = transform.rotation;

        ray.origin = transform.position;
        ray.direction = transform.forward;
        Debug.Log(mask);

        _power *= 10;
#endif
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    check = true;
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    check = false;
    //}

    private void Update()
    {
        ray.origin = transform.position;
        ray.direction = transform.forward;
        if (check)
        {
            onRotate();
        }

        


    }

    public void onRotate()
    {
        Vector3 pointerPos = ARAVRInput.RHandDirection - prevPos;

        transform.Rotate(transform.up, -Vector3.Dot(pointerPos * _power, temp.transform.right), Space.World);
        transform.Rotate(temp.transform.right, Vector3.Dot(pointerPos * _power, temp.transform.up), Space.World);


        //좌우
        //transform.Rotate(0, -Vector3.Dot(pointerPos * _power, temp.transform.right), 0);
        //transform.Rotate(0, Vector3.Dot(pointerPos * _power, temp.transform.up), 0);

        //상하좌우
        //transform.Rotate(Vector3.up, -Vector3.Dot(pointerPos * _power, temp.transform.right),0);
        //transform.Rotate(temp.transform.right, Vector3.Dot(pointerPos * _power, Vector3.up),0);

        prevPos = ARAVRInput.RHandDirection;
        onMouseDrag = true;
    }
    public void offRotate()
    {
#if PC
        if (onMouseDrag)
            transform.rotation = originRot;
#endif
        check = false;
    }
    private void OnMouseDrag()
    {
        //Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);

        //this.transform.position = Camera.main.ScreenToWorldPoint(mousePos);

        Vector3 mousePos = Input.mousePosition - prevPos;
        transform.Rotate(transform.up, -Vector3.Dot(mousePos, Camera.main.transform.right), Space.World);
        transform.Rotate(Camera.main.transform.right, Vector3.Dot(mousePos, Camera.main.transform.up), Space.World);

        prevPos = Input.mousePosition;

        onMouseDrag = true;


    }

    private void OnMouseExit()
    {
#if PC
        if (onMouseDrag)
            transform.rotation = originRot;
#endif
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        desc.SetActive(true);
        GetComponent<Manual>().getSignal();
        //eventData.pointerCurrentRaycast.gameObject.GetComponent<Animator>().SetBool("isFloat", true);
        gameObject.GetComponent<Animator>().SetBool("isFloat", true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {

        //transform.rotation = originRot;
        //GetComponent<Manual>().initFunc = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        check = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        check = false;
        VRInteraction go = GameObject.FindGameObjectWithTag("VRInteraction").GetComponent<VRInteraction>();
        if (go.currentState != 2)
        {
            desc.SetActive(false);
        }
        GetComponent<Manual>().offSignal();

        GetComponent<Manual>().cantDrawning = true;
        GetComponent<Manual>().cantFloat = false;
        gameObject.GetComponent<Animator>().SetBool("isFloat", false);
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        //drag가 가능한지? 보류

    }


    public void OnBeginDrag(PointerEventData eventData)
    {

    }

    public void OnEndDrag(PointerEventData eventData)
    {

    }
}
