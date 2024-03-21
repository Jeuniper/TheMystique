using NodeCanvas.Tasks.Actions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform targetObject;
    public float smoothing = 0.1f;

    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void LateUpdate()
    {
        if (targetObject != null) { ToFollow(); }
        
    }

    void ToFollow()
    {
        if (transform.position != targetObject.position)
        {
            Vector3 targetPos = targetObject.position;
            transform.position = Vector3.Lerp(transform.position, targetPos,smoothing);
        }
        //transform.position = targetObject.position + vector;
    }
}
