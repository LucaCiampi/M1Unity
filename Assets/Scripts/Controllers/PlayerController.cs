using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public LivingBeingSettings preset;
    public float rotationSpeed = 10f;

    private Vector3 _direction;
    private Vector3 _rotation;

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
    public delegate void PlayerLivesEvent();
    public event PlayerLivesEvent OnPlayerLivesUpdate;

    private void Start()
    {
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
        if (collision.transform.tag == "Enemy")
        {
            this.OnPlayerLivesUpdate.Invoke();
        }
    }
}
