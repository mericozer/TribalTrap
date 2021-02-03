using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public int monsterNum;
    public Animator _stakesAnimator;
    public Animator _buoyAnimator;
    public AudioManager audioManager;
    public bool isThereRedGiants, isThereChest, isThereStakes, isThereBuoys, lastLevel;
    
    public List<Animator> _seaAnimator = new List<Animator>();

    /*[SerializeField]
    GameObject fishPoint;
    [SerializeField]
    List<GameObject> fishspawnPoints;

    [SerializeField]
    GameObject barrel;
    [SerializeField]
    List<GameObject> barrelspawnPoints;

    GameObject currentPoint;
    GameObject currentBarrelPoint;
    List<int> takenFishPoints;
    List<int> takenBarrelPoints;
    int indexFish;
    int indexBarrel;*/

    [SerializeField]
    GameObject chest;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        //takenFishPoints = new List<int>();
        //takenBarrelPoints = new List<int>();
        chest.SetActive(false);
        Debug.Log("health =" + PlayerPrefs.GetFloat("Life"));
        CanvasController.instance.UpdateHealthBar("start");
        CanvasController.instance.CalcHealth();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateMonsterNumber()
    {
        monsterNum--;
        if(monsterNum == 0)
        {
            if (lastLevel)
            {
                
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                Debug.Log("number of enemies: " + enemies.Length);
                foreach (GameObject enemy in enemies) {

                    Destroy(enemy);
                }

                   
            }
            if (isThereRedGiants)
            { Diving.instance.setIsDiving(true); }
            if (isThereChest)
            { chest.SetActive(true); }
            if (isThereStakes)
            { _stakesAnimator.SetBool("complete", true); }
            if (isThereBuoys)
            { _buoyAnimator.SetBool("complete", true); }


            foreach (Animator anim in _seaAnimator)
            {
                anim.SetBool("complete", true);
            }
            //SpawnFishPoints();
            //SpawnBarrelPoints();
            audioManager.Switch("TribalTheme", "CalmTheme");
            
        }
    }

   


    ////Codes for cancelled idea

    /*private void SpawnFishPoints()
    {
        
        for (int i = 0; i < 3; i++)
        {
            indexFish = Random.Range(0, fishspawnPoints.Count);
            if (takenFishPoints.Any())
            {
                while (takenFishPoints.Contains(indexFish))
                {
                    indexFish = Random.Range(0, fishspawnPoints.Count);
                }
            }
            takenFishPoints.Add(indexFish);
            currentPoint = fishspawnPoints[indexFish];
            currentPoint = Instantiate(fishPoint, currentPoint.transform.position, Quaternion.identity);
        }
        


    }

    private void SpawnBarrelPoints()
    {

        for (int i = 0; i < 3; i++)
        {
            indexBarrel = Random.Range(0, barrelspawnPoints.Count);
            if (takenBarrelPoints.Any())
            {
                while (takenBarrelPoints.Contains(indexBarrel))
                {
                    indexBarrel = Random.Range(0, barrelspawnPoints.Count);
                }
            }
            takenBarrelPoints.Add(indexBarrel);
            currentBarrelPoint = barrelspawnPoints[indexBarrel];
            currentBarrelPoint = Instantiate(barrel, currentBarrelPoint.transform.position, Quaternion.Euler(0,0,75));
        }



    }*/
}
