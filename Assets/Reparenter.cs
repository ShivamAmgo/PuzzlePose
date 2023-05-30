using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reparenter : MonoBehaviour
{
    Transform Reparent_TO;
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
    }
    
}
