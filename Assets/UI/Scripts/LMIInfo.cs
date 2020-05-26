using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LMIInfo : MonoBehaviour
{
    public MapboxApI MapBoxApiScript;
    public Sprite TriangleLogo;
    public Sprite SquareLogo;
    public Sprite Circlelogo;

    public TextMeshProUGUI CreatorName;
    public TextMeshProUGUI LikeCounter;
    public TextMeshProUGUI Landmarkname;
    public Image landmarkLogo;

    public GameObject targetLandmark;

    public void Start()
    {
        hideInfoMenu();
        MapBoxApiScript = GameObject.Find("ARLandmarkController").GetComponent<MapboxApI>();
    }

    public void hideInfoMenu()
    {
        gameObject.SetActive(false);
        MapBoxApiScript.LandmarkWindow = false;
    }
    public void showInfoMenu(GameObject existingLandmark)
    {
        //Setup the target landmark
        targetLandmark = existingLandmark;

        gameObject.SetActive(true);
        MapBoxApiScript.LandmarkWindow = true;

        //Populate all the objcts
        CreatorName.text = existingLandmark.GetComponent<ARLandMarkInternalController>().stringlandmarkCreator;
        LikeCounter.text = existingLandmark.GetComponent<ARLandMarkInternalController>().stringlandMarkLikes;
        Landmarkname.text = existingLandmark.GetComponent<ARLandMarkInternalController>().stringlandmarkText;

        string landMarkShapeString = existingLandmark.GetComponent<ARLandMarkInternalController>().landmarkLogo;
        if (landMarkShapeString == "Triangle")
        {
            landmarkLogo.sprite = TriangleLogo;
        }else
        {
            if(landMarkShapeString == "Square")
            {
                landmarkLogo.sprite = SquareLogo;
            }else
            {
                if(landMarkShapeString == "Circle")
                {
                    landmarkLogo.sprite = Circlelogo;
                }
            }
        }
    }

    public void addLike()
    {
        int likes;
        //Pull the like count from the internal landmark controller
        likes = System.Convert.ToInt32(targetLandmark.GetComponent<ARLandMarkInternalController>().stringlandMarkLikes);
        likes ++;
        //Feed it back in
        targetLandmark.GetComponent<ARLandMarkInternalController>().stringlandMarkLikes = likes.ToString();
        //Feed it to the JEE-UI
        LikeCounter.text = likes.ToString();
        //Feed it to the server
        StartCoroutine(MapBoxApiScript.updateLandMark(targetLandmark.GetComponent<ARLandMarkInternalController>().landMarkID,likes));
        MapBoxApiScript.hudDebug00 = "Pullin Response Code: "+MapBoxApiScript.responseCode;
        hideInfoMenu();
    }
}
