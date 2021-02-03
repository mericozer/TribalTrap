using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform ship;

    [SerializeField] private Vector3 offset;
    // Update is called once per frame
    private void Awake()
    {
        offset = new Vector3(ship.position.x - transform.position.x, 0, ship.position.z - transform.position.z);
    }

    void Update()
    {
        transform.position = new Vector3(ship.position.x, transform.position.y, ship.position.z) - offset;
    }
}
