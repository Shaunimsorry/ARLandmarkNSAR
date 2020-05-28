using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerClass
{
    public string playerName;
    public int playerID;
    public int playerLikes;
    public int playerLandmarks;
    public int playerLevel;
    public int playerAge;
    public string playerCountry;
}

[System.Serializable]
public class PlayerSet
{
    public List<PlayerClass> ActivePlayers;
}
