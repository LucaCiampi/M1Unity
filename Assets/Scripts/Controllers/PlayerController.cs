using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public LivingBeingSettings preset;

    private float _rotationSpeed;

    public new Camera camera;

    public float maxHitDistance
    {
        get => preset.maxHitDistance;
    }

    public Animation swordAttackAnimation;

    private Vector3 _direction;
    private Vector3 _rotation;
    private float _speed;

    private bool _playerInvicible = false;

    private static PlayerController _instance = null;

    public static PlayerController instance
    {
        get
        {
            if (!_instance) _instance = FindObjectOfType<PlayerController>();
            return _instance;
        }
        private set { _instance = value; }
    }

    // Events
    public delegate void PlayerLifeEvent();

    public event PlayerLifeEvent OnPlayerGotHit;
    public event PlayerLifeEvent OnPlayerGotHealth;

    public delegate void HasWin();

    public event HasWin OnHasWin;

    private void Awake()
    {
        this._speed = preset.speed;
        this._rotationSpeed = preset.rotationSpeed;
    }

    private void Start()
    {
        InputManager.instance.OnUserShoot += Attack;
        GameManager.instance.OnGameOver += FreezePlayer;
    }

    // Update is called once per frame
    void Update()
    {
        _direction = new Vector3(0, 0, Input.GetAxis("Vertical"));
        // animator.SetFloat("speed", _direction.z);

        MovePlayer(_direction);
        RotatePlayer(Input.GetAxis("Horizontal"));
    }

    /**
     * Moves the player forward or backward
     */
    private void MovePlayer(Vector3 direction)
    {
        this.transform.Translate(direction * _speed * Time.deltaTime);
    }

    /**
     * Rotates the player
     */
    private void RotatePlayer(float rotation)
    {
        this.transform.Rotate(0, rotation * _rotationSpeed * Time.deltaTime, 0);
    }

    /**
     * Detects collision
     */
    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Enemy") && !_playerInvicible)
        {
            this.OnPlayerGotHit.Invoke();
            _playerInvicible = true;
            StartCoroutine(PlayerInvicibilityTime());
        }

        else if (collision.transform.CompareTag("Treasure"))
        {
            this.OnHasWin.Invoke();
        }
        
        else if (collision.transform.CompareTag("Collectible"))
        {
            this.OnPlayerGotHealth.Invoke();
            Destroy(collision.gameObject); 
        }
    }

    /**
     * Prevents the player from taking damage for a specified amount of seconds
     */
    IEnumerator PlayerInvicibilityTime()
    {
        yield return new WaitForSeconds(2);
        this._playerInvicible = false;
    }

    /**
     * Simulates the player giving a sword attack
     */
    private void Attack()
    {
        swordAttackAnimation.Play();
        RaycastHit hit;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, maxHitDistance))
        {
            if (hit.transform.CompareTag("Enemy"))
            {
                GameManager.instance.killLivingBeing(hit.transform.gameObject);
            }
        }
    }

    /**
     * Prevents the player from moving
     */
    private void FreezePlayer()
    {
        this._speed = 0;
        this._rotationSpeed = 0;
    }
}