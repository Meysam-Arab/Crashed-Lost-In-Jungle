using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public struct Speech
{
    public string speechText;
    public TMP_FontAsset speechFont;
    public int speechFontSize;
    public Color32 speechStartColor;
    public Color32 speechEndColor;
    public float speechDuration;
    public float speechFadeSpeed;

    /// <summary>
    /// probability of choosing this specific speech
    /// </summary>
    public float speechProbability;

    /// <summary>
    /// allowed tags to drop
    /// </summary>
    public List<string> speechItemTag;

    /// <summary>
    /// probability of each item tag drop
    /// </summary>
    public List<float> speechItemProbability;

    /// <summary>
    /// count of each item tag drop
    /// </summary>
    public List<int> speechItemMinCount;
    public List<int> speechItemMaxCount;

    /// <summary>
    /// how many diffrent item drops
    /// </summary>
    public int speechDistinctItemDropCount;

    public float speechSmoothness;
}

