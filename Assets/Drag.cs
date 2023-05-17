using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Clock;

public class Drag : MonoBehaviour
{
    bool CanDrag = false;
    bool ModelPlaced = false;
    Vector3 Startpos;
    [SerializeField] float dragSpeed = 1;
    [SerializeField] AnimationChanger ANC;
    bool IsTimerExpired = false;
    bool IsRoundStarted = false;
    private void OnEnable()
    {
        AnimationChanger.OnModelPlaced += OnModelPlaced;
        Clock.ontimerExpired += TimerExpired;
        PuzzleManager.OnRoundStart += OnRoundStarted;
        
    }

    private void OnDisable()
    {
        AnimationChanger.OnModelPlaced -= OnModelPlaced;
        Clock.ontimerExpired -= TimerExpired;
        PuzzleManager.OnRoundStart -= OnRoundStarted;
    }

    private void TimerExpired()
    {
        IsTimerExpired = true; 
        Reset();
    }

    private void OnModelPlaced(Transform model)
    {
        if (model != transform.root) return;
        ModelPlaced = true;
    }
    private void Start()
    {
        Startpos = transform.root.position;
    }
    public void DragTo(Vector3 Pos)
    {
        //Debug.Log("s"+Pos);
        transform.position+=Pos*dragSpeed*Time.deltaTime;
    }
    private void FixedUpdate()
    {
        if (ModelPlaced || IsTimerExpired) return;
        if (CanDrag)
        { 
            
        }
    }
    public void Reset()
    {
        transform.root. position = Startpos;
    }
    private void OnMouseDrag()
    {
        if (ModelPlaced || IsTimerExpired || !IsRoundStarted) return;
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.root.position = new Vector3(worldPosition.x, worldPosition.y, Startpos.z);
    }
    private void OnMouseUp()
    {
        if (ModelPlaced || IsTimerExpired || !IsRoundStarted) return;
        Reset();
        ANC.PlayNextPose();
    }
    private void OnMouseDown()
    {
        
    }
    void OnRoundStarted()
    {
        IsRoundStarted = true;
    }
}
