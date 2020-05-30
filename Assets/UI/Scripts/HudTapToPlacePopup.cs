using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudTapToPlacePopup : MonoBehaviour
{
    public MapboxApI mainApiLink;
    public bool hideHelp = false;

    public void Update()
    {
        if(mainApiLink.LandmarkWindow && !hideHelp)
        {
            gameObject.SetActive(false);
        }else
        {
            gameObject.SetActive(true);
        }

        if(hideHelp)
        {
            gameObject.SetActive(false);
        }
    }

    public void dismissHelp()
    {
        hideHelp = true;
    }
}