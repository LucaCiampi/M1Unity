using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager _Instance = null;
    public static LevelManager Instance
    {
        get
        {
            if (_Instance == null)
                _Instance = FindObjectOfType<LevelManager>();
            return _Instance;
        }
        private set
        {
            _Instance = value;
        }
    }
    public delegate void LevelLaunch(int levelId);
    public event LevelLaunch OnLevelLaunch;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnLaunchLevel1()
    {
        Debug.Log("launching 1 ...");
        this.LaunchLevel(1);
    }
    public void OnLaunchLevel2()
    {
        Debug.Log("launching 2 ...");
        this.LaunchLevel(2);
    }
    public void OnLaunchLevel3()
    {
        Debug.Log("launching 3 ...");
        this.LaunchLevel(3);
    }

    void LaunchLevel(int levelId)
    {
        OnLevelLaunch.Invoke(levelId);
    }
}
