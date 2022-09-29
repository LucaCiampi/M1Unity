using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.AI;

public class MoveToPlayer : MonoBehaviour
{
    public bool hasSeenPlayer = true;
    public NavMeshAgent agent;

    private GameObject _player;
    
    // Start is called before the first frame update
    void Start()
    {
        _player = GameManager.instance.player;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasSeenPlayer)
        {
            agent.SetDestination(_player.transform.position);
        }
    }

    
}
