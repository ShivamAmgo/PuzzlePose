using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceManager : MonoBehaviour
{
    public static SequenceManager Instance { get; private set; }
    [SerializeField] List<Sprite> SequencePoses;
    [SerializeField] SpriteRenderer ModelPoseSPR;
    [SerializeField] Transform[] Spawnpoints;
    [SerializeField] Drag Modeldrag;
    int sequencePoseCounter = 0;
    bool SequenceActive = true;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        
    }
    private void Start()
    {
        ModelPoseSPR.sprite = SequencePoses[sequencePoseCounter];
    }
    public bool CheckSequenceActiveStatus()
    {
        return SequenceActive;
    }
    public void NextSequencePose()
    { 
        sequencePoseCounter++;
        if (sequencePoseCounter >= SequencePoses.Count)
        {
            SequenceActive = false;
            PuzzleManager.Instance.Win(true);
            return;
        }
        ModelPoseSPR.transform.GetComponent<ShapeFitChecker>().ActivateSprite(true, SequencePoses[sequencePoseCounter]);
        //ModelPoseSPR.sprite= SequencePoses[sequencePoseCounter];
        ModelPoseSPR.transform.root.position = Spawnpoints[sequencePoseCounter].position;
        Modeldrag.RefreshModel();
    }
}
