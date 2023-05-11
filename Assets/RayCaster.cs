using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
public enum TouchInputState
{ 
    Idle,
    Moving
}
public class RayCaster : MonoBehaviour
{
    [SerializeField]LayerMask RayMask;
    [SerializeField] float PoseChangeFrequency=0.5f;
    [SerializeField] float MaxGrabTimer = 0.5f;
    TouchInputState TouchState;
    RaycastHit rayhit;
    Touch touch;
    bool CanRaycast = true;
    Drag ActiveModelToDrag;
    float GrabTimer = 0;
    private void Start()
    {
        //RayMask = LayerMask.GetMask("Models");

    }
    private void Update()
    {

        if (Input.touchCount <= 0) return;
        touch = Input.GetTouch(0);
        if (TouchState == TouchInputState.Idle)
        {
            Idle();
        }
        else if (TouchState == TouchInputState.Moving)
        {
            Move();
        }
        
    }
    void Idle()
    {

        if (touch.tapCount > 0 && touch.phase != TouchPhase.Moved && touch.phase != TouchPhase.Stationary && touch.phase!=TouchPhase.Began)
        {
            if (!CanRaycast) return;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out rayhit, 100, RayMask))
            {

                RayDetector RD = rayhit.collider.transform.GetComponent<RayDetector>();
                if (RD == null)
                {
                    Debug.Log("null" + rayhit.transform.name);
                    return;
                }
                CanRaycast = false;
                RD.ChangePose();
                StartCoroutine(SetCanRaycast());
               // Debug.Log("Tapped" + RD.name);
            }
        }
        else if(touch.phase==TouchPhase.Stationary && touch.phase!=TouchPhase.Moved)
        {
            GrabTimer += Time.deltaTime;
            if (GrabTimer >= MaxGrabTimer)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out rayhit, 100, RayMask))
                {
                    

                    Drag drag = rayhit.transform.GetComponent<Drag>();
                    if (drag == null)
                    {
                        Debug.Log("null" + rayhit.transform.name);
                        return;
                    }
                    ActiveModelToDrag = drag;
                   
                }
            }

        }
        if (touch.phase == TouchPhase.Ended)
        {
            GrabTimer = 0;
            if (ActiveModelToDrag != null)
            {
                ActiveModelToDrag.Reset();
                ActiveModelToDrag = null;
            }
            

        }
        if (touch.phase == TouchPhase.Moved)
        {
            GrabTimer = 0;
            float Xmove = touch.deltaPosition.x;
            float Ymove = touch.deltaPosition.y;
            if (ActiveModelToDrag == null) return;
            //ActiveModelToDrag.DragTo(new Vector3(Xmove,Ymove,0));
        }
        
    }
   
    void Move()
    { 
        
    }
    IEnumerator SetCanRaycast()
    {
        yield return new WaitForSeconds(PoseChangeFrequency);
        CanRaycast = true;
    }
}
