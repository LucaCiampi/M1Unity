using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    
    // Start is called before the first frame update
    void Start()
    {
        PlayerController.instance.OnPlayerLivesUpdate += UpdateLivesLeft;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateLivesLeft()
    {
        livesLeftText.text = "Life : " + GameManager.instance.livesLeft;
    }
}
