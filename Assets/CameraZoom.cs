using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UIElements;

public class CameraZoom : MonoBehaviour
{
    [SerializeField]private float zoomSpeed = 0.5f;
    [SerializeField] float MaxZoom = 30;
    [SerializeField] float MinZoom=60;
    [SerializeField] float ZoomBackDuration = 0.5f;
    bool IsActive = true;
    private void OnEnable()
    {
        PuzzleManager.OnPoliceCalled += OnpoliceCalled;
        Clock.ontimerExpired+=OnTimerExpired;
    }

  

    private void OnDisable()
    {
        PuzzleManager.OnPoliceCalled -= OnpoliceCalled;
        Clock.ontimerExpired -= OnTimerExpired;
    }

   

    void Update()
    {
        if (!IsActive) return;
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            // Perform the zooming
            ZoomCamera(deltaMagnitudeDiff * zoomSpeed);
        }
    }


    void ZoomCamera(float deltaMagnitude)
    {
        // Adjust the camera's field of view based on the zoom input
        Camera.main.fieldOfView += deltaMagnitude;
        // Ensure the field of view stays within a valid range (e.g., between 10 and 90 degrees)
        Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, MaxZoom, MinZoom);
    }
    private void OnpoliceCalled(bool WinStatus)
    {

        CameraZoomOut();
    }
    void CameraZoomOut()
    {
        DOTween.To(() => Camera.main.fieldOfView, value => Camera.main.fieldOfView = value, 60, ZoomBackDuration).SetEase(Ease.Linear).OnUpdate
            (
            () =>
            {

            }
            );
        this.enabled = false;
    }
    private void OnTimerExpired()
    {
        CameraZoomOut();
    }
    
}
