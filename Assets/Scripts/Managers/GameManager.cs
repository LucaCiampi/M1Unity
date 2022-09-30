using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;

public class GameManager : MonoBehaviour
{
    [Tooltip("Amount of lives left")] public byte livesLeft = 3;
    public GameObject player;
    public GameObject[] enemiesPrefab;
    public GameObject enemy;

    private static GameManager _instance = null;

    public static GameManager instance
    {
        get
        {
            if (!_instance) _instance = FindObjectOfType<GameManager>();
            return _instance;
        }
        private set { _instance = value; }
    }
    
    // Events
    public delegate void GameStatusEvent();
    public event GameStatusEvent OnGameOver;

    private void OnEnable()
    {
        PlayerController.instance.OnPlayerGotHit += RemoveLife;
        GuiManager.instance.OnRestartButtonPressed += RestartGame;
        GuiManager.instance.OnBackToMenuButtonPressed += BackToMenu;
        LevelManager.Instance.OnLevelLaunch += SetPlayerPositionToMazeEntrance;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (player == null) player = GameObject.FindGameObjectWithTag("Player");
        GuiManager.instance.UpdateLivesLeft();
        InvokeRepeating("SpawnEnemy", 0f, 5.0f);
    }

    // Update is called once per frame
    void Update()
    {
    }

    /**
     * Removes a life to the player
     */
    public void RemoveLife()
    {
        if (livesLeft > 0)
        {
            livesLeft -= 1;
            checkForGameOver();
        }
    }

    /**
     * Checks if the player is dead
     */
    private void checkForGameOver()
    {
        if (livesLeft <= 0)
        {
            GameOver();
        }
    }

    /**
     * The game is over
     */
    private void GameOver()
    {
        this.OnGameOver.Invoke();
    }

    /**
     * removes a living being from the game
     * @param target : the transform component of the target
     */
    public void killLivingBeing(GameObject target)
    {
        Destroy(target);
    }

    /**
     * Restarts the game
     */
    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /**
     * Goes back to level selection
     */
    private void BackToMenu()
    {
        LevelManager.Instance.ResetLevel();
    }

    /**
     * Sets the player position to 0,0
     */
    private void SetPlayerPositionToMazeEntrance(int levelID)
    {
        player.transform.position = new Vector3(15, 1, -35);
    }

    /**
     * Instantiates an enemy within the array of enemies passed
     */
    public void SpawnEnemy()
    {
        Random rnd = new Random();
        int randomEnemyIndex = rnd.Next(0, enemiesPrefab.Length);
        Instantiate(enemiesPrefab[randomEnemyIndex].gameObject);
    }
}