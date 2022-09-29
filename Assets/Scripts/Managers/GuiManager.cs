using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

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
    
    // Start is called before the first frame update
    void Start()
    {
        PlayerController.instance.OnPlayerGotHit += UpdateLivesLeft;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /**
     * Updates the number of lives left on GUI
     */
    private void UpdateLivesLeft()
    {
        livesLeftText.text = "Life : " + GameManager.instance.livesLeft;
        
    }
}
