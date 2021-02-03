using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Discover : MonoBehaviour
{
   

    

    private void Awake()
    {
        
    }
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
            if (gameObject.tag == "fishing")
            {
                CanvasController.instance.OpenDiscoverDialogs("fish");
                
                //canvas
            }
            else if (gameObject.tag == "barrel")
            {
                CanvasController.instance.OpenDiscoverDialogs("barrel");
            }
            else if (gameObject.tag == "buoy")
            {
                CanvasController.instance.OpenDiscoverDialogs("buoy");
                
            }
            else if (gameObject.tag == "chest")
            {
                CanvasController.instance.OpenDiscoverDialogs("chest");
            }
        }
    }
}
