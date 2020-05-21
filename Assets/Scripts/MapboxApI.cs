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
    public float landmarkSpawnDistance = 100.0f; 

    public Text UpdateInfoDebug;
    public string hudDebug00;
    public List<mapboxFeatureClass> dynamicFeatureList;

    //LandMark Spawning Variables
    public InputField TXTInput_LandMarkName;
    public Vector3 landmarkScale;
    public GameObject landMarkPrefab;
    //The Landmark the user is looking at (deduced via raycasting)
    public GameObject userLookLandMark;


    //API Details
    public string responseCode;
    public string jsonOutput;
    public string dataset_id = "cka0vudxx13p72smuavafd1um";
    public string access_token = "pk.eyJ1IjoidGFua2J1c3RhIiwiYSI6ImNrN3BhYjdvZTAzbHMza3VmMmhhbGxtc3YifQ.Z8ohtdn7-BsOzeznsXm3EQ";
    public string secret_token = "sk.eyJ1IjoidGFua2J1c3RhIiwiYSI6ImNrYWQ3MjN6ODFqMWMzNHM5NzM3bm40ajgifQ.WEZKnrork6sshSBo1aPcjA";
    public string userName = "tankbusta";
    

    //Setting Up Dyanmic Lists
    public mapboxFeatureSet RetrievedFeatureList;

    //Building Live Landmark Spawning
    public List<GameObject> LiveLandMarks;



    //Start!
    public void Start()
    {
        //Activate The Mapbox API and Pulldown all the data
        StartCoroutine(listLandmarks());
        //Populate The Dynamic List With Landmarks 100 Clicks From GPS every 5 Seconds
        StartCoroutine(populateDynamicList(landmarkSpawnDistance));
        //Deploy and manage all landmarks from the lists
    }

    public void Update()
    {

        //Making the GUi Simpler
        string lookatV3 = userFocusSquare.focusSquareRayCastHitVector.ToString();
        string lookatV2 = locationProviderFactoryLink.mapManager.WorldToGeoPosition(userFocusSquare.focusSquareRayCastHitVector).ToString();
        string totalFeatures = RetrievedFeatureList.features.Count.ToString();
        string dynamicFeatures = dynamicFeatureList.Count.ToString();
        string livelandmarks = LiveLandMarks.Count.ToString();
        UpdateInfoDebug.text = "LAV3: "+lookatV3+"\n"+"LAV2: "+lookatV2+"\n"+"TF: "+totalFeatures+"\n"+"DF: "+dynamicFeatures+"\n"+"LL: "+livelandmarks+"\n"+"HUD Debug00: "+hudDebug00;

    }




    public void CreateNewLandMarkAtLocation()
    {
        Vector2d landmarkLocation = locationProviderFactoryLink.mapManager.WorldToGeoPosition(userFocusSquare.focusSquareRayCastHitVector);
        Vector3 landmarkDeploylocation = userFocusSquare.focusSquareRayCastHitVector;
        float landmarkHeight = landmarkDeploylocation.y;
        string feature_id = generateFeatureID();
        string LandMarkName = TXTInput_LandMarkName.text;
        StartCoroutine(createLandmark(landmarkLocation,LandMarkName,feature_id,landmarkHeight));
        GameObject createdLandmark = GameObject.Instantiate(landMarkPrefab,landmarkDeploylocation,transform.rotation);
        createdLandmark.name = "Landmark_"+feature_id.ToString();
        createdLandmark.transform.localScale = landmarkScale;
        
        //Add to LiveList
        LiveLandMarks.Add(createdLandmark);

    }

    public void SpawnExistingLandMarkIntoScene(string feature_id, mapboxFeatureClass feature)
    {
        Vector2d existingLandMarkLocation;
        existingLandMarkLocation.y = feature.geometry.coordinates[0];
        existingLandMarkLocation.x = feature.geometry.coordinates[1];
        Vector3 existingLandMarkUnityLocation = locationProviderFactoryLink.mapManager.GeoToWorldPosition(existingLandMarkLocation);

        if(userFocusSquare.focusSquareRayCastHitVector !=null)
        {
            existingLandMarkUnityLocation.y = userFocusSquare.focusSquareRayCastHitVector.y;
        }
        
        GameObject existingLandMark = GameObject.Instantiate(landMarkPrefab,existingLandMarkUnityLocation, landMarkPrefab.transform.rotation);
        existingLandMark.name = "Landmark_"+feature_id.ToString();
        existingLandMark.transform.localScale = landmarkScale;

        //Add to LiveList
        LiveLandMarks.Add(existingLandMark);
    }

    public float landMarkDistance(mapboxFeatureClass inputFeature, Vector3 LookAtRaycast)
    {
        //This function calculates the distance between the user's focus square and a particular feature
        Vector3 featureVector3;
        Vector2d featureVector2d;

        //Need to check the latlon or lonlat here
        //Appears That X is Lat and Y is Lon
        //JSON 0 is Lon and 1 is Lat
        //Still to be confirmed
        featureVector2d.x = inputFeature.geometry.coordinates[1];
        featureVector2d.y = inputFeature.geometry.coordinates[0];
        featureVector3 = locationProviderFactoryLink.mapManager.GeoToWorldPosition(featureVector2d);

        return Vector3.Distance(LookAtRaycast,featureVector3);

    }
    public string generateFeatureID()
    {
        string generatedFeatureID = "";
        const string glyphs = "abcdefghijklmnopqrstuvwxyz123456789";

        //added one more here to make it 32
        //string referenceMapBoxString = "1d470b6133258df834ed36fc0c3ec0";
        for(int i = 0; i<32; i++)
        {
            generatedFeatureID += glyphs[Random.Range(0,glyphs.Length)];
        }
        return generatedFeatureID;
    }
    public IEnumerator listLandmarks()
    {
        Debug.Log("Listing Landmarks");
        string endpoint_list = "https://api.mapbox.com/datasets/v1/"+userName+"/"+dataset_id+"/"+"features?"+"access_token="+access_token;
        UnityWebRequest www=UnityWebRequest.Get(endpoint_list);
        www.SetRequestHeader("Content-Type", "application/json");
        yield return www.SendWebRequest();
        responseCode = www.responseCode.ToString();
        string downloadeddata = www.downloadHandler.text;
        Debug.Log(downloadeddata);
        RetrievedFeatureList = JsonUtility.FromJson<mapboxFeatureSet>(downloadeddata);
        if(www.isNetworkError || www.isHttpError)
        {
            Debug.Log("Error: "+www.error);
        }else
        {
            Debug.Log("Passed "+www.downloadHandler.text);
        }
    }
    public IEnumerator createLandmark(Vector2d landmarkLocation, string LandMarkName, string feature_id, float landmarkHeight)
    {   
        //Prepare Dynamic Placeholders
        mapboxFeatureClass testLandMark = new mapboxFeatureClass();
        Properties featureProperties = new Properties();
        Geometry featureGeometry = new Geometry();
        List<double> featurecoordinates = new List<double>();
        testLandMark.properties = featureProperties;
        testLandMark.geometry = featureGeometry;
        testLandMark.geometry.coordinates = featurecoordinates;
        var userFocusVector2D = locationProviderFactoryLink.mapManager.WorldToGeoPosition(userFocusSquare.focusSquareRayCastHitVector);

        //Fill in the landmark Data
        testLandMark.id = feature_id;
        testLandMark.type = "Feature";
        testLandMark.properties.name = LandMarkName;
        testLandMark.geometry.coordinates.Add(landmarkLocation.y);
        testLandMark.geometry.coordinates.Add(landmarkLocation.x);
        testLandMark.geometry.type = "Point";
        testLandMark.properties.creator = "Xinz";
        testLandMark.properties.likes = 1112;
        testLandMark.properties.landmarkID = feature_id;

        //Add the to spawn lists
        dynamicFeatureList.Add(testLandMark);

        //Final JSON Conversion
        jsonOutput= JsonUtility.ToJson(testLandMark);

        //Start Sending the landmark to MBApi
        string endpoint_create = "https://api.mapbox.com/datasets/v1/tankbusta/"+dataset_id+"/features/"+feature_id+"/?access_token="+secret_token;
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
        //Periodically Populate the list and exclude if the distance is larger than max distance
        //This should eventually become a city or area list << TODO
        //So the larger ideas is 
        foreach(mapboxFeatureClass i in RetrievedFeatureList.features)
        {
            Debug.Log("Checking Max D");
            float distance = landMarkDistance(i,userFocusSquare.focusSquareRayCastHitVector);
            if (distance < maxDistance)
            {
                Debug.Log("Found Landmarks Undermax D");
                //Check if the feature is live or in the dynaamic list
                if(!dynamicFeatureList.Contains(i))
                {
                    Debug.Log("Feauture In List!");
                    //Not in game or dynamic list spawn the existing object
                    dynamicFeatureList.Add(i);
                    SpawnExistingLandMarkIntoScene(i.properties.landmarkID,i);
                }else
                {
                    Debug.Log("Correcting for error");
                    //Check if the object is in the lists but not in the scene
                    GameObject searchResult = GameObject.Find("Landmark_"+i.properties.landmarkID.ToString());
                    if (!searchResult)
                    {
                        if(searchResult = null)
                        {
                            dynamicFeatureList.Remove(i);
                        }
                    }
                }
            }else
            {
                //Object is too far remove and clean from lists
                dynamicFeatureList.Remove(i);
                string LandMarkToDestroyName = "Landmark_"+i.properties.landmarkID.ToString();
                GameObject.Destroy(GameObject.Find(LandMarkToDestroyName));
                //You can optimize this
                LiveLandMarks.Remove(GameObject.Find(LandMarkToDestroyName));
            }
        }
        StartCoroutine(updateAllSpawnedLandmarkLocations());
        yield return new WaitForSeconds(5.0f);
        StartCoroutine(populateDynamicList(landmarkSpawnDistance));
    }

    public IEnumerator updateAllSpawnedLandmarkLocations()
    {
        foreach(GameObject i in LiveLandMarks)
        {
            foreach(mapboxFeatureClass feature in dynamicFeatureList)
            {
                if(i.name.Contains(feature.properties.landmarkID))
                {
                    Vector2d reConfirmedVector2D;
                    reConfirmedVector2D.y = feature.geometry.coordinates[0];
                    reConfirmedVector2D.x = feature.geometry.coordinates[1];
                    Vector3 reConfirmedVector3 = locationProviderFactoryLink.mapManager.GeoToWorldPosition(reConfirmedVector2D);
                    reConfirmedVector3.y = userFocusSquare.focusSquareRayCastHitVector.y;
                    i.transform.position = reConfirmedVector3;
                }
            }
        }
        yield return null;
    }
}
