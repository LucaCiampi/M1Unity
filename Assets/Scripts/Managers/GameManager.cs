using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Tooltip("Amount of lives left")] public byte livesLeft = 3;
    public GameObject player;

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
    
    // Start is called before the first frame update
    void Start()
    {
        if (player == null) player = GameObject.FindGameObjectWithTag("Player");
        PlayerController.instance.OnPlayerGotHit += RemoveLife;
        GuiManager.instance.OnRestartButtonPressed += RestartGame;
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
}