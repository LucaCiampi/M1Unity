using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{
    public WallPreset preset;
    public new MeshRenderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        if (preset != null)
            this.renderer.material = preset.Material;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
