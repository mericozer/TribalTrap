using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diving : MonoBehaviour
{
    public static Diving instance;

    Vector3 down = new Vector3(0, -1, 0);
    private float _speed = 10;
    public bool isDiving = false;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("y position " + transform.position.y);
        if (isDiving)
        {
            transform.position += down.normalized * Time.deltaTime * _speed;
        }
        if (transform.position.y <= -20)
        {
            
            Destroy(gameObject);
        }
    }

    public void setIsDiving(bool b)
    {
        isDiving = b;
    }
}
