using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class bicycle : MonoBehaviour
{
    [SerializeField] float biketravelduration = 2;
    
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
        transform.DOMoveX(pos.position.x,biketravelduration).SetEase(Ease.Linear);
    }
}
