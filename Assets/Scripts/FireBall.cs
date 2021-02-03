using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
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
        switch (other.tag)
        {
            
            case "Ship":
                CanvasController.instance.UpdateHealthBar("dec");
                
                break;
            case "AgroField":
               
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "AgroField")
            Destroy(gameObject);
    }
}
