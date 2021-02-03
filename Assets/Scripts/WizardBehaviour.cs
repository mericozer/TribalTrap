using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WizardBehaviour : MonoBehaviour
{
    private static float _speed = 10;
    private static float _rotationSpeed = 20;
    private bool _isInsideAgroField = false, _justDidDamage = false, _inRange = false;
    [SerializeField] private bool lastLevel;
    public Transform ship;
    private static readonly int Attacking = Animator.StringToHash("Attacking");
    private static int _maxHealth = 4;
    private int _currentHealth = _maxHealth;
    private float damageTimer = 2f;
    [SerializeField] GameObject fireBall;
    [SerializeField] GameObject fireBallSpawn;
    [SerializeField] private float speed = 100f;
    //add particles
    [SerializeField] private ParticleSystem _deathParticle, _hitParticle;

    private void Awake()
    {
        //_animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!CanvasController.instance.isGameRunning) return;
        if (_isInsideAgroField && !_justDidDamage) //TODO: Check if damage can be done.
        {
            float dist = Vector3.Distance(ship.position, transform.position);
            //if (dist < 20)
            //{
                // get the target direction:
                Vector3 targetDir = ship.position - transform.position;
                targetDir.y = 0; // kill any height difference to avoid tilting
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetDir), _rotationSpeed * Time.deltaTime);
                if (dist > 20)
                { // check min distance
                  // only move to the target if farther than min distance
                    transform.position += transform.forward * _speed * Time.deltaTime;
                }
            //}
        }
        if (_inRange)
        {
            //attack anim and spell
            if (damageTimer > 0)
            {
                damageTimer -= Time.deltaTime;
            }
            else
            {
                //_animator.Play("attack_short_001");
                GetComponent<Animation>().CrossFadeQueued("attack_short_001", 0.0f);
                DOVirtual.DelayedCall(0.9f, () => { SpawnFireBall(); });
                //GetComponent<Animation>().CrossFadeQueued("idle_combat");
                damageTimer = 2f;
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Cannon":
                _currentHealth--;
                Destroy(other.gameObject);
                if (_currentHealth == 0)
                {
                    if (!lastLevel)
                    { LevelManager.instance.UpdateMonsterNumber(); }
                    //wizard death anim
                    _deathParticle.Play();
                    _isInsideAgroField = false;
                    //wizard death anim
                    GetComponent<Animation>().CrossFade("dead", 0.0f);
                    //_animator.Play("dead");
                    //_animator.Play("Dead");
                    DOVirtual.DelayedCall(1f, () => { Destroy(gameObject); });
                }
                else
                {
                    //add particle to wizard
                     _hitParticle.Play();
                    GetComponent<Animation>().CrossFade("damage_001", 0.0f);
                    //GetComponent<Animation>().CrossFadeQueued("idle_combat");
                    //_animator.Play("damage_001");
                    //_animator.Play("Get_hit");
                }
                break;

                //no dmg with collision
           /* case "Ship":
                _inRange = true;

                //_justDidDamage = true;
                //Invoke(nameof(EnableFollow), 2);
                break;*/
            case "AgroField":
                GetComponent<Animation>().CrossFade("attack_short_001", 0.0f);
                DOVirtual.DelayedCall(0.9f, () => { SpawnFireBall(); });
               
                //GetComponent<Animation>().CrossFadeQueued("idle_combat");
                //_animator.Play("attack_short_001");
                _inRange = true;
                _isInsideAgroField = true;
                //wizard attack anim
                //_animator.SetBool(Attacking, true);
                break;
        }
    }


    private void EnableFollow()
    {
        _justDidDamage = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("AgroField"))
        {
            _isInsideAgroField = false;
            //wizard attack anim stop
            //_animator.SetBool(Attacking, false);
            _inRange = false;
        }

        /*if (other.CompareTag("Ship"))
        {
            _inRange = false;
        }*/
    }

    private void SpawnFireBall()
    {
        GameObject instCannon = Instantiate(fireBall, fireBallSpawn.transform.position, Quaternion.identity);
        Rigidbody instCannonBallRB = instCannon.GetComponent<Rigidbody>();
        instCannonBallRB.AddForce(fireBallSpawn.transform.forward * speed, ForceMode.Acceleration);
    }
}
