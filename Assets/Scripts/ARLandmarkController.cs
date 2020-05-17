using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityARInterface;
using Mapbox.Unity.Map;
using Mapbox.Unity.Utilities;
using Mapbox.Unity.Location;
using Mapbox.Utils;
using UnityEngine.UI;

public class ARLandmarkController : MonoBehaviour
{
    //This is the master ARLandmark Script controlling all variables of the landmark
    //Location Variables
    public Vector2d landmarkLatLon;
    public Vector3 landmarkUnityPosition;
    public Transform landmarkUnityTransform;

    //Arland Mark Identity Variables
    public int landmarkARLM_ID;
    public int landmarkLikes;
    public string landmarkName;
    public int ownerID;
    public int viewerID;

    //Graphics Varibles
    public int landmarkThumbID;
    public int landmarkLargeThumbID;
    public float landmarkRotateSpeed;
    public Color landmarkThemeColor;
}
