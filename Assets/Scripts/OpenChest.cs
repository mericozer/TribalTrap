using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChest : MonoBehaviour
{
    int collectableTotems;
    bool isCollected = false;
    public static OpenChest instance;

    [SerializeField]
    private List<ParticleSystem> goldParticles = new List<ParticleSystem>();
    
    private Animator chestCap;
    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;

    }
    void Start()
    {
        collectableTotems = 3;
        chestCap = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (collectableTotems == 0)
        {
            isCollected = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ship")
        {
            if (isCollected)
            {
                CanvasController.instance.ChestIntereaction("open", gameObject, collectableTotems);
            }
            else { 
            CanvasController.instance.ChestIntereaction("locked", gameObject, collectableTotems);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ship")
        {
            CanvasController.instance.ChestIntereaction("out", gameObject, collectableTotems);
        }
    }

    public void AddTotem()
    {

        collectableTotems--;
        Debug.Log(collectableTotems);
    }

    public void StopParticles()
    {
        foreach (ParticleSystem system in goldParticles)
        {
            system.Stop();
        }
    }

    public void OpenChestCap()
    {
        chestCap.SetBool("complete", true);
    }

}
