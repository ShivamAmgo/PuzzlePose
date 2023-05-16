using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeFitChecker : MonoBehaviour
{
    [SerializeField]List<Sprite> AllPosesSprite;
    [SerializeField] SpriteRenderer SPR;
    [SerializeField] Vector3 ModelPosOffset;
    //[SerializeField] SkinnedMeshRenderer Renderer;
    AnimationChanger ANC;
    Sprite ActiveSprite;
    bool ModelPlaced = false;
    public delegate void DeliverSpritesList(ShapeFitChecker shapeFitChecker, List<Sprite> Poses);
    public static event DeliverSpritesList OnDeliverSpritesList;
    private void Start()
    {
        ActiveSprite = AllPosesSprite[Random.Range(0, AllPosesSprite.Count)];
        SPR.sprite = ActiveSprite;
        OnDeliverSpritesList?.Invoke(this,AllPosesSprite);
    }
    private void FixedUpdate()
    {
        if (!ModelPlaced) return;
        //ANC.SetModelToPos(transform.position + ModelPosOffset);
    }
    public bool CheckCompatibility(AnimationClip Pose)
    {
        Debug.Log("clip " + Pose.name + "spritename "+ActiveSprite.name);
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
        //Debug.Log("entered");
            ANC = other.transform.GetComponent<AnimationChanger>();
        AnimationClip Clip = ANC.GetActivePose();
        if (CheckCompatibility(Clip))
        {
            //ANC.Placed();
            ModelPlaced = true;
            ANC.SetModelToPos(transform.root.position);
            SPR.enabled = false;
            
        }
        
        
    }
}
