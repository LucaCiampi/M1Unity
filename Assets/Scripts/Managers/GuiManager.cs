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
        private set
        {
            _instance = value;
        }
    }

    public TextMeshProUGUI livesLeftText;
    public GameObject lifeLayout;
    public Sprite lifeHeartSprite;
    public GameObject levelSelector;
    public Button[] levelsButtons;
    public GameObject backButton;
    public GameObject youDiedPanel;
    
    // Events
    public delegate void GameStatusEvent();
    public event GameStatusEvent OnRestartButtonPressed;
    
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
    private void UpdateLivesLeft()
    {
        livesLeftText.text = "Life : " + GameManager.instance.livesLeft;
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
}
