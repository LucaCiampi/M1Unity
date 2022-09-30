using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LivingBeingSettings : ScriptableObject
{
    public float speed = 1.0f;
    public float rotationSpeed = 10f;
    public float maxHitDistance = 3f;
    public bool killOnTouch = false;
    public byte health = 1;
}
