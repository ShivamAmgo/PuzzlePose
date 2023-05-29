using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UIElements;

public enum PropType
{ 
    Elevator,
    Train
}
public class PropsManager : MonoBehaviour
{
    [SerializeField] Transform ElevatorLgate;
    [SerializeField] Transform ElevatorRgate;
    [SerializeField]float ElevatorCloseDuration=0.5f;
    [SerializeField] PropType ActivePropType;
    [SerializeField] float GAteScaleVAlue = 1;
    [SerializeField] float GatePosValue = 1.62f;
    
    private void OnEnable()
    {
       PuzzleManager.OnPoliceCalled += OnAllPolicePlaced;
        Clock.ontimerExpired += OnTimerExpired;
    }

  

    private void OnDisable()
    {
        PuzzleManager.OnPoliceCalled -= OnAllPolicePlaced;
        Clock.ontimerExpired -= OnTimerExpired;
    }

    private void OnAllPolicePlaced(bool WInStatus)
    {
        if(WInStatus)
        PropCheck(ActivePropType);
    }
    void PropCheck(PropType Proptype)
    {
        switch (Proptype) 
        {
            case PropType.Elevator:
                PlayGAteAnimation();
                break;
                
            case PropType.Train:
                PlayTrainGateAnimation();
                break;
        }
    }
    private void OnTimerExpired()
    {
       // if (ActivePropType == PropType.Train)
            //PlayTrainGateAnimation();
    }
    void PlayGAteAnimation()
    {
        float scale=ElevatorLgate.localScale.x;
        DOTween.To(() => scale, value => scale = value, GAteScaleVAlue, ElevatorCloseDuration).SetEase(Ease.Linear).OnUpdate
            (
                ()=>
                { 
                    ElevatorLgate.localScale=new Vector3(scale, ElevatorLgate.localScale.y,ElevatorLgate.localScale.z);
                    ElevatorRgate.localScale = new Vector3(scale, ElevatorRgate.localScale.y, ElevatorRgate.localScale.z);
                }
            );
    }
    void PlayTrainGateAnimation()
    {
        
        ElevatorLgate.DOLocalMoveX(-GatePosValue,ElevatorCloseDuration).SetEase(Ease.Linear);
        ElevatorRgate.DOLocalMoveX(GatePosValue,ElevatorCloseDuration).SetEase(Ease.Linear);
        DOVirtual.DelayedCall(2, () =>
        {
            PlayPropExitAnimation();
        });
        this.enabled = false;
    }
    void PlayPropExitAnimation()
    { 
        Mover mover = transform.GetComponentInParent<Mover>();
        if (mover == null) return;
        mover.MoveAlongAxis();
    }
}
