using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance { get; private set; }
    public delegate void Callpolice(bool WinStatus);
    public delegate void RoundStart(); 
    public static event RoundStart OnRoundStart;
    public static event Callpolice OnPoliceCalled;
    [SerializeField] float PoliceDelayDuration = 2;
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
        if (Input.GetKeyUp(KeyCode.S))
        { 
            StartRound();
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
        DOVirtual.DelayedCall(PoliceDelayDuration,() => 
        {
            OnPoliceCalled?.Invoke(WinStatus);
        });
        if (WinStatus)
        {

            
        }
        else
        {
            
        }
        Debug.Log("Scene Cleared " + WinStatus);
    }
    public void StartRound()
    { 
        OnRoundStart?.Invoke();
        Debug.Log("Round Started");
    }
}
