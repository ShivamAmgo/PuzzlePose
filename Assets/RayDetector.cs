using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayDetector : MonoBehaviour
{
    [SerializeField] AnimationChanger ANC;
    bool ModelPlaced = false;
    private void OnEnable()
    {
        AnimationChanger.OnModelPlaced += OnModelPlaced; 
    }

    

    private void OnDisable()
    {
        AnimationChanger.OnModelPlaced -= OnModelPlaced;
    }
    private void OnModelPlaced(Transform model)
    {
        if (model != transform.root) return;
        ModelPlaced = true;
    }
    public void ChangePose()
    {
        if (ModelPlaced) return;
        ANC.PlayNextPose();
    }
}
