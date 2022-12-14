using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public int[,,] mapGrid = {{
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
        { 4, 0, 3, 0, 0, 0, 0, 0, 3, 0, 1 },
        { 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1 },
        { 1, 3, 1, 0, 1, 0, 0, 0, 1, 0, 1 },
        { 1, 0, 1, 0, 1, 0, 1, 0, 1, 1, 1 },
        { 1, 0, 3, 0, 1, 0, 1, 0, 1, 0, 1 },
        { 1, 0, 1, 1, 1, 0, 1, 3, 1, 0, 1 },
        { 1, 0, 1, 0, 0, 0, 1, 0, 1, 0, 1 },
        { 1, 3, 1, 0, 1, 0, 1, 0, 1, 0, 1 },
        { 1, 0, 0, 3, 1, 0, 1, 0, 0, 0, 1 },
        { 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1 }
    },
    {
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
        { 4, 0, 1, 0, 0, 0, 0, 3, 0, 0, 1 },
        { 1, 3, 1, 3, 1, 1, 1, 1, 1, 0, 1 },
        { 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1 },
        { 1, 1, 1, 1, 1, 0, 3, 0, 1, 3, 1 },
        { 1, 0, 1, 0, 0, 3, 0, 0, 1, 0, 1 },
        { 1, 3, 1, 0, 0, 0, 0, 0, 0, 0, 1 },
        { 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1 },
        { 1, 0, 1, 1, 1, 1, 1, 3, 1, 0, 1 },
        { 1, 0, 0, 3, 0, 0, 0, 0, 1, 0, 1 },
        { 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1 }
    },
    {
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
        { 4, 0, 1, 0, 3, 0, 0, 3, 0, 0, 1 },
        { 1, 0, 1, 0, 1, 0, 1, 1, 1, 0, 1 },
        { 1, 3, 0, 0, 1, 3, 1, 0, 1, 1, 1 },
        { 1, 1, 1, 1, 1, 0, 1, 0, 1, 0, 1 },
        { 1, 3, 0, 0, 0, 0, 1, 3, 1, 0, 1 },
        { 1, 0, 1, 3, 1, 1, 1, 0, 0, 3, 1 },
        { 1, 0, 1, 0, 0, 1, 0, 0, 1, 0, 1 },
        { 1, 3, 1, 1, 1, 1, 1, 0, 1, 0, 1 },
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
    },
    {
        { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 },
        { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 },
        { 2, 2, 2, 2, 2, 0, 0, 0, 0, 2, 2 },
        { 2, 2, 2, 2, 2, 0, 0, 0, 0, 0, 2 },
        { 2, 2, 2, 2, 2, 0, 0, 0, 0, 2, 2 },
        { 2, 2, 0, 0, 0, 0, 0, 0, 0, 2, 2 },
        { 2, 2, 0, 0, 0, 0, 0, 0, 0, 2, 2 },
        { 2, 2, 0, 0, 0, 0, 0, 0, 2, 2, 2 },
        { 2, 2, 0, 0, 0, 0, 0, 0, 2, 2, 2 },
        { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 },
        { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }
    }
    };
    public GameObject wallPrefab;
    public WallPreset WallPreset1;
    public WallPreset WallPreset2;
    public GameObject MazeContainer;
    public GameObject treasurePrefab;
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
                if (mapGrid[levelId - 1, i, j] == 1)//wall
                {
                    GameObject wallInstance;
                    wallInstance = Instantiate(wallPrefab, new Vector3(j * 3, 1.5f, i * -3), Quaternion.identity);
                    wallInstance.transform.SetParent(MazeContainer.transform);
                    if (mapWallTypes[levelId - 1, i, j] == 1)
                        wallInstance.GetComponent<WallController>().preset = this.WallPreset1;
                    if (mapWallTypes[levelId - 1, i, j] == 2)
                        wallInstance.GetComponent<WallController>().preset = this.WallPreset2;
                }
                if (mapGrid[levelId - 1, i, j] == 3)//enemy spawner
                {
                    GameManager.instance.enemiesSpawners.Add(new Vector3(j * 3, 1.5f, i * -3));
                }
                if (mapGrid[levelId - 1, i, j] == 4)//treasure
                {
                    GameObject treasureInstance;
                    treasureInstance = Instantiate(treasurePrefab, new Vector3(j * 3, 1f, i * -3), Quaternion.identity);
                    treasureInstance.transform.SetParent(MazeContainer.transform);
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
