using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class mapboxFeatureClass
{
    public string type;
    public Properties properties;
    public Geometry geometry;
    public string id;
}

[System.Serializable]
public class Geometry
{
    [SerializeField]
    public List<double> coordinates;
    public string type;
}

[System.Serializable]
public class Properties
{
    public string name;
}