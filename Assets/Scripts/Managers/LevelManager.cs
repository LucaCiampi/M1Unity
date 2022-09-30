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
    private int _levelClearance = 0;
    public int LevelClearance
    {
        get => _levelClearance; 
        set
        {
            this._levelClearance = value;
            this.OnUpdateLevelClearance.Invoke();
        }
    }

    //events
    public delegate void LevelLaunch(int levelId);
    public event LevelLaunch OnLevelLaunch;
    public delegate void LevelReset();
    public event LevelReset OnLevelReset;
    public delegate void UpdateLevelClearance();
    public event UpdateLevelClearance OnUpdateLevelClearance;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        PlayerController.instance.OnHasWin += WinLevel;
    }

    public void OnLaunchLevel1() { this.LaunchLevel(1); }
    public void OnLaunchLevel2() { this.LaunchLevel(2); }
    public void OnLaunchLevel3() { this.LaunchLevel(3); }

    public void LaunchLevel(int levelId)
    {
        this.OnLevelLaunch.Invoke(levelId);
    }
    public void ResetLevel()
    {
        this.OnLevelReset.Invoke();
    }
    private void WinLevel()
    {
        LevelClearance += 1;
        this.OnLevelReset.Invoke();
    }
}
