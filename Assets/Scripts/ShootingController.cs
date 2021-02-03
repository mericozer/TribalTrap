using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShootingController : MonoBehaviour
{
    [SerializeField] private List<ParticleSystem> leftSide = new List<ParticleSystem>(), rightSide = new List<ParticleSystem>();
    [SerializeField] private List<GameObject> rightCannons = new List<GameObject>();
    [SerializeField] private List<GameObject> leftCannons = new List<GameObject>();
    [SerializeField] private GameObject cannonBall;
    [SerializeField] private float speed = 20000f;
    [SerializeField] private Image leftCooldownSpiral, rightCooldownSpiral;
    [SerializeField] private TextMeshProUGUI leftCooldownCounter, rightCooldownCounter;
    [SerializeField] private bool isRightCannonEnabled = true;
    private float leftFireStart = -2.5f;
    private float rightFireStart = -2.5f;
    private float fireCooldown = 2.5f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (Time.time > leftFireStart + fireCooldown)
            {
                FindObjectOfType<AudioManager>().Play("CannonSound");

                foreach (GameObject objects in leftCannons)
                {
                    GameObject instCannon = Instantiate(cannonBall, objects.transform.position, Quaternion.identity);
                    Rigidbody instCannonBallRB = instCannon.GetComponent<Rigidbody>();
                    instCannonBallRB.AddForce(objects.transform.forward * speed, ForceMode.Acceleration);
                }
                foreach (ParticleSystem system in leftSide)
                {
                    system.Play();
                }
                leftFireStart = Time.time;
                StartCoroutine(LeftCannonCooldown());
            }
        }
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(!isRightCannonEnabled) return;
            if (Time.time > rightFireStart + fireCooldown)
            {
                FindObjectOfType<AudioManager>().Play("CannonSound");

                foreach (GameObject objects in rightCannons)
                {
                    GameObject instCannon = Instantiate(cannonBall, objects.transform.position, Quaternion.identity);
                    Rigidbody instCannonBallRB = instCannon.GetComponent<Rigidbody>();
                    instCannonBallRB.AddForce(objects.transform.forward * speed, ForceMode.Acceleration);
                }
                foreach (ParticleSystem system in rightSide)
                {
                    system.Play();

                }
                rightFireStart = Time.time;
                StartCoroutine(RightCannonCooldown());
            }
        }
    }

    private IEnumerator LeftCannonCooldown()
    {
        leftCooldownSpiral.gameObject.SetActive(true);
        while (Time.time < leftFireStart + fireCooldown)
        {
            leftCooldownSpiral.fillAmount = (leftFireStart + fireCooldown - Time.time) / fireCooldown;
            leftCooldownCounter.SetText((leftFireStart + fireCooldown - Time.time).ToString("N2"));
            yield return null;
        }
        leftCooldownSpiral.gameObject.SetActive(false);
        yield return null;
    }
    private IEnumerator RightCannonCooldown()
    {
        rightCooldownSpiral.gameObject.SetActive(true);
        while (Time.time < rightFireStart + fireCooldown)
        {
            rightCooldownSpiral.fillAmount = (rightFireStart + fireCooldown - Time.time) / fireCooldown;
            rightCooldownCounter.SetText((rightFireStart + fireCooldown - Time.time).ToString("N2"));
            yield return null;
        }
        rightCooldownSpiral.gameObject.SetActive(false);
        yield return null;
    }
    
}
