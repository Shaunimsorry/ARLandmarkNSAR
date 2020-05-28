using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LMI_CarouselController : MonoBehaviour
{
    public MapboxApI mapboxAPILink;
    public void showCarousel()
    {
        gameObject.SetActive(true);
        mapboxAPILink.LandmarkWindow = true;
    }
    public void hideCarousel()
    {
        gameObject.SetActive(false);
        mapboxAPILink.LandmarkWindow = false;
    }
}
