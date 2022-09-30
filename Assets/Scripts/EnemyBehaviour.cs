using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    public LivingBeingSettings preset;

    // Start is called before the first frame update
    void Start()
    {
        if (this.GetComponent<NavMeshAgent>())
        {
            this.GetComponent<NavMeshAgent>().speed = preset.speed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
