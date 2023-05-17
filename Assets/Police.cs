using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Police : MonoBehaviour
{
    [SerializeField] float WalkDuration = 3;
    Animator animator;
    Vector3 StartRot ;
    private void OnEnable()
    {
        PuzzleManager.OnPoliceCalled += CheckCrime;
    }
    private void OnDisable()
    {
        PuzzleManager.OnPoliceCalled -= CheckCrime;
    }
    

    

    private void Start()
    {
        animator = GetComponent<Animator>();
        StartRot = transform.eulerAngles;
    }
    private void CheckCrime(bool WinStatus)
    {
        animator.SetTrigger("Walk");
        DOVirtual.DelayedCall(WalkDuration, () =>
        {
            animator.SetTrigger("LookAround");
        });
        if (WinStatus)
        {

        }

        else
        {

        }
    }
    public void AfterUturn()
    {
        //Debug.Log("u turn");
        transform.rotation *= Quaternion.Euler(new Vector3(0, 180, 0));
        DOVirtual.DelayedCall(WalkDuration + 1, () => 
        {
            animator.SetTrigger("Idle");
        });
    }


}
