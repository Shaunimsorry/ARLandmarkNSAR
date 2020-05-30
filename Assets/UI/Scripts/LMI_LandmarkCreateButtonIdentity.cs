using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LMI_LandmarkCreateButtonIdentity : MonoBehaviour
{

    public string landmarkLogoShape;
    public string landmarkName;
    public string landmarkBackgroundKeyword;
    public Sprite landmarkSpriteAsset;
    public MapboxApI mapBoxMasterControllerLink;


    public void SendCaroselDataToMapboxAPI()
    {
        mapBoxMasterControllerLink.CreateCarosellLandMarkAtLocation(landmarkName, landmarkLogoShape, landmarkBackgroundKeyword);
    }

}
