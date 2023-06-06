using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SuitMan : MonoBehaviour
{
    [SerializeField] float WalkDuration;
    [SerializeField]float ProposeDelay;
    [SerializeField] GameObject Bukkeh;
    Animator animator;
    
    private void OnEnable()
    {
        PuzzleManager.OnPoliceCalled += OnPoliceCall;
        
    }
    private void OnDisable()
    {
        PuzzleManager.OnPoliceCalled -= OnPoliceCall;
    }

    private void OnPoliceCall(bool WinStatus)
    {
        animator.Play("Walking");
        if (WinStatus)
        {
            DOVirtual.DelayedCall(ProposeDelay, () =>
            {
                animator.Play("Kneel");
            });
        }
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.Play("Idle");
    }
    
}
