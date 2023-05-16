using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class AnimationChanger : MonoBehaviour
{
    
    [SerializeField] List<AnimationClip> AllPoses;
    [SerializeField] SkinnedMeshRenderer Renderer;
    [SerializeField] Material MaterialAfterPlaced;
    
    //[SerializeField] Collider BoxColliderCheck;
    int ClipCounter;
    Animator m_animator;
    
    AnimatorStateInfo Stateinfo;
    AnimationClip ActiveClip;
    [SerializeField] bool InEditor = true;
    public delegate void CaptureImage(string ClipName);
    public static event CaptureImage OnCaptureImage;

    public delegate void ModelPlaced(Transform model);
    public static event ModelPlaced OnModelPlaced;

    public delegate void DeliverModelsInfo(Transform Models);
    public static event DeliverModelsInfo OnDeliverModelsInfo;
    bool IsModelPlaced = false;
    private void Start()
    {
        m_animator = GetComponent<Animator>();
        ClipCounter = Random.Range(0, AllPoses.Count);
        ActiveClip = AllPoses[ClipCounter];
        m_animator.Play(ActiveClip.name + "");
        OnDeliverModelsInfo?.Invoke(this.transform);
    }
    private void Update()
    {/*
        if (Input.GetMouseButtonDown(0) && !InEditor)
        {
            PlayNextPose();
        }
        else */if (Input.GetKeyDown(KeyCode.K))
        {
            // Stateinfo=
            AnimatorClipInfo[] clipinfo = m_animator.GetCurrentAnimatorClipInfo(0);

            OnCaptureImage?.Invoke(clipinfo[0].clip.name);
            Debug.Log("currently pose "+clipinfo[0].clip.name);
        }
        
    }
    private void FixedUpdate()
    {
        if (!IsModelPlaced) return;

    }
    public void PlayNextPose()
    {
        ClipCounter++;
        
        if (ClipCounter >= AllPoses.Count)
        {
            ClipCounter = 0;
        }
        //Debug.Log("" + ClipCounter);
        m_animator.Play(AllPoses[ClipCounter].name+"");
        ActiveClip = AllPoses[ClipCounter];
        //Debug.Log(AllPoses[ClipCounter].name + " counter " + ClipCounter);
    }
    public void ModelDetected(Transform other)
    {
         
    }

    public void SetModelToPos(Vector3 Pos)
    {
        Debug.Log("Placed");
        OnModelPlaced?.Invoke(this.transform);

        DOVirtual.DelayedCall(0.5f, () =>
        {
            transform.DOMove(Pos, 0.3f).SetEase(Ease.Linear).OnComplete(() => 
            {
                Placed();
            });

        });
        //dovi
    }
    public AnimationClip GetActivePose()
    {
        return ActiveClip;
    }
    public void Placed()
    {
        
        Renderer.material = MaterialAfterPlaced;
    }

}
