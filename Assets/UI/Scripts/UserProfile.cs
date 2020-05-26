using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserProfile : MonoBehaviour
{
    public MapboxApI MainMapBoxApi;

    void Start()
    {
        gameObject.SetActive(false);
        MainMapBoxApi.LandmarkWindow = false;
    }
    public void showuserProfile()
    {
        gameObject.SetActive(true);
        MainMapBoxApi.LandmarkWindow = true;
    }

    public void hideuserProfile()
    {
        gameObject.SetActive(false);
        MainMapBoxApi.LandmarkWindow = false;
    }
}
