using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARLandMarkInternalController : MonoBehaviour
{
    public Camera targetCamera;
    // Start is called before the first frame update
    void Start()
    { 
        if(!targetCamera)
        {
            targetCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        } 
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 landmarkVector = transform.position;
        Vector3 cameraVector = targetCamera.transform.position;
        Vector3 relativepos = cameraVector - landmarkVector;
        relativepos.y = 0;
        Quaternion deducedRotation = Quaternion.LookRotation(relativepos);;
        transform.rotation = deducedRotation;
    }
}
