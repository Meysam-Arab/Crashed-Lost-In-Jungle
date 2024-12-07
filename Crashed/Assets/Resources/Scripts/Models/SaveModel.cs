using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class SaveModel
{
    public int energy;
    public int level;
    public int collectedMaps;
    public string playerPrefabName;
    public string equipedWeaponeTag;
    public int gameVersion;
    //public Vector2 playerPosition;
    //public List<string> enemyTags;
    //public List<Vector2> enemyPositions;
    //public List<string> blockadeTags;
    //public List<Vector2> blockadePositions;
    //public List<string> energyTags;
    //public List<Vector2> energyPositions;
    //public Vector2 exitPosition;

    public SaveModel()
    {
        //enemyTags = new List<string>();
        //enemyPositions = new List<Vector2>();
        // blockadeTags = new List<string>();
        //blockadePositions = new List<Vector2>();
        //energyTags = new List<string>();
        //energyPositions = new List<Vector2>();
    }
}
