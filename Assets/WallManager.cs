
using System.Collections;
using System.Collections.Generic;

using System.Threading;

using UnityEngine;
using DG.Tweening;

public class WallManager : MonoBehaviour
{
    [SerializeField]List<Transform> SpawnPoints;
    [SerializeField] float ModelsPosition_z_Offset = 0.88f;
    [SerializeField] Transform SpawnPointTransformForOffset;
    [SerializeField] bool IsSequenceRound = false;
     int PoseCounts = 0;
    List<Transform> AllModels=new List<Transform>();
    Dictionary<ShapeFitChecker, List<Sprite>> AllModelsAndPoses=new Dictionary<ShapeFitChecker, List<Sprite>>();
    public delegate void AllModelsPlace();
    public delegate void DelivermodelsOffset(float offset,Transform From);
    public static event DelivermodelsOffset DelivermodelsOffsetDelegate;
    public static event AllModelsPlace OnAllModelsPlace;
    private void OnEnable()
    {
        ShapeFitChecker.OnDeliverSpritesList += StoreModelsAndPoses;
        AnimationChanger.OnDeliverModelsInfo += RecieveModels;
        AnimationChanger.OnModelPlaced += ModelPlaced;
    }

   

    private void OnDisable()
    {
        ShapeFitChecker.OnDeliverSpritesList -= StoreModelsAndPoses;
        AnimationChanger.OnDeliverModelsInfo -= RecieveModels;
        AnimationChanger.OnModelPlaced -= ModelPlaced;
    }
    void Start()
    {
        DOVirtual.DelayedCall(1, () => 
        {
            DelivermodelsOffsetDelegate?.Invoke(ModelsPosition_z_Offset, SpawnPointTransformForOffset);
            //DOTween.KillAll();
        });
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void ModelPlaced(Transform model)
    {
        CheckWin(model);
    }
    void BakePoses(ShapeFitChecker shapeFitChecker)
    {
        shapeFitChecker.transform.root.position = SpawnPoints[PoseCounts].position;
        PoseCounts++;
    }
    void PickRandomPose()
    { 
        
    }
    private void StoreModelsAndPoses(ShapeFitChecker shapeFitChecker, List<Sprite> Poses)
    {
        if (AllModelsAndPoses.ContainsKey(shapeFitChecker)) return;
        AllModelsAndPoses.Add(shapeFitChecker, Poses);
        
        int randomnum = Random.Range(0, Poses.Count);
        Sprite s = Poses[randomnum];
        
        shapeFitChecker.transform.GetComponent<SpriteRenderer>().sprite = s;
        shapeFitChecker.MakeActiveSprite(s);
        BakePoses(shapeFitChecker);
    }
    void RecieveModels(Transform t)
    {
        if (AllModels.Contains(t)) return;
        AllModels.Add(t);
        
    }
    public void CheckWin(Transform t)
    {
        
        if (!AllModels.Contains(t)) return;
        if (IsSequenceRound)
        {
            if (SequenceManager.Instance.CheckSequenceActiveStatus())
            {
                PuzzleManager.Instance.CallPolice(true);
                return;
            } 
        }
        AllModels.Remove(t);
        
        if (AllModels.Count <= 0)
        {
            OnAllModelsPlace?.Invoke();
            PuzzleManager.Instance.CallPolice(true);//winstatus true
        }

    }
}
