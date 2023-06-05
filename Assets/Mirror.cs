using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{
    [SerializeField] AudioClip MirrorBreakSound;
   public delegate void MirrorBreak();

   public static event MirrorBreak OnMirrorBreak;

   public void OnBreakMirror()
   {
        AudioManager.Instance.PlaySound("Prop", MirrorBreakSound, false);
        OnMirrorBreak?.Invoke();
   }
}
