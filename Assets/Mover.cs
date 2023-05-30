using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    //[SerializeField] PropType Proptype;
    [SerializeField] Vector3 Move_Axis = new Vector3(0, 0, -18);
    [SerializeField] float MoveDuration = 10;
    public delegate void DeliverMoverinfo(Transform Mover);
    public static event DeliverMoverinfo OnDeliverInfo;
    private void Start()
    {
        
    }
    public void MoveAlongAxis()
    {
        OnDeliverInfo.Invoke(transform);
        transform.DOMove(transform.position + Move_Axis,MoveDuration).SetEase(Ease.Linear);

    }
}
