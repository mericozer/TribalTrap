using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FallingFireBall : MonoBehaviour
{
    private Rigidbody rb;
    public float speed = 100;
    Vector3 down = new Vector3(0, -1, 0);
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        rb.AddForce(down * speed, ForceMode.Acceleration);

        if (gameObject.transform.position.y <= -5)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Ship")
        {
            CanvasController.instance.UpdateHealthBar("dec");
            StartCoroutine(LateDestroy());
        }
    }

    IEnumerator LateDestroy()
    {
       
        
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
       
    }
}
