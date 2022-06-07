//#define Pick
#define Rotate
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RayCastForVR : OVRCursor, IBeginDragHandler, IEndDragHandler
{
    [HideInInspector]
    public LineRenderer line;
    public Material lineMaterial;
    public float lineDistance = 8.0f;

    Vector3 target;

    Ray ray;
    RaycastHit hit;
    RaycastHit saveLastHitInfo;

    public LayerMask mask;

    Grab grab;

    [HideInInspector]
    public Pickable pickedItem;
    void Start()
    {
        line = gameObject.AddComponent<LineRenderer>();
        line.widthMultiplier = 0.01f;
        line.useWorldSpace = false;
        line.material = lineMaterial;
        line.numCapVertices = 10;
        target = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + lineDistance);

        grab = gameObject.GetComponent<Grab>();
    }

    // Update is called once per frame
    void Update()
    {
        line.SetPosition(0, transform.localPosition);
        line.SetPosition(1, target);

        ray.origin = transform.position;
        ray.direction = transform.forward;

#if Pick
        if (ARAVRInput.GetDown(ARAVRInput.Button.HandTrigger))
        {
            if (Physics.Raycast(ray, out hit, lineDistance, mask))
            {
                if (pickedItem)
                {
                    grab.DropItem(pickedItem);
                }
                else
                {

                    var pickable = hit.transform.GetComponent<Pickable>();

                    if(pickable)
                        grab.PickItem(pickable);
                }
            }
        }
#elif Rotate
        if (ARAVRInput.GetDown(ARAVRInput.Button.HandTrigger) || ARAVRInput.GetDown(ARAVRInput.Button.IndexTrigger))
        {
            if (Physics.Raycast(ray, out hit, lineDistance, mask))
            {
                hit.transform.GetComponent<dragEvent>().check = true;
                saveLastHitInfo = hit;
            }
        }
        else if (ARAVRInput.GetUp(ARAVRInput.Button.HandTrigger) || ARAVRInput.GetUp(ARAVRInput.Button.IndexTrigger))
        {
            //if (Physics.Raycast(ray, out hit, lineDistance, mask))
            //{
            //    hit.transform.GetComponent<dragEvent>().check = false;
            //}
            try
            {
                saveLastHitInfo.transform.GetComponent<dragEvent>().check = false;
            }
            catch (System.NullReferenceException) { }
        }
#endif
    }

    public override void SetCursorRay(Transform ray)
    {

    }
    public override void SetCursorStartDest(Vector3 start, Vector3 dest, Vector3 normal)
    {

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (Physics.Raycast(ray, out hit, lineDistance, mask))
        {
            if (ARAVRInput.GetDown(ARAVRInput.Button.HandTrigger) || ARAVRInput.GetDown(ARAVRInput.Button.IndexTrigger))
            {
                //hit.transform.GetComponent<dragEvent>().check = true;
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (Physics.Raycast(ray, out hit, lineDistance, mask))
        {
            //hit.transform.GetComponent<dragEvent>().check = false;
        }
    }
}
