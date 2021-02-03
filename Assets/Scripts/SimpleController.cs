using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Rigidbody))]
public class SimpleController : MonoBehaviour
{
    [SerializeField] private float turnSpeed = 20;
    private const float moveSpeed = 900;
    [SerializeField] private List<ParticleSystem> dashWind = new List<ParticleSystem>();

    private const float maxDashTime = 1.0f;
    [SerializeField] private float dashDistance = 100;
    [SerializeField] private float dashStoppingSpeed = 0.1f;
    [SerializeField] private float currentDashTime = maxDashTime;
   // [SerializeField] private float dashSpeed = 6;
    [SerializeField] private bool isDashEnabled = true;
    private float dashStart = -2f;
    private float dashCooldown = 2f;

    [SerializeField] private Image dashCooldownSpiral;
    [SerializeField] private TextMeshProUGUI dashCooldownCounter;

    private Rigidbody _rb;
    // Update is called once per frame
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!CanvasController.instance.isGameRunning) return;
        transform.Rotate(Vector3.up, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime);
        _rb.velocity = transform.forward * Time.deltaTime * Input.GetAxisRaw("Vertical") * moveSpeed;
        CheckAndDash();
        // _rb.AddForce(transform.forward * Time.deltaTime * Input.GetAxis("Vertical") * moveSpeed);
    }

    private void CheckAndDash()
    {
        if (!CanvasController.instance.isGameRunning)
        {
            _rb.velocity = Vector3.zero;
            if(Input.GetKeyDown(KeyCode.Space) && CanvasController.instance.healthAmount != 0)
                CanvasController.instance.LoadNextLevel();
            return;
        }
        if (!isDashEnabled) return;
        if (Input.GetKeyDown(KeyCode.Space)) //Right mouse button
        {
            if (Time.time > dashStart + dashCooldown)
            {
                currentDashTime = 0;
            }

        }
        if (currentDashTime < maxDashTime)
        {
            _rb.velocity = transform.forward * dashDistance;
            currentDashTime += dashStoppingSpeed;

            foreach (ParticleSystem system in dashWind)
            {
                system.Play();
            }
            dashStart = Time.time;
            StartCoroutine(DashCannonCooldown());
        }


    }

    private IEnumerator DashCannonCooldown()
    {
        dashCooldownSpiral.gameObject.SetActive(true);
        while (Time.time < dashStart + dashCooldown)
        {
            dashCooldownSpiral.fillAmount = (dashStart + dashCooldown - Time.time) / dashCooldown;
            dashCooldownCounter.SetText((dashStart + dashCooldown - Time.time).ToString("N2"));
            yield return null;
        }
        dashCooldownSpiral.gameObject.SetActive(false);
        yield return null;
    }
}
