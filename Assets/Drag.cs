using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;


public class Drag : MonoBehaviour
{
    bool CanDrag = false;
    bool ModelPlaced = false;
    Vector3 Startpos;
    [SerializeField] float dragSpeed = 1;
    [SerializeField] AnimationChanger ANC;
    bool IsTimerExpired = false;
    bool IsRoundStarted = false;
    Vector3 offset = Vector3.zero;
    float distanceFromCamera;
    Plane plane;
    private GameObject planeVisualization;
    private void OnEnable()
    {
        AnimationChanger.OnModelPlaced += OnModelPlaced;
        Clock.ontimerExpired += TimerExpired;
        PuzzleManager.OnRoundStart += OnRoundStarted;
        WallManager.DelivermodelsOffsetDelegate += RecieveOffset;
        
    }

    

    private void OnDisable()
    {
        AnimationChanger.OnModelPlaced -= OnModelPlaced;
        Clock.ontimerExpired -= TimerExpired;
        PuzzleManager.OnRoundStart -= OnRoundStarted;
        WallManager.DelivermodelsOffsetDelegate -= RecieveOffset;
    }

    private void TimerExpired()
    {
        IsTimerExpired = true; 
        Reset();
    }

    private void OnModelPlaced(Transform model)
    {
        if (model != transform.root) return;
        ModelPlaced = true;
    }
    private void Start()
    {
        Startpos = transform.position;
        /*
        plane = new Plane(Vector3.up, transform.position);

        planeVisualization = GameObject.CreatePrimitive(PrimitiveType.Quad);
        planeVisualization.transform.localScale = new Vector3(10f, 10f, 1f);
        planeVisualization.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
        planeVisualization.GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, 0.5f);*/
    }
    public void DragTo(Vector3 Pos)
    {
        //Debug.Log("s"+Pos);
        transform.position+=Pos*dragSpeed*Time.deltaTime;
    }
    private void FixedUpdate()
    {
        if (ModelPlaced || IsTimerExpired) return;
        if (CanDrag)
        { 
            
        }
    }
    private void RecieveOffset(float offset, Transform From)
    {
        Startpos=new Vector3(transform.position.x,transform.position.y,From.position.z- offset);

        
       // Offest_Z = offset;
    }
    public void Reset()
    {
        transform.position = Startpos;
    }
    private void OnMouseDrag()
    {
        if (ModelPlaced || IsTimerExpired || !IsRoundStarted) return;
        
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.localPosition = new Vector3(worldPosition.x, worldPosition.y, transform.localPosition.z);
        Debug.Log(transform.position+" = world " + worldPosition);
        // Get the current mouse position in screen coordinates

        /*
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Perform a ray-plane intersection with the defined plane
        if (plane.Raycast(ray, out float distance))
        {
            // Get the intersection point
            Vector3 intersectionPoint = ray.GetPoint(distance);

            // Update the object's position to the intersection point
            transform.position = intersectionPoint;
        }
        planeVisualization.transform.position = new Vector3(transform.position.x, planeVisualization.transform.position.y, transform.position.z);*/
    }
    private void OnMouseUp()
    {
        if (ModelPlaced || IsTimerExpired || !IsRoundStarted) return;
        Reset();
        ANC.PlayNextPose();
    }
    private void OnMouseDown()
    {
        
    }
    void OnRoundStarted()
    {
        transform.position = Startpos;
        IsRoundStarted = true;
    }
}
