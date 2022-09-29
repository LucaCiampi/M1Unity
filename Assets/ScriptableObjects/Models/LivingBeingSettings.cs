using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LivingBeingSettings : ScriptableObject
{
    public float speed = 1.0f;
    public float jumpForce = 10f;
    public bool killOnTouch = false;
}
