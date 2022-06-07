//#define PC
#define Oculus
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    [SerializeField]
    private Camera characterCamera;

    [SerializeField]
    private Transform slot;

    public Pickable pickedItem;

    [SerializeField]
    LayerMask layerMask;

    
    void Update()
    {
#if PC
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            if(pickedItem)
            {
                DropItem(pickedItem);
            }
            else
            {
                var ray = characterCamera.ViewportPointToRay(Vector3.one * 0.5f);
                RaycastHit hit;
                if(Physics.Raycast(ray,out hit,4.5f,layerMask))
                {
                    Debug.Log(hit.collider.name);
                    var pickable = hit.transform.GetComponent<Pickable>();
                    if(pickable)
                        PickItem(pickable);
                }
            }
        }
#endif
    }

    public void PickItem(Pickable item)
    {
        pickedItem = item;
#if Oculus
        gameObject.GetComponent<RayCastForVR>().pickedItem = item;
#endif

        item.Rb.isKinematic = true;
        item.Rb.velocity = Vector3.zero;
        item.Rb.angularVelocity = Vector3.zero;

        item.transform.SetParent(slot);

        item.transform.localPosition = Vector3.zero;
        item.transform.localEulerAngles = Vector3.zero;


    }

    public void DropItem(Pickable item)
    {
#if PC
        pickedItem = null;
#elif Oculus
        gameObject.GetComponent<RayCastForVR>().pickedItem = null;
#endif

        item.transform.SetParent(null);
        item.Rb.isKinematic = false;

#if PC
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        int mask = LayerMask.GetMask("Interactable");
        Vector3 throwAngle;
        if (Physics.Raycast(ray, out hit, 1.5f, mask))
        {
            throwAngle = hit.point - slot.position;
        }
        else
            throwAngle = transform.forward * 7.5f;

        throwAngle.y = 4.5f;
        item.Rb.AddForce(throwAngle * 1.0f, ForceMode.Impulse);
        //item.Rb.AddForce(item.transform.forward * 7.5f, ForceMode.Impulse);
#endif
    }
}
