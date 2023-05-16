using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance { get; private set; }
    public delegate void Callpolice(bool WinStatus);
    public static event Callpolice OnPoliceCalled;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        //Application.targetFrameRate = 60;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    public void Win(bool winstatus)
    {
        if (winstatus)
        {
            Debug.Log("scene CLeared");
        }
    }
    public void CallPolice(bool WinStatus)
    {
        if (WinStatus)
        { 
            OnPoliceCalled?.Invoke(WinStatus);
            Debug.Log("Scene Cleared "+WinStatus);
        }
    }
}
