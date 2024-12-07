using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapModel
{
    public static string TAG_MAP = "Map";


    public static bool IsTagMap(string tag)
    {
        if (tag == TAG_MAP)
            return true;
        return false;
    }

    public static int GetBornDay()
    {
        return 5;
    }
}
