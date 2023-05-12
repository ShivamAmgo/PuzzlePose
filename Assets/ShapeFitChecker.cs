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

    private void Start()
    {
        ActiveSprite = AllPosesSprite[Random.Range(0, AllPosesSprite.Count)];
        SPR.sprite = ActiveSprite;
    }
    private void FixedUpdate()
    {
        if (!ModelPlaced) return;
        //ANC.SetModelToPos(transform.position + ModelPosOffset);
    }
    public bool CheckCompatibility(AnimationClip Pose)
    {
        
        if (Pose.name == ActiveSprite.name)
        {
            SpriteMatched();
            return true;
        }
        else
            return false;
    }
    void SpriteMatched()
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {

        if (other.transform.tag != "Model") return;
        Debug.Log("entered");
            ANC = other.transform.GetComponent<AnimationChanger>();
        AnimationClip Clip = ANC.GetActivePose();
        if (CheckCompatibility(Clip))
        {
            //ANC.Placed();
            ModelPlaced = true;
            ANC.SetModelToPos(transform.root.position);
        }
        
        
    }
}
