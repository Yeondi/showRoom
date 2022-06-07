using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manual : MonoBehaviour
{
    public bool initFunc = false;

    [SerializeField]
    Vector3 targetPos = Vector3.zero;

    public bool cantFloat = false;

    public bool cantDrawning = false;

    Vector3 originPos;

    void Start()
    {
        targetPos = new Vector3(transform.localPosition.x, transform.localPosition.y + 0.25f, transform.localPosition.z);
        originPos = transform.localPosition;
    }

    void Update()
    {
        if (initFunc)
        {
            init();
        }
    }

    public void getSignal()
    {
        if (!cantFloat)
        {
            Debug.Log("onTrigger by Manual");
            gameObject.GetComponent<Manual>().enabled = true;
            gameObject.GetComponent<Outline>().enabled = true;

            targetPos = new Vector3(transform.localPosition.x, transform.localPosition.y + 0.1f, transform.localPosition.z);
            initFunc = true;

            cantFloat = true;
            cantDrawning = false;
        }
    }

    public void offSignal()
    {
        if (!cantDrawning)
        {
            //targetPos = new Vector3(transform.localPosition.x, transform.localPosition.y - 0.1f, transform.localPosition.z);
            //Vector3 speed = Vector3.zero;
            //transform.localPosition = Vector3.SmoothDamp(transform.localPosition, targetPos, ref speed, 0.22f);

            transform.localPosition = originPos;

         
        }
    }
    void init()
    {
        if (gameObject.transform.localPosition.y > targetPos.y - 0.001)
            initFunc = false;

        //gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPos, 0.001f);

        Vector3 speed = Vector3.zero;

        transform.localPosition = Vector3.SmoothDamp(transform.localPosition, targetPos, ref speed, 0.22f);
    }
}
