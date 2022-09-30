using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        if (player == null) player = GameObject.FindGameObjectWithTag("Player");
        PlayerController.instance.OnPlayerGotHit += RemoveLife;
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
        print("Game over");
    }

    /**
     * removes a living being from the game
     * @param target : the transform component of the target
     */
    public void killLivingBeing(GameObject target)
    {
        Destroy(target);
    }
}