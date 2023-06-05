using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class Police : MonoBehaviour
{
    [SerializeField] float WalkDuration = 3;
    [SerializeField] AudioClip[] HmmmSounds;
    [SerializeField] AudioClip WalkSound;
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
        AudioClip hmm=GetRandomSound();
        animator.SetTrigger("Walk");
        //AudioManager.Instance.PlaySound("Police", WalkSound, false);
        DOVirtual.DelayedCall(WalkDuration, () =>
        {
            animator.SetTrigger("LookAround");
            AudioManager.Instance.PlaySound("Spare", hmm,false);
        });
        if (WinStatus)
        {

        }

        else
        {
            animator.SetTrigger("Yell");
        }
    }
    public void RotateTowardsTheives()
    {
        transform.DORotate(Vector3.forward, 0.25f).SetEase(Ease.Linear);
    }
    public void AfterUturn()//animation event
    {
        
        DOVirtual.DelayedCall(0.15f, () =>
        {
            Debug.Log("Uturned");
            transform.DORotate((new Vector3(0, 90, 0)), 0.25f).SetEase(Ease.Linear);
        });
        
        DOVirtual.DelayedCall(WalkDuration + 2, () => 
        {
            animator.SetTrigger("Idle");
        });
    }
    AudioClip GetRandomSound()
    {
        int random = 0;
        random = UnityEngine.Random.Range(0, HmmmSounds.Count());
            
        return HmmmSounds[random];
    }
    public void PlayWalkSound()
    {
        AudioManager.Instance.PlaySound("Police", WalkSound, false);
    }

}
