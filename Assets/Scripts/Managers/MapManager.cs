using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public int[,] mapGrid = {
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
        { 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1 },
        { 1, 0, 1, 0, 1, 0, 0, 0, 1, 0, 1 },
        { 1, 0, 1, 0, 1, 0, 1, 0, 1, 1, 1 },
        { 1, 0, 0, 0, 1, 0, 1, 0, 1, 0, 1 },
        { 1, 0, 1, 1, 1, 0, 1, 0, 1, 0, 1 },
        { 1, 0, 1, 0, 0, 0, 1, 0, 1, 0, 1 },
        { 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1 },
        { 1, 0, 0, 0, 1, 0, 1, 0, 0, 0, 1 },
        { 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1 }
    };
    public int[,] mapWallTypes = {
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 1, 1, 1, 1, 1, 0, 0 },
        { 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0 },
        { 0, 0, 0, 0, 1, 0, 0, 0, 1, 1, 0 },
        { 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0 },
        { 0, 0, 1, 1, 1, 0, 0, 0, 1, 0, 0 },
        { 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0 },
        { 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
    };
    public GameObject wallPrefab;
    public WallPreset WallPreset;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 11; i++)
        {
            for (int j = 0; j < 11; j++)
            {
                if (mapGrid[i, j] == 1)
                    Instantiate(wallPrefab, new Vector3(j * 3, 0, i * -3), Quaternion.identity).GetComponent<WallController>().preset = ( mapWallTypes[i, j] == 1 ? this.WallPreset : null) ;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
