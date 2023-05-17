using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Clock;

public class RayDetector : MonoBehaviour
{
    [SerializeField] AnimationChanger ANC;
    bool ModelPlaced = false;
    bool IsTimerExpired = false;
    private void OnEnable()
    {
        AnimationChanger.OnModelPlaced += OnModelPlaced;
        Clock.ontimerExpired += TimerExpired;
    }

    

    private void OnDisable()
    {
        AnimationChanger.OnModelPlaced -= OnModelPlaced;
        Clock.ontimerExpired -= TimerExpired;
    }
    private void OnModelPlaced(Transform model)
    {
        if (model != transform.root) return;
        ModelPlaced = true;
    }
    public void ChangePose()
    {
        if (ModelPlaced || IsTimerExpired) return;
        ANC.PlayNextPose();
    }
    private void TimerExpired()
    {
        Debug.Log("Expired");
        IsTimerExpired = true;
    }
}
