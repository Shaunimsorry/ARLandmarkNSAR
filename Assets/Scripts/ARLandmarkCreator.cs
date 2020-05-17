using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityARInterface;
using Mapbox.Unity.Map;
using Mapbox.Unity.Utilities;
using Mapbox.Unity.Location;
using Mapbox.Utils;
using UnityEngine.UI;

public class ARLandmarkCreator : MonoBehaviour
{
    public GameObject ARLandmarkPrefab;
    public ARFocusSquare focusSquare;
    public LocationProviderFactory locationFactory;


    
    public void createNewLandmark()
    {
        //Convert the pased latlong into unity space
        var LatLonToUnity = locationFactory.mapManager.GeoToWorldPosition(locationFactory.mapManager.WorldToGeoPosition(focusSquare.focusSquareRayCastHitVector),true);
        Vector3 dyna = new Vector3(LatLonToUnity.x,focusSquare.focusSquareRayCastHitVector.y,LatLonToUnity.z);
        var landmarkinstance = GameObject.Instantiate(ARLandmarkPrefab,dyna,transform.rotation);
    }
}
