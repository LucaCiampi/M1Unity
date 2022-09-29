using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallManager : MonoBehaviour
{
    public WallPreset preset;
    public MeshRenderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        this.renderer.material = preset.Material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
