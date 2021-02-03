using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WitchBehaviour : MonoBehaviour
{
    public Transform ship;
    private static float _rotationSpeed = 20;
    private float healthAmount = 100;
    public ParticleSystem deathParticle;
    private bool startFight = false;
    public GameObject fallingBall,witchStats;
    public Vector3 upper = new Vector3(0, 38, 0);
    public float shortTimer = 1f, longTimer = 20f;
    public Animator skullAnimator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (!CanvasController.instance.isGameRunning) return;
       
            Vector3 targetDir = ship.position - transform.position;
            //targetDir.y = 0; // kill any height difference to avoid tilting
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetDir), _rotationSpeed);

        if (startFight)
        {
            if (longTimer > 0)
            {
                longTimer -= Time.deltaTime;
            }
            else
            {
                FindObjectOfType<AudioManager>().Play("FireBallRain");
                skullAnimator.Play("WandSkullAnim");
                StartCoroutine(Fiver());
                longTimer = 10f;
            }
            
        }


            
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Cannon")
        {
            healthAmount -= 10;
            CanvasController.instance.UpdateWitchHealth(healthAmount);
            if (healthAmount == 0)
            {
                deathParticle.Play();
                LevelManager.instance.UpdateMonsterNumber();
                DOVirtual.DelayedCall(0.2f, () => { Destroy(gameObject); });
                
            }
        }
        else if (other.tag == "AgroField")
        {
            if (!startFight)
            {
                FindObjectOfType<AudioManager>().Play("FireBallRain");
                skullAnimator.Play("WandSkullAnim");
                StartCoroutine(Fiver());
                witchStats.SetActive(true);
            }
            startFight = true;
            
        }

    }

    IEnumerator Fiver()
    {
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(0.5f);
            Vector3 sky = ship.transform.position + upper;
            Instantiate(fallingBall, sky, Quaternion.identity);
        }
        

    }
}
