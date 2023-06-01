using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeFitChecker : MonoBehaviour
{
    [SerializeField]List<Sprite> AllPosesSprite;
    [SerializeField] SpriteRenderer SPR;
    [SerializeField] Vector3 ModelPosOffset;
    [SerializeField] bool IsSequenceRound = false;
    //[SerializeField] SkinnedMeshRenderer Renderer;
    AnimationChanger ANC;
    Sprite ActiveSprite;
    bool ModelPlaced = false;
    public delegate void DeliverSpritesList(ShapeFitChecker shapeFitChecker, List<Sprite> Poses);
    public static event DeliverSpritesList OnDeliverSpritesList;
    BoxCollider colu;
    private void Start()
    {
        colu = GetComponent<BoxCollider>();
        ActiveSprite = AllPosesSprite[Random.Range(0, AllPosesSprite.Count)];
        SPR.sprite = ActiveSprite;
        OnDeliverSpritesList?.Invoke(this,AllPosesSprite);
        //ActivateSprite(!IsSequenceRound);
    }
    private void FixedUpdate()
    {
        if (!ModelPlaced) return;
        //ANC.SetModelToPos(transform.position + ModelPosOffset);
    }
    public bool CheckCompatibility(AnimationClip Pose)
    {
        //Debug.Log("clip " + Pose.name + "spritename "+ActiveSprite.name);
        if (Pose.name == ActiveSprite.name)
        {
            //SpriteMatched();
            return true;
        }
        else
            return false;
    }
    public void MakeActiveSprite(Sprite S)
    {
        ActiveSprite = S;
        SPR.sprite = S;
    }
    
    private void OnTriggerEnter(Collider other)
    {

        if (other.transform.tag != "Model") return;
        Debug.Log("entered" + ActiveSprite.name);
            ANC = other.transform.GetComponent<AnimationChanger>();
        AnimationClip Clip = ANC.GetActivePose();
        if (CheckCompatibility(Clip))
        {
            //ANC.Placed();
            //ModelPlaced = true;
            ANC.SetModelToPos(transform.root.position);
            SPR.enabled = false;
            if (FindObjectOfType<SequenceManager>())
            {

                return;
            }
            
            
        }
        
        
    }
    public void ActivateSprite(bool activeStatus,Sprite Activesprite)
    {
        colu.enabled = activeStatus;
        SPR.enabled = activeStatus;
        ActiveSprite = Activesprite;
        SPR.sprite = Activesprite;
    }
}
