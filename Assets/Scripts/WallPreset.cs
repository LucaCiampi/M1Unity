using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class WallPreset : ScriptableObject
{
    [Tooltip("Reference to the material to apply on the wall")]
    public Material Material;
}
