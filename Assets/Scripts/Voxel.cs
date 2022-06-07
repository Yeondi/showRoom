using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Voxel : MonoBehaviour
{
    public float _velocity = 5f;
    public float _destroyTime = 3f;

    [SerializeField]
    float _currentTime;

    void Update()
    {
        _currentTime += Time.deltaTime;
        if (_currentTime > _destroyTime)
        {
            gameObject.SetActive(false);
            VoxelMaker.voxelPool.Add(gameObject);
            //Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        _currentTime = 0f;
        Vector3 direction = Random.insideUnitSphere;
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        rb.velocity = direction * _velocity;
    }
}
