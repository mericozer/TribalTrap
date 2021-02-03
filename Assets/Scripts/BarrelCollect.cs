﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelCollect : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ship")
        {
            CanvasController.instance.UpdateBarrelSlider("press", gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ship")
        {
            CanvasController.instance.UpdateBarrelSlider("stop", gameObject);
        }
    }
}
