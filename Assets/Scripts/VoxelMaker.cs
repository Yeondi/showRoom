using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoxelMaker : MonoBehaviour
{
    public GameObject voxelFactory;
    public int objectPoolSize = 20;

    public static List<GameObject> voxelPool = new List<GameObject> ();

    public float createTime = 0.1f;
    float currentTime = 0f;


    public Transform crosshair;

    private void Start()
    {
        for(int i=0;i<objectPoolSize;i++)
        {
            GameObject voxel = Instantiate(voxelFactory);
            voxel.SetActive(false);
            voxelPool.Add(voxel);
        }
    }
    private void Update()
    {
        currentTime += Time.deltaTime;
        if(currentTime > createTime)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();
            if(Physics.Raycast(ray,out hit))
            {
                if(voxelPool.Count > 0)
                {
                    currentTime = 0;
                }
            }
        }
        if(Input.GetButtonDown("Fire1"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();
            if(Physics.Raycast(ray,out hit))
            {
                GameObject voxel = Instantiate(voxelFactory);
                voxel.transform.position = hit.point;
            }
        }

        if(ARAVRInput.Get(ARAVRInput.Button.One))
        {
            currentTime += Time.deltaTime;

            if(currentTime > createTime)
            {
                Ray ray = new Ray(ARAVRInput.RHandPosition, ARAVRInput.RHandDirection);
                RaycastHit hit = new RaycastHit();
                if (Physics.Raycast(ray, out hit))
                {
                    GameObject voxel = Instantiate(voxelFactory);
                    voxel.transform.position = hit.point;
                }
            }
        }



        ARAVRInput.drawRay(crosshair);
    }
}
