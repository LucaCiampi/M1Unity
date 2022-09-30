using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;

public class GameManager : MonoBehaviour
{
    [Tooltip("Amount of lives left")] public byte livesLeft;
    public GameObject player;
    public GameObject[] enemiesPrefab;
    [HideInInspector] public List<Vector3> enemiesSpawners;
    public GameObject healthPotionPrefab;

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
        PlayerController.instance.OnPlayerGotHealth += AddLife;
        GuiManager.instance.OnRestartButtonPressed += RestartGame;
        GuiManager.instance.OnBackToMenuButtonPressed += BackToMenu;
        LevelManager.Instance.OnLevelLaunch += StartLevel;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (player == null) player = GameObject.FindGameObjectWithTag("Player");
        GuiManager.instance.UpdateLivesLeft();
        livesLeft = PlayerController.instance.preset.health;
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
     * Adds a life to the player
     */
    public void AddLife()
    {
        if (livesLeft > 0)
        {
            livesLeft += 1;
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
        Random rnd = new Random();
        int chanceDroppingHealth = rnd.Next(1, 4);
        if (chanceDroppingHealth == 1)
        {
            Instantiate(healthPotionPrefab).transform.position = target.transform.position;
        }

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
     * The level has started
     */
    private void StartLevel(int levelId)
    {
        foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Destroy(enemy);
        }

        livesLeft = 3;
        InvokeRepeating("SpawnEnemy", 0f, 5.0f);
        SetPlayerPositionToMazeEntrance(levelId);
    }

    /**
     * Sets the player position to 0,0
     */
    private void SetPlayerPositionToMazeEntrance(int levelID)
    {
        player.transform.position = new Vector3(15, 1, -35);
        player.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    /**
     * Instantiates an enemy within the array of enemies passed
     */
    public void SpawnEnemy()
    {
        Random rnd = new Random();
        
        int randomEnemyIndex = rnd.Next(0, enemiesPrefab.Length);
        if (enemiesSpawners.Count > 0)
        {
            int randomEnemySpawner = rnd.Next(0, enemiesSpawners.Count);
            Instantiate(enemiesPrefab[randomEnemyIndex].gameObject).transform.position =
                enemiesSpawners[randomEnemySpawner];
        }
        else
        {
            Instantiate(enemiesPrefab[randomEnemyIndex].gameObject).transform.position = new Vector3(10, 3, -20);
        }
    }
}