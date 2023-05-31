using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class SoccerBall : MonoBehaviour
{
    [SerializeField] Vector3 BallSavePos;
    [SerializeField] Vector3 BallMissPos;
    [SerializeField] float BallTravelDuration = 1;
    [SerializeField] float JumpPower = 1.5f;
    [SerializeField] Vector3 StartPos = new Vector3(0.0439999998f, -0.640999973f, 5.45100021f);
    private void OnEnable()
    {
        PuzzleManager.OnPoliceCalled += CheckGoalSave;
    }
    private void OnDisable()
    {
        PuzzleManager.OnPoliceCalled -= CheckGoalSave;
    }
    private void Start()
    {
        transform.localPosition = StartPos;
    }
    private void CheckGoalSave(bool WinStatus)
    {
        PlayBallAnimation(WinStatus);
    }
    void PlayBallAnimation(bool WinStatus)
    {
        Vector3 FinalBallPos = Vector3.zero;
        if (WinStatus)
        {
           FinalBallPos= BallSavePos;

        }
        else
        {
            FinalBallPos = BallMissPos;
            JumpPower *= 2;
        }
        transform.DOLocalJump(FinalBallPos, JumpPower, 1, BallTravelDuration).SetEase(Ease.Linear).OnComplete(() => 
        {
            DOVirtual.DelayedCall(2, () =>

            {
                CallWin(WinStatus);
            });
        });
    }
    void CallWin(bool winstatus)
    {
        PuzzleManager.Instance.Win(winstatus);
        
    }
}
