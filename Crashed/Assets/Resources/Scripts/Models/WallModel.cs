using System.Collections;
using System.Collections.Generic;


public class WallModel
{
    public static string TAG_WALL = "Wall";

    public static bool IsTagWall(string tag)
    {
        if (tag == TAG_WALL)
            return true;
        return false;
    }
}
