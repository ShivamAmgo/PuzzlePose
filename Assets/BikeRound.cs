using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeRound : MonoBehaviour
{
    public delegate void CallBike(Transform spawnpos);
    public static event CallBike OnBikeCalled;
    [SerializeField] ShapeFitChecker SFC;
    [SerializeField] float BikeArriveDelay = 2;
    private void OnEnable()
    {
        Mirror.OnMirrorBreak += MirrorBroken;
    }
    private void OnDisable()
    {
        Mirror.OnMirrorBreak -= MirrorBroken;
    }
    private void Start()
    {
        SFC.gameObject.SetActive(false);
    }
    private void MirrorBroken()
    {
        StartCoroutine(bikecallevent());
    }
    IEnumerator bikecallevent()
    {
        yield return new WaitForSeconds(1.5f);
        OnBikeCalled?.Invoke(transform);
        yield return new WaitForSeconds(BikeArriveDelay);
        SFC.gameObject.SetActive(true);
    }

}
