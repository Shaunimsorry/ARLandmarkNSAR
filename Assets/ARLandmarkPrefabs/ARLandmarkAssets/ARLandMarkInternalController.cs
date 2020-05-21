using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARLandMarkInternalController : MonoBehaviour
{
    public Camera targetCamera;

    //Setup Landmark UpperLogo Assets
    public GameObject landmark_triangleLogo;
    public GameObject landmark_Squarelogo;
    public GameObject landmark_circleLogo;

    public string landmarkLogo;







    // Start is called before the first frame update
    void Start()
    { 
        if(!targetCamera)
        {
            targetCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        }

        landmark_circleLogo.SetActive(false);
        landmark_Squarelogo.SetActive(false);
        landmark_triangleLogo.SetActive(false);
         
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

        //Setup Landmark Type Switcher
        if(landmarkLogo == "Square")
        {
                landmark_circleLogo.SetActive(false);
                landmark_Squarelogo.SetActive(true);
                landmark_triangleLogo.SetActive(false);
        }else
        {
            if(landmarkLogo == "Circle")
            {
                landmark_circleLogo.SetActive(true);
                landmark_Squarelogo.SetActive(false);
                landmark_triangleLogo.SetActive(false);
            }else
            {
                if(landmarkLogo == "Triangle")
                {
                    landmark_circleLogo.SetActive(false);
                    landmark_Squarelogo.SetActive(false);
                    landmark_triangleLogo.SetActive(true);    
                }
            }
        }
    }
}
