using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerDetector : MonoBehaviour
{
    [SerializeField] AnimationChanger ANC;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag != "Model") return;

        ANC.ModelDetected(other.transform);
    }
}
