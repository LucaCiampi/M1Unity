using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GuiManager : MonoBehaviour
{
    private static GuiManager _instance = null;

    public static GuiManager instance
    {
        get
        {
            if (!_instance) _instance = FindObjectOfType<GuiManager>();
            return _instance;
        }
        private set { _instance = value; }
    }
    
    public GameObject lifeLayout;
    public GameObject heartPrefab;
    public GameObject skullPrefab;
    public GameObject levelSelector;
    public Button[] levelsButtons;
    public GameObject backButton;
    public GameObject youDiedPanel;

    // Events
    public delegate void GameStatusEvent();

    public event GameStatusEvent OnRestartButtonPressed;
    public event GameStatusEvent OnBackToMenuButtonPressed;

    // Start is called before the first frame update
    void Start()
    {
        PlayerController.instance.OnPlayerGotHit += UpdateLivesLeft;
        GameManager.instance.OnGameOver += DisplayYouDiedGameOver;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnEnable()
    {
        LevelManager.Instance.OnLevelLaunch += HideMenu;
        LevelManager.Instance.OnLevelReset += DisplayMenu;
        LevelManager._Instance.OnUpdateLevelClearance += RefreshLevelButtons;
    }

    /**
     * Updates the number of lives left on GUI
     */
    public void UpdateLivesLeft()
    {
        foreach (Transform child in lifeLayout.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        if (GameManager.instance.livesLeft > 0)
        {
            for (int i = 1; i <= GameManager.instance.livesLeft; i++)
            {
                GameObject.Instantiate(heartPrefab).transform.SetParent(lifeLayout.transform);;
            }
        }
        else
        {
            GameObject.Instantiate(skullPrefab).transform.SetParent(lifeLayout.transform);
        }
    }

    private void DisplayMenu()
    {
        levelSelector.SetActive(true);
        backButton.SetActive(false);
    }

    private void HideMenu(int levelId)
    {
        levelSelector.SetActive(false);
        backButton.SetActive(true);
    }

    private void RefreshLevelButtons()
    {
        for (int i = 0; i < levelsButtons.Length; i++)
        {
            levelsButtons[i].interactable = LevelManager.Instance.LevelClearance >= i ? true : false;
        }
    }
    /**
     * Displays the "You died" game over panel on player death
     */
    private void DisplayYouDiedGameOver()
    {
        youDiedPanel.SetActive(true);
    }

    /**
     * The restart button has been clicked on
     */
    public void RestartButtonPressed()
    {
        this.OnRestartButtonPressed.Invoke();
    }
    /**
     * The back to menu button has been clicked on
     */
    public void BackToMenuPressed()
    {
        this.OnBackToMenuButtonPressed.Invoke();
    }
}