using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using static PuzzleManager;
using TMPro;
using UnityEngine.UI;
using System;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance { get; private set; }
    public delegate void Callpolice(bool WinStatus);
    public delegate void RoundStart(); 
    public static event RoundStart OnRoundStart;
    public static event Callpolice OnPoliceCalled;
    [SerializeField] float PoliceDelayDuration = 2;
    [SerializeField] GameObject StartTExt;
    [SerializeField] private int StartSceneIndex = 0;
    [SerializeField] GameObject[] Win_failPanel;
    [SerializeField] float WinFAilPanelDelay = 7;
    [SerializeField] TextMeshProUGUI WinText;
    [SerializeField] TextMeshProUGUI FailText;
    [SerializeField]string wintextValue= "Escaped!!!";
    [SerializeField]string failtextValue= "Caught!!!";
    [SerializeField]bool IsSequenceRound=false;
    [SerializeField] Button SkipToLevel;
    [SerializeField] TMP_InputField SkiplevelInput;
    private bool IsEditor = false;
    bool IsRoundStarted = false;
    int scenecounts;

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
        if (SceneManager.GetActiveScene().buildIndex <= StartSceneIndex)
        {
            SkiplevelInput.gameObject.SetActive(true);
            SkipToLevel.gameObject.SetActive(true);
        }
        WinText.text = wintextValue;
        FailText.text = failtextValue;
        scenecounts = SceneManager.sceneCountInBuildSettings;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Restart(); 
        }
        if (!IsRoundStarted && Input.GetMouseButtonDown(0))
        {
            IsRoundStarted = true;
            StartRound();
        }
       
    }
    public void Win(bool winstatus)
    {
        if (winstatus == true && IsSequenceRound)
        {
            if (SequenceManager.Instance.CheckSequenceActiveStatus())
            {
                SequenceManager.Instance.NextSequencePose();
                return;
            }
        }
        DOVirtual.DelayedCall(WinFAilPanelDelay, () =>
        {
            if (winstatus)
            {
                //Debug.Log("scene CLeared");
                PanelActivate(1);
            }
            else
            {
                PanelActivate(0);
            }
        });
        
    }
    void PanelActivate(int index)
    {
        foreach (GameObject obj in Win_failPanel)
        { 
            obj.SetActive(false);
            
        }
        if (index >= Win_failPanel.Length)
        {
            return;
        }
        else 
        {
            Win_failPanel[index].SetActive(true);
        }
    }
    public void CallPolice(bool WinStatus)
    {
        DOVirtual.DelayedCall(PoliceDelayDuration,() => 
        {
            OnPoliceCalled?.Invoke(WinStatus);
        });
        if (IsSequenceRound)
        {
            if (SequenceManager.Instance.CheckSequenceActiveStatus())
            {
                return;
            }
        }
        if (WinStatus)
        {
            Win(true);
            
        }
        else
        {
            Win(false);
        }
        Debug.Log("Scene Cleared " + WinStatus);
    }
    public void StartRound()
    { 
        StartTExt.SetActive(false);
        OnRoundStart?.Invoke();
       // Debug.Log("Round Started");
    }
    public void Restart()
    {
        DOTween.KillAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        /*
        if(GAScript.Instance)
            GAScript.Instance.LevelCompleted(SceneManager.GetActiveScene().name);
            */

        int index = SceneManager.GetActiveScene().buildIndex;
        index++;

        if (index <= SceneManager.sceneCountInBuildSettings - 1)
        {

            SceneManager.LoadScene(index);
        }
        else
        {
            SceneManager.LoadScene(StartSceneIndex);
        }
    }
    public void JumpToLevel()
    {
        int level;
        
        if (!Int32.TryParse(SkiplevelInput.text,out level)) return;
         level= (Int32.Parse(SkiplevelInput.text));
        Debug.Log("Level to " + level);
        if (level > scenecounts || level-1<0) 
        {
            Debug.Log("scene counts " + scenecounts);
            return;
        }
            
        SceneManager.LoadScene(--level);
    }
}
