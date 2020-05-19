using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Mapbox.Utils;
using Mapbox.Map;
using UnityARInterface;
using Mapbox.Unity.Map;
using Mapbox.Unity.Utilities;
using Mapbox.Unity.Location;
using UnityEngine.UI;


public class MapboxApI : MonoBehaviour
{

    //Interface Into Locationprovider
    public LocationProviderFactory locationProviderFactoryLink;
    public ARFocusSquare userFocusSquare;
    public Vector3 focusSquareRayRayCastHit; 

    //SetupDebuging Texts
    public Text currentLocation;
    public Text currentLookAtVector3;
    public Text currentFeatures;
    public Text userVector2D;
    public Slider detectionSlider;
    public Text sliderValue;
    public Text dynamicFeatureListCount;
    public Text distanceToFeature;  
    public Text fzeroDouble;
    public List<mapboxFeatureClass> dynamicFeatureList;




    //API Details
    public string responseCode;
    public string jsonOutput;
    public string dataset_id = "cka0vudxx13p72smuavafd1um";
    public string access_token = "pk.eyJ1IjoidGFua2J1c3RhIiwiYSI6ImNrN3BhYjdvZTAzbHMza3VmMmhhbGxtc3YifQ.Z8ohtdn7-BsOzeznsXm3EQ";
    public string secret_token = "sk.eyJ1IjoidGFua2J1c3RhIiwiYSI6ImNrYWQ3MjN6ODFqMWMzNHM5NzM3bm40ajgifQ.WEZKnrork6sshSBo1aPcjA";
    public string userName = "tankbusta";
    

    //Setting Up Dyanmic Lists
    public mapboxFeatureSet RetrievedFeatureList;


    //Detection System Initial Stuff
    public double detectionRadius = 0;
    public int nearbyFeatures = 0;
    public Vector2d userV2D;



    //Start!
    public void Start()
    {
        StartCoroutine(listLandmarks());
        StartCoroutine(populateDynamicList(detectionSlider.value));
        //StartCoroutine(createLandmark());

    }

    public void Update()
    {

        //GUI Debugging Things
        focusSquareRayRayCastHit = userFocusSquare.focusSquareRayCastHitVector;
        currentLookAtVector3.text = "Current Look at V3: "+focusSquareRayRayCastHit.ToString();
        userVector2D.text = locationProviderFactoryLink.mapManager.WorldToGeoPosition(focusSquareRayRayCastHit).ToString();
        currentFeatures.text = RetrievedFeatureList.features.Count.ToString();
        sliderValue.text = detectionSlider.value.ToString();
        dynamicFeatureListCount.text = dynamicFeatureList.Count.ToString();

        //Keep Polling the distance to that first feature ?
        distanceToFeature.text = landMarkDistance(RetrievedFeatureList.features[0],focusSquareRayRayCastHit).ToString();
    }

    public float landMarkDistance(mapboxFeatureClass inputFeature, Vector3 LookAtRaycast)
    {
        //This function calculates the distance between the user's focus square and a particular feature
        Vector3 featureVector3;
        Vector2d featureVector2d;

        featureVector2d.x = inputFeature.geometry.coordinates[0];
        featureVector2d.y = inputFeature.geometry.coordinates[1];
        featureVector3 = locationProviderFactoryLink.mapManager.GeoToWorldPosition(featureVector2d);

        return Vector3.Distance(LookAtRaycast,featureVector3);

    }

    // public void populateDynamiclist(float maxDistance)
    // {
    //     dynamicFeatureList.Clear();
    //     //Periodically Populate the list and exclude if the distance is larger than max distance
    //     foreach(mapboxFeatureClass i in RetrievedFeatureList.features)
    //     {
    //         float distance = landMarkDistance(i,focusSquareRayRayCastHit);
    //         if (distance > maxDistance)
    //         {
    //             dynamicFeatureList.Add(i);
    //         }
    //     }
    // }

    public IEnumerator listLandmarks()
    {
        string endpoint_list = "https://api.mapbox.com/datasets/v1/"+userName+"/"+dataset_id+"/"+"features?"+"access_token="+access_token;
        UnityWebRequest www=UnityWebRequest.Get(endpoint_list);
        www.SetRequestHeader("Content-Type", "application/json");
        yield return www.SendWebRequest();
        responseCode = www.responseCode.ToString();
        string downloadeddata = www.downloadHandler.text;
        RetrievedFeatureList = JsonUtility.FromJson<mapboxFeatureSet>(downloadeddata);
        if(www.isNetworkError || www.isHttpError)
        {
            Debug.Log("Error: "+www.error);
        }else
        {
            Debug.Log("Passed "+www.downloadHandler.text);
        }
        
        // foreach (mapboxFeatureClass i in RetrievedFeatureList.features)
        // {
        //     Debug.Log(i.geometry.coordinates[0]);
        // }
    }
    public IEnumerator createLandmark()
    {   
        double x = 67.035213;
        double y = 24.841509;

        string feature_id = "d470b6133258df834ed36fc0c3ec0";

        mapboxFeatureClass testLandMark = new mapboxFeatureClass();

        Properties featureProperties = new Properties();
        Geometry featureGeometry = new Geometry();
        List<double> featurecoordinates = new List<double>();

        testLandMark.properties = featureProperties;
        testLandMark.geometry = featureGeometry;
        testLandMark.geometry.coordinates = featurecoordinates;


        testLandMark.id = feature_id;
        testLandMark.type = "Feature";
        testLandMark.properties.name = "ThePlace";
        testLandMark.geometry.coordinates.Add(x);
        testLandMark.geometry.coordinates.Add(y);
        testLandMark.geometry.type = "Point";

        testLandMark.properties.creator = "Xinz";
        testLandMark.properties.likes = 1112;

        jsonOutput= JsonUtility.ToJson(testLandMark);

        string endpoint_create = "https://api.mapbox.com/datasets/v1/tankbusta/cka0vudxx13p72smuavafd1um/features/"+feature_id+"/?access_token=sk.eyJ1IjoidGFua2J1c3RhIiwiYSI6ImNrYWQ3MjN6ODFqMWMzNHM5NzM3bm40ajgifQ.WEZKnrork6sshSBo1aPcjA";
        UnityWebRequest www=UnityWebRequest.Put(endpoint_create,jsonOutput);
        www.SetRequestHeader("Content-Type", "application/json");
        yield return www.SendWebRequest();
        responseCode = www.responseCode.ToString();
        
        if(www.isNetworkError || www.isHttpError)
        {
            Debug.Log("Error: "+www.error);
        }else
        {
            Debug.Log("Passed "+www.downloadHandler.text);
        }
    }

    public IEnumerator populateDynamicList(float maxDistance)
    {
        Debug.Log("Populating List!");
        dynamicFeatureList.Clear();
        //Periodically Populate the list and exclude if the distance is larger than max distance
        foreach(mapboxFeatureClass i in RetrievedFeatureList.features)
        {
            float distance = landMarkDistance(i,focusSquareRayRayCastHit);
            if (distance < maxDistance)
            {
                dynamicFeatureList.Add(i);
            }
        }
        yield return new WaitForSeconds(5.0f);
        StartCoroutine(populateDynamicList(detectionSlider.value));
    }
}
