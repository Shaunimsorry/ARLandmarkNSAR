using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LMI_LandmarkCreateButtonIdentity : MonoBehaviour
{

    public string landmarkLogoShape;
    public string landmarkName;
    public string landmarkBG;
    public MapboxApI mapBoxMasterControllerLink;


    public void SendCaroselDataToMapboxAPI()
    {
        mapBoxMasterControllerLink.CreateCarosellLandMarkAtLocation(landmarkName, landmarkLogoShape, landmarkBG);
    }

}
