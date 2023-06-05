using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class bicycle : MonoBehaviour
{
    [SerializeField] float biketravelduration = 2;
    [SerializeField] AudioClip CycleMoveSound;
    [SerializeField] AudioClip BellSound;
    
    private void OnEnable()
    {
        BikeRound.OnBikeCalled += Onbicyclecalled;
    }
    private void OnDisable()
    {
        BikeRound.OnBikeCalled -= Onbicyclecalled;
    }

    private void Onbicyclecalled(Transform pos)
    {
        AudioManager.Instance.PlaySound("Prop", CycleMoveSound, false);
        AudioManager.Instance.PlaySound("Spare", BellSound, false);
        transform.DOMoveX(pos.position.x, biketravelduration).SetEase(Ease.Linear).OnComplete(() => 
        
        {
            
        });
    }
}
