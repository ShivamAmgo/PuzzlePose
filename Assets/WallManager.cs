
using System.Collections;
using System.Collections.Generic;

using System.Threading;

using UnityEngine;

public class WallManager : MonoBehaviour
{
    [SerializeField]List<Transform> SpawnPoints;
    
     int PoseCounts = 0;
    List<Transform> AllModels=new List<Transform>();
    Dictionary<ShapeFitChecker, List<Sprite>> AllModelsAndPoses=new Dictionary<ShapeFitChecker, List<Sprite>>();
    public delegate void AllModelsPlace();
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
        AllModels.Remove(t);
        if (AllModels.Count <= 0)
        {
            OnAllModelsPlace?.Invoke();
            PuzzleManager.Instance.CallPolice(true);//winstatus true
        }

    }
}
