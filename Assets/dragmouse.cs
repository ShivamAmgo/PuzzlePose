using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragmouse : MonoBehaviour
{

    private bool isDraging;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    private void OnMouseDrag()
    {

        if (!isDraging) { return; }

        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = new Vector3(worldPosition.x, worldPosition.y, transform.localPosition.z);
    }

    private void OnMouseDown()
    {
        isDraging = true;
    }
}
