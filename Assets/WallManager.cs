
using System.Collections;
using System.Collections.Generic;

using System.Threading;

using UnityEngine;

public class WallManager : MonoBehaviour
{
    [SerializeField]List<Transform> SpawnPoints;
     int PoseCounts = 0;
    [SerializeField] GameObject AllModels;
    Dictionary<ShapeFitChecker, List<Sprite>> AllModelsAndPoses=new Dictionary<ShapeFitChecker, List<Sprite>>();
    private void OnEnable()
    {
        ShapeFitChecker.OnDeliverSpritesList += StoreModelsAndPoses;
    }

   

    private void OnDisable()
    {
        ShapeFitChecker.OnDeliverSpritesList -= StoreModelsAndPoses;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
