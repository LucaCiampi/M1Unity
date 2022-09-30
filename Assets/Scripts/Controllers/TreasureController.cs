using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureController : MonoBehaviour
{

    private GameObject goldIngot;
    [Range(1, 100)]
    public int rotateSpeed = 50;
    // Start is called before the first frame update
    void Start()
    {

    }

    void OnEnable()
    {
        PlayerController.instance.OnHasWin += OnWin;
        LevelManager.Instance.OnLevelLaunch += OnLevelLaunch;
        LevelManager.Instance.OnLevelReset += OnLevelReset;
    }

    // Update is called once per frame
    void Update()
    {
        if (goldIngot != null)
            goldIngot.transform.Rotate(new Vector3(0, rotateSpeed * Time.deltaTime, 0), Space.World);
    }

    void OnWin()
    {
        Destroy(goldIngot);
    }

    void OnLevelLaunch(int levelId)
    {
        goldIngot = GameObject.FindGameObjectWithTag("Treasure");
    }

    void OnLevelReset()
    {
        goldIngot = null;
    }
}
