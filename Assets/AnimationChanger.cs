using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;
using Unity.VisualScripting;

public class AnimationChanger : MonoBehaviour
{
    
    [SerializeField] List<AnimationClip> AllPoses;
    [SerializeField] List<Material> AllOutlineMaterial;
    [SerializeField] SkinnedMeshRenderer Renderer;
    [SerializeField] Material MaterialAfterPlaced;
    [SerializeField]List<AnimationClip> AllIdleClips;
    [SerializeField] bool IsCApturing = true;
    [SerializeField] float IdleAnimRandomnessDelay=5;
    
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
    bool IsRoundStarted = false;
    float IdleTimer = 0;

    private void OnEnable()
    {
        Clock.ontimerExpired += TimerExpired;
        PuzzleManager.OnRoundStart += RoundStarted;
    }
    private void OnDisable()
    {
        Clock.ontimerExpired -= TimerExpired;
        PuzzleManager.OnRoundStart -= RoundStarted;
    }

    

    private void Start()
    {
        m_animator = GetComponent<Animator>();
        ChangeIdlePose();
        OnDeliverModelsInfo?.Invoke(this.transform);
    }
    private void Update()
    {
        if (!IsCApturing) return;
        if (Input.GetMouseButtonDown(0) )
        {
            PlayNextPose();
        }
        else if (Input.GetKeyDown(KeyCode.K) )
        {
            // Stateinfo=
            AnimatorClipInfo[] clipinfo = m_animator.GetCurrentAnimatorClipInfo(0);

            OnCaptureImage?.Invoke(clipinfo[0].clip.name);
            Debug.Log("currently pose "+clipinfo[0].clip.name);
        }
        
    }
    private void FixedUpdate()
    {
         if(IsModelPlaced || IsRoundStarted) return;
        
        IdleTimer += Time.deltaTime;
        if (IdleTimer > IdleAnimRandomnessDelay)
        {
            IdleTimer = 0;
            ChangeIdlePose();
        }

    }
    public void BakeModel()
    {
        if (IsCApturing) return;
        ClipCounter = Random.Range(0, AllPoses.Count);
        ActiveClip = AllPoses[ClipCounter];
        m_animator.Play(ActiveClip.name + "");
    }
    void ChangeIdlePose()
    {
        if (AllIdleClips.Count <= 0)
        { 
            BakeModel();
            return;
        }
        IdleAnimRandomnessDelay = Random.Range(5, 9);
        int random = Random.Range(0, AllIdleClips.Count);
        m_animator.Play(AllIdleClips[random].name);
    }
    public void PlayNextPose()
    {
        if (!IsRoundStarted) return;
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
        IsModelPlaced = true;
        DOVirtual.DelayedCall(0.5f, () =>
        {
            transform.DOMove(Pos, 0.3f).SetEase(Ease.Linear).OnComplete(() => 
            {
                Placed();
            });

        });
        //dovi
    }
    private void TimerExpired()
    {
        if (IsModelPlaced) return;
        PlayDefeatAnimations();
    }
    void PlayDefeatAnimations()
    {
        int random = Random.Range(1, 3);
        m_animator.Play("Defeat" + random);
    }
    public AnimationClip GetActivePose()
    {
        return ActiveClip;
    }
    public void Placed()
    {
        //List<Material>renderermaterials=new List<Material>();
        if (AllOutlineMaterial.Count <= 0) 
        {
            Debug.Log("No Outline material");
            return;
        }
       
        Material[] renderermaterials;
        renderermaterials = Renderer.sharedMaterials;
        for(int i=0;i<renderermaterials.Length;i++)
        {
            renderermaterials[i] = AllOutlineMaterial[i];

        }
        Renderer.sharedMaterials = renderermaterials;
        //Debug.Log("count mat " + renderermaterials.Length);
    }
    public void RoundStarted()
    { 
        IsRoundStarted = true;
        //BakeModel();
    }
}
