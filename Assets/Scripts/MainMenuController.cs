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

    public static string userName;

    public void Start()
    {
        //Testing to see if this will allow the data from the menu controller to carry forward into the next scene
        DontDestroyOnLoad(this.gameObject);
    }


    public void StartARLandmark()
    {
        if(startScreenUsernameInput.text != "")
        {
            userName = startScreenUsernameInput.text;
            SceneManager.LoadScene("ARLandmarkWSAR");    
        }

    }
}
