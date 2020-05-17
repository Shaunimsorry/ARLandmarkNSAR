using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityARInterface;
using Mapbox.Unity.Map;
using Mapbox.Unity.Utilities;
using Mapbox.Unity.Location;
using Mapbox.Utils;
using UnityEngine.UI;

public class LatLongReporter : MonoBehaviour
{
    public ARFocusSquare focusSquareScript;
    public Vector3 focusSquareHitVector;
    public Vector2d realworldPos;
    public Text reportingTxt;
    public LocationProviderFactory locationFactory;



    public void Update()
    {
        focusSquareHitVector = focusSquareScript.focusSquareRayCastHitVector;
        reportingTxt.text = locationFactory.mapManager.WorldToGeoPosition(focusSquareHitVector).ToString();
    }
}
