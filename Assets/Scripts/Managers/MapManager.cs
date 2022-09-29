using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public int[,,] mapGrid = {{
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
    },
    {
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
        { 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1 },
        { 1, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1 },
        { 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1 },
        { 1, 1, 1, 1, 1, 0, 0, 0, 1, 0, 1 },
        { 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1 },
        { 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1 },
        { 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1 },
        { 1, 0, 1, 1, 1, 1, 1, 0, 1, 0, 1 },
        { 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1 },
        { 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1 }
    }
    };
    public int[,,] mapWallTypes = {{
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
    },
    {
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
        { 1, 1, 1, 2, 2, 2, 2, 2, 2, 1, 1 },
        { 1, 1, 2, 2, 2, 2, 2, 2, 2, 1, 1 },
        { 1, 1, 2, 2, 2, 2, 2, 2, 2, 1, 1 },
        { 1, 1, 2, 2, 2, 2, 2, 2, 2, 1, 1 },
        { 1, 1, 2, 2, 2, 2, 2, 2, 2, 1, 1 },
        { 1, 1, 2, 2, 2, 2, 2, 2, 2, 1, 1 },
        { 1, 1, 2, 2, 2, 2, 2, 2, 2, 1, 1 },
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }
    }
    };
    public GameObject wallPrefab;
    public WallPreset WallPreset1;
    public WallPreset WallPreset2;
    public GameObject MazeContainer;
    // Start is called before the first frame update
    void Start()
    {

    }

    void OnEnable()
    {
        LevelManager.Instance.OnLevelLaunch += createMap;
    }
    void createMap(int levelId)
    {
        //destroy previous blocks
        while (MazeContainer.transform.childCount > 0)
        {
            DestroyImmediate(MazeContainer.transform.GetChild(0).gameObject);
        }
        //instanciate blocks with the map
        for (int i = 0; i < 11; i++)
        {
            for (int j = 0; j < 11; j++)
            {
                if (mapGrid[levelId - 1, i, j] == 1)
                {
                    GameObject wallInstance;
                    wallInstance = Instantiate(wallPrefab, new Vector3(j * 3, 0, i * -3), Quaternion.identity);
                    wallInstance.transform.SetParent(MazeContainer.transform);
                    if (mapWallTypes[levelId - 1, i, j] == 1)
                        wallInstance.GetComponent<WallController>().preset = this.WallPreset1;
                    if (mapWallTypes[levelId - 1, i, j] == 2)
                        wallInstance.GetComponent<WallController>().preset = this.WallPreset2;
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
