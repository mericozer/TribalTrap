using System;
using DG.Tweening;
using UnityEngine;

public class MonsterBehaviour : MonoBehaviour
{
    private static float _speed = 10;
    private bool _isInsideAgroField = false, _justDidDamage = false, _inRange = false;
    [SerializeField] private bool lastLevel;
    public Transform ship;
    private Animator _animator;
    private static readonly int Attacking = Animator.StringToHash("Attacking");
    private static int _maxHealth = 4;
    private int _currentHealth = _maxHealth;
    private float damageTimer = 0.5f;
    [SerializeField] private ParticleSystem _deathParticle, _hitParticle;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!CanvasController.instance.isGameRunning) return;
        if (_isInsideAgroField && !_justDidDamage) //TODO: Check if damage can be done.
        {
            transform.LookAt(ship);
            transform.position += (ship.transform.position - transform.position).normalized * Time.deltaTime * _speed;
        }
        if (_inRange)
        {
            if (damageTimer > 0)
            {
                damageTimer -= Time.deltaTime;
            }
            else {
                CanvasController.instance.UpdateHealthBar("dec");
                damageTimer = 0.5f;
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
                    _deathParticle.Play();
                    _isInsideAgroField = false;
                    _animator.Play("Dead");
                    DOVirtual.DelayedCall(1f, () => { Destroy(gameObject); });
                }
                else
                {
                    _hitParticle.Play();
                    _animator.Play("Get_hit");
                }
                break;
            case "Ship":
                CanvasController.instance.UpdateHealthBar("dec");
                _inRange = true;
                _justDidDamage = true;
                Invoke(nameof(EnableFollow), 2);
                break;
            case "AgroField":
                _isInsideAgroField = true;
                _animator.SetBool(Attacking, true);
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
            _animator.SetBool(Attacking, false);
        }

        if (other.CompareTag("Ship"))
        {
            _inRange = false;
        }
    }
}
