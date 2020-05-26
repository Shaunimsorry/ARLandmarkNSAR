using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    public Button StartButton;
    public Button LoginButton;
 
    public bool loginPass;

    //Storin Usernames
    public TMP_InputField startScreenUsernameInput;

    //Do i even need static here ?
    public static string userName;


    public void StartARLandmark()
    {
        if(startScreenUsernameInput.text != "")
        {
            userName = startScreenUsernameInput.text;
            SceneManager.LoadScene("ARLandmarkWSAR");    
        }

    }
}
