using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UIElements;

public enum PropType
{ 
    Elevator,
    Train,
    Rope,
    Bicycle
}
public class PropsManager : MonoBehaviour
{
    [SerializeField] Transform ElevatorLgate;
    [SerializeField] Transform ElevatorRgate;
    [SerializeField]float ElevatorCloseDuration=0.5f;
    [SerializeField] PropType ActivePropType;
    [SerializeField] float GAteScaleVAlue = 1;
    [SerializeField] float GatePosValue = 1.62f;
    [SerializeField] AudioClip BicycleChainSound;
    [SerializeField] AudioClip BellSound;
    [SerializeField] AudioClip TrainSound;
    [SerializeField] AudioClip ElevatorSound;
    [SerializeField] AudioClip RopeChainSound;

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
            case PropType.Rope:
                PlayRopeExitAnimation();
                break;

            case PropType.Bicycle:
                PlayBikeAnimation();
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
        AudioManager.Instance.PlaySound("Prop", ElevatorSound, false);
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
            AudioManager.Instance.PlaySound("Prop", TrainSound, false);
        });
        this.enabled = false;
    }
    void PlayPropExitAnimation()
    { 
        Mover mover = transform.GetComponentInParent<Mover>();
        if (mover == null) return;
        mover.MoveAlongAxis();
    }
    void PlayRopeExitAnimation()
    {

        PlayPropExitAnimation();
        AudioManager.Instance.PlaySound("Prop", RopeChainSound, true);
    }
    void PlayBikeAnimation()
    {
        Debug.Log("Here");
        AudioManager.Instance.PlaySound("Prop", BicycleChainSound, false);
        AudioManager.Instance.PlaySound("Spare", BellSound, false);
        Mover mover = transform.GetComponentInParent<Mover>();
        if (mover == null)
        {
            Debug.Log("No mOver found");
            return;
        }
        
        mover.MoveAlongAxis();
    }
    
}
