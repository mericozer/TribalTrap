﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canonball : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("AgroField"))
        {
            Destroy(gameObject);
        }
    }
}
