using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MonsterWanderer : MonoBehaviour
{
    private float timeToChangeDirection;
    private GameObject target;
    private Animator _anim;
    public float rotateSpeed;
    private float moveSpeed = 15f;
    private Vector3 down = new Vector3(0, -1, 0);
    private Vector3 up = new Vector3(0, 1, 0);

    private GameObject[] wanderPoints;
    private int wanderPointCount;
    int nextWanderPoint,currentWanderPoint = -1;
    private bool isTurned = false, move = false, isUnderWater = false ;
    // Use this for initialization
    public void Start()
    {
        wanderPoints = GameObject.FindGameObjectsWithTag("WanderPoint");
        wanderPointCount = wanderPoints.Length;
        _anim = GetComponent<Animator>();
        //SelectDirection();
        
    }

    // Update is called once per frame
    public void Update()
    {
        if (isTurned)
        {
            TurnToDirection();
            Debug.Log("testUpdateturned");
        }
        else if (move)
        {
            MoveToDirection();
            Debug.Log("testUpdatemove");
        }
        else if (!isUnderWater)
        {
            SelectDirection();
        }
        
    }



    private void TurnToDirection()
    {
        /* float angle = Random.Range(0f, 360f);
         Quaternion quat = Quaternion.AngleAxis(angle, Vector3.forward);
         Vector3 newUp = quat * Vector3.up;
         newUp.z = 0;
         newUp.Normalize();
         transform.up = newUp;
         timeToChangeDirection = 5f;*/
       
        var q = Quaternion.LookRotation(target.transform.position - transform.position);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, q, rotateSpeed * Time.deltaTime);
        if (q == transform.rotation)
        {
            isTurned = false;
            move = true;
            
        }
    }

    private void SelectDirection()
    {
        nextWanderPoint = Random.Range(0, wanderPointCount);
        if (currentWanderPoint == -1)
        {
            
            currentWanderPoint = nextWanderPoint;
        }
        else
        {
            while (nextWanderPoint == currentWanderPoint)
            {
                nextWanderPoint = Random.Range(0, wanderPointCount);
            }
            currentWanderPoint = nextWanderPoint;
        }
        target = wanderPoints[nextWanderPoint];
        isTurned = true;

    }

    private void MoveToDirection()
    {
        transform.position += (target.transform.position - transform.position).normalized * Time.deltaTime * moveSpeed;
        
       
        
    }

    private void DiveAndEmerge()
    {
        
        if (transform.position.y <= -20)
        {
            transform.position += up.normalized * Time.deltaTime * moveSpeed;

        }
        else { 
        transform.position += down.normalized * Time.deltaTime * moveSpeed;
        }

        if (transform.position.y >= -9)
        { isUnderWater = false; }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger");
        switch (other.tag)
        {
            case "Ship":
                CanvasController.instance.UpdateHealthBar("dec");
                CanvasController.instance.UpdateHealthBar("dec");
                break;
            case "WanderPoint":
                move = false;
                break;
            case "Monster":
                move = false;
                break;
            case "Cannon":
                Debug.Log("hitTheRed");
                move = false;
                isUnderWater = true;
                _anim.Play("diveToWaterAnim");
                isUnderWater = false;
               
                break;
           

        }
    }


   



}
