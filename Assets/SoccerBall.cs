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
    [SerializeField] List<Vector3> BallSavePoses;
    [SerializeField] bool MirrorBreakOnStart = false;
    [SerializeField] float BallRebondForce = 5;
    int BallSaveposCounter = 0;
    Rigidbody RB;
    Tween ActiveTween;
    private void OnEnable()
    {
        PuzzleManager.OnPoliceCalled += CheckGoalSave;
        Mirror.OnMirrorBreak += OnMirrorBroke;
        PuzzleManager.OnRoundStart += RoundStarted;
    }

   

    private void OnDisable()
    {
        PuzzleManager.OnPoliceCalled -= CheckGoalSave;
        Mirror.OnMirrorBreak -= OnMirrorBroke;
        PuzzleManager.OnRoundStart -= RoundStarted;
    }
    private void Start()
    {
        RB=GetComponent<Rigidbody>();
        transform.localPosition = StartPos;
      
    }
    private void CheckGoalSave(bool WinStatus)
    {
        PlayBallAnimation(WinStatus);
    }
    private void RoundStarted()
    {
        if (MirrorBreakOnStart)
        {
            DOVirtual.DelayedCall(1, () =>
            {
                PlayBallAnimation(true);
            });
        }
    }
    private void OnMirrorBroke()
    {
        ActiveTween.Kill();
        if (MirrorBreakOnStart)
        {
            RB.isKinematic = false;
            RB.AddForce(-BallRebondForce * Vector3.forward, ForceMode.Impulse);
            //return;
        }
    }
    void PlayBallAnimation(bool WinStatus)
    {
        Vector3 FinalBallPos = Vector3.zero;
        if (WinStatus)
        {
            FinalBallPos = BallSavePoses[BallSaveposCounter];
            BallSaveposCounter++;

        }
        else
        {
            FinalBallPos = BallMissPos;
            JumpPower *= 2;
        }
        if (!WinStatus && MirrorBreakOnStart) return;
        ActiveTween = transform.DOLocalJump(FinalBallPos, JumpPower, 1, BallTravelDuration).SetEase(Ease.Linear).OnComplete(() => 
        {
            if (MirrorBreakOnStart) return;
            DOVirtual.DelayedCall(2, () =>

            {
                
                CallWin(WinStatus);
                Reset();
            });
        });
    }
    void CallWin(bool winstatus)
    {
        PuzzleManager.Instance.Win(winstatus);
        
    }
    private void Reset()
    {
        transform.localPosition = StartPos;
    }
    
}
