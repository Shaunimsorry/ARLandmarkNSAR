using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARLandMarkInternalController : MonoBehaviour
{
    public Camera targetCamera;
    // Start is called before the first frame update
    void Start()
    { 
        // if(!targetCamera)
        // {
        //     targetCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        // } 
    }

    // Update is called once per frame
    void Update()
    {
        //transform.LookAt(targetCamera.transform, transform.up);
    }
}
