using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VRUIOperations : MonoBehaviour
{
    public UnityEvent OnEnter;
    public UnityEvent OnExit;

    private void OnTriggerEnter(Collider other)
    {
        TrigExit.instance.currentCollider = GetComponent<VRUIOperations>();
        Debug.Log("errrr");
        OnEnter.Invoke();
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if(ARAVRInput.GetDown(ARAVRInput.Button.One))
    //    {
    //        Debug.Log("거의 매프레임?");
    //        Invoke("onDesc", 3.0f);
    //    }
    //    OnStay.Invoke();
    //    Debug.Log("11111");
    //}

    private void OnTriggerExit(Collider other)
    {
        OnExit.Invoke();
    }
}
