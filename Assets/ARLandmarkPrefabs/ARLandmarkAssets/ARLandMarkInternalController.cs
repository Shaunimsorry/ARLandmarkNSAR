using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ARLandMarkInternalController : MonoBehaviour
{
    public Camera targetCamera;
    public GameObject illustrationAsset;
    //Setup Landmark UpperLogo Assets
    public string landmarkLogo;
    public string stringlandmarkText;
    public string stringlandmarkCreator;
    public string stringlandMarkLikes;
    public string stringBackgroundKeyword;
    public TextMeshPro landmarkTitle;
    public TextMeshPro landmarkCreator;
    public TextMeshPro landmarkLikes;

    public string landMarkID;

    //Shape Library
    public GameObject landmark_triangleLogo;
    public GameObject landmark_Squarelogo;
    public GameObject landmark_circleLogo;
    public GameObject landmark_warningLogo;

    //Background Library
    public Sprite illustration_smile;
    public Sprite illustration_enjoyYourTrip;
    public Sprite illustration_goodJob;
    public Sprite illustration_takeARest;
    public Sprite illustration_niceView;
    public Sprite illustration_vendingMachine;
    public Sprite illustration_chargeStation;
    public Sprite illustration_watchYourStep;
    public Sprite illustration_watchYourKids;








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

        
        //Setup the land mark with data from the cloud
        landmarkTitle.text = stringlandmarkText;
        landmarkCreator.text = stringlandmarkCreator;
        landmarkLikes.text = stringlandMarkLikes;


        //Setup Landmark Type Switcher
        if(landmarkLogo == "Square")
        {
                landmark_circleLogo.SetActive(false);
                landmark_Squarelogo.SetActive(true);
                landmark_triangleLogo.SetActive(false);
                landmark_warningLogo.SetActive(false); 
        }else
        {
            if(landmarkLogo == "Circle")
            {
                landmark_circleLogo.SetActive(true);
                landmark_Squarelogo.SetActive(false);
                landmark_triangleLogo.SetActive(false);
                landmark_warningLogo.SetActive(false); 
            }else
            {
                if(landmarkLogo == "Triangle")
                {
                    landmark_circleLogo.SetActive(false);
                    landmark_Squarelogo.SetActive(false);
                    landmark_triangleLogo.SetActive(true);  
                    landmark_warningLogo.SetActive(false);  
                }else
                {
                    if(landmarkLogo == "Warning")
                    {
                        landmark_warningLogo.SetActive(true);
                        landmark_triangleLogo.SetActive(false);
                        landmark_circleLogo.SetActive(false);
                        landmark_Squarelogo.SetActive(false);
                    }
                }
            }
        }
        //Setup conditional formatting for the background
        if(stringBackgroundKeyword == "Smile")
        {
            illustrationAsset.GetComponent<SpriteRenderer>().sprite = illustration_smile;
        }else
        {
            if(stringBackgroundKeyword == "Good Job")
            {
                illustrationAsset.GetComponent<SpriteRenderer>().sprite = illustration_goodJob;
            }else
            {
                if(stringBackgroundKeyword == "Enjoy Trip")
                {
                    illustrationAsset.GetComponent<SpriteRenderer>().sprite = illustration_enjoyYourTrip;
                }else
                {
                    if(stringBackgroundKeyword == "Nice View")
                    {
                        illustrationAsset.GetComponent<SpriteRenderer>().sprite = illustration_niceView;
                    }else
                    {
                        if(stringBackgroundKeyword == "Take A Rest")
                        {
                            illustrationAsset.GetComponent<SpriteRenderer>().sprite = illustration_takeARest;
                        }else
                        {
                            if(stringBackgroundKeyword == "Nice View")
                            {
                                illustrationAsset.GetComponent<SpriteRenderer>().sprite = illustration_niceView;
                            }else
                            {
                                if(stringBackgroundKeyword == "Vending Machine")
                                {
                                    illustrationAsset.GetComponent<SpriteRenderer>().sprite = illustration_vendingMachine;
                                }else
                                {
                                    if(stringBackgroundKeyword == "Charge Station")
                                    {
                                        illustrationAsset.GetComponent<SpriteRenderer>().sprite = illustration_chargeStation;
                                    }else
                                    {
                                        if(stringBackgroundKeyword == "Watch Your Step")
                                        {
                                            illustrationAsset.GetComponent<SpriteRenderer>().sprite = illustration_watchYourStep;
                                        }else
                                        {
                                            if(stringBackgroundKeyword == "Watch Your Kids")
                                            {
                                                illustrationAsset.GetComponent<SpriteRenderer>().sprite = illustration_watchYourKids;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

                 
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


        //Needs to be live so its always updating
        landmarkLikes.text = stringlandMarkLikes;

    }
}
