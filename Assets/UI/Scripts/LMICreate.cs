using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LMICreate : MonoBehaviour
{
    public MapboxApI MapBoxApiScript;
    public string LandmarkLogoShape;
    public InputField LandMarkName;
    public Image selectedThumbImage;
    public Sprite TriangleLogo;
    public Sprite SquareLogo;
    public Sprite Circlelogo;

    public void Start()
    {
        hideCreateMenu();
        MapBoxApiScript = GameObject.Find("ARLandmarkController").GetComponent<MapboxApI>();
    }

    public void hideCreateMenu()
    {
        gameObject.SetActive(false);
        MapBoxApiScript.LandmarkWindow = false;
    }
    public void showCreateMenu()
    {
        gameObject.SetActive(true);
        MapBoxApiScript.LandmarkWindow = true;
    }



    public void setLogoToTriangle()
    {
        LandmarkLogoShape = "Triangle";
        selectedThumbImage.sprite = TriangleLogo;
    }
      public void setLogoToSquare()
    {
        LandmarkLogoShape = "Square";
        selectedThumbImage.sprite = SquareLogo;
    }

    public void setLogoToCircle()
    {
        LandmarkLogoShape = "Circle";
        selectedThumbImage.sprite = Circlelogo;
    }


}
