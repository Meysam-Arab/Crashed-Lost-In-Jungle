using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel
{
    public int energy;
    public string equipedWeaponeTag;
    public int collectedMapCount;
    public string prefabName;

    public void Initialize()
    {
        energy = 50;
        equipedWeaponeTag = null;
        collectedMapCount = 0;
    }
}
