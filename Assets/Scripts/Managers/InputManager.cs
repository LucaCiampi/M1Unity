using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager _instance = null;
    public static InputManager instance
    {
        get
        {
            if (!_instance) _instance = FindObjectOfType<InputManager>();
            return _instance;
        }
        private set
        {
            _instance = value;
        }
    }

    // Events
    public delegate void InputSpacebarEvent();
    public event InputSpacebarEvent OnUserShoot;
    
    // Start is called before the first frame update
    void Start()
    {
        if (InputManager.instance == null) InputManager.instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) this.OnUserShoot.Invoke();
    }
}
