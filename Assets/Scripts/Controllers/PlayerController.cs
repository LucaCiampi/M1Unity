using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public LivingBeingSettings preset;
    public float rotationSpeed = 10f;
    public Camera camera;
    public float maxHitDistance = 100f;

    private Vector3 _direction;
    private Vector3 _rotation;

    private bool _playerInvicible = false;

    private static PlayerController _instance = null;
    public static PlayerController instance
    {
        get
        {
            if (!_instance) _instance = FindObjectOfType<PlayerController>();
            return _instance;
        }
        private set
        {
            _instance = value;
        }
    }
    
    // Events
    public delegate void PlayerLifeEvent();
    public event PlayerLifeEvent OnPlayerGotHit;

    private void Start()
    {
        InputManager.instance.OnUserShoot += Shoot;
    }

    // Update is called once per frame
    void Update()
    {
        _direction = new Vector3(0, 0, Input.GetAxis("Vertical"));

        MovePlayer(_direction);
        RotatePlayer(Input.GetAxis("Horizontal"));
    }

    /**
     * Moves the player forward or backward
     */
    private void MovePlayer(Vector3 direction)
    {
        this.transform.Translate(direction * preset.speed * Time.deltaTime);
    }
    
    /**
     * Rotates the player
     */
    private void RotatePlayer(float rotation)
    {
        this.transform.Rotate(0, rotation * rotationSpeed * Time.deltaTime, 0);
    }
    
    /**
     * Detects collision
     */
    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Enemy" && !_playerInvicible)
        {
            this.OnPlayerGotHit.Invoke();
            _playerInvicible = true;
            print("player got hit");
            StartCoroutine(PlayerInvicibilityTime());
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
     * Simulates shooting ball trajectory
     */
    private void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, maxHitDistance))
        {
            if (hit.transform.CompareTag("Enemy"))
            {
                GameManager.instance.killLivingBeing(hit.transform.gameObject);
            }
        }
    }
}
