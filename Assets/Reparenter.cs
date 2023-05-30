using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Reparenter : MonoBehaviour
{
    Transform Reparent_TO;
    [SerializeField] Vector3 LocalPositionset;
    private void OnEnable()
    {
        Mover.OnDeliverInfo += Mover_OnDeliverInfo;
    }
    private void OnDisable()
    {
        Mover.OnDeliverInfo -= Mover_OnDeliverInfo;
    }

    private void Mover_OnDeliverInfo(Transform Mover)
    {
        transform.parent = Mover;
        transform.DOLocalMove(LocalPositionset,0.2f).SetEase(Ease.Linear);
    }
    
}
