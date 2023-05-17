using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Clock : MonoBehaviour
{
    public float timeLimit = 60f; // Total time in seconds
    private float currentTime = 0f; // Current time in seconds
    [SerializeField]private TextMeshProUGUI timerText;
    bool IsActive = true;
    public delegate void OnTimerExpired();
    public static event OnTimerExpired ontimerExpired;
    bool IsROundStarted = false;
    private void OnEnable()
    {
        WallManager.OnAllModelsPlace += onAllModelsPlaced;
        PuzzleManager.OnRoundStart += OnRoundStarted;
    }

   

    private void OnDisable()
    {
        WallManager.OnAllModelsPlace -= onAllModelsPlaced;
        PuzzleManager.OnRoundStart -= OnRoundStarted;
    }

   

    private void Start()
    {
        //timerText = GetComponent<Text>();
        currentTime = timeLimit;
        timerText.text = string.Format("{0:00}:{1:00}", 00, currentTime);
    }

    private void Update()
    {
        if (!IsActive || !IsROundStarted) return;
        currentTime -= Time.deltaTime;
        if (currentTime <= 0)
        {
            currentTime = 0;
            IsActive = false;
            ontimerExpired?.Invoke();
            PuzzleManager.Instance.CallPolice(false);
        }
        

        // Check if time has run out
       

        // Convert time to minutes and seconds
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);

        // Update the timer display
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    private void onAllModelsPlaced()
    {
        IsActive = false;
    }
    private void OnRoundStarted()
    {
        IsROundStarted = true;
    }
}
