using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Compass : MonoBehaviour
{

    public Vector3 NorthDirection;
    public Transform player;
    public Quaternion MissionDirection;

    //public RectTransform NorthLayer;
    public RectTransform MissionLayer;

    public Transform missionPlace;
    
    // Update is called once per frame
    void Update()
    {
        ChangeNorthDirection();
        ChangeMissionDirection();
    }

    public void ChangeNorthDirection()
    {
        NorthDirection.z = player.eulerAngles.y;
        //NorthLayer.localEulerAngles = NorthDirection;

    }

    public void ChangeMissionDirection()
    {
        
        Vector3 dir = missionPlace.position - transform.position;

       

        MissionDirection = Quaternion.LookRotation(dir);

        MissionDirection.z = -MissionDirection.y;
        MissionDirection.x = 0;
        MissionDirection.y = 0;


        MissionLayer.localRotation = MissionDirection * Quaternion.Euler(NorthDirection);
    }
}
