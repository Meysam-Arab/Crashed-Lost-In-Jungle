using System;
using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Reflection;
using TMPro;

public static class MeysamUtility
{



    public static Color32 ColorOrangeButton = new Color32(255, 117, 0, 255);
    public static Color32 ColorRedButton = new Color32(245, 27, 50, 255);
    public static Color32 ColorBlueButton = new Color32(28, 160, 255, 255);
    public static Color32 ColorGreenButton = new Color32(28, 160, 0, 255);
    public static Color32 ColorYellowButton = new Color32(255, 202, 1, 255);
    public static Color32 ColorPurpleButton = new Color32(157, 26, 235, 255);
    public static Color32 ColorBloodRed = new Color32(166, 16, 30, 255);
    public static Color32 ColorYellowText = new Color32(251, 195, 105, 255);
    public static Color32 ColorYellowButtonText = new Color32(252, 195, 104, 255);
    public static Color32 ColorGreyButtonText = new Color32(186, 133, 48, 255);
    public static Color32 ColorGreyButtonBorder = new Color32(172, 172, 172, 255);
    public static Color32 ColorNormalButtonBorder = new Color32(255, 255, 255, 255);

   

    //public static Material FontMaterialA;
    //public static Material FontMaterialB;


 
        //// Use a different material preset which was derived from this font asset and created using the Create Material Preset Context Menu.
        //m_TextComponent.fontSharedMaterial = FontMaterialA;

    public static string removeSubstringFromString(string original, string substring2remove)
    {
        return original.Replace(substring2remove, "").Trim();
    }



    public static void CopyValues<T>(T from, T to)
    {
        var json = JsonUtility.ToJson(from);
        JsonUtility.FromJsonOverwrite(json, to);
    }

    public static void ConvertTime(int min, int sec, out int newHour, out int newMin)
    {
        newHour = 0;
        newMin = 0;

        newMin = 60 - sec;
        if (sec == 0)
            newMin = 59;

        if (min == 11)
        {
            newHour = 7;
        }
        else if (min == 10)
        {
            newHour = 8;
        }
        else if (min == 9)
        {
            newHour = 9;
        }
        else if (min == 8)
        {
            newHour = 10;
        }
        else if (min == 7)
        {
            newHour = 11;
        }
        else if (min == 6)
        {
            newHour = 12;
        }
        else if (min == 5)
        {
            newHour = 13;
        }
        else if (min == 4)
        {
            newHour = 14;
        }
        else if (min == 3)
        {
            newHour = 15;
        }
        else if (min == 2)
        {
            newHour = 16;
        }
        else if (min == 1)
        {
            newHour = 17;
        }
        else if (min == 0)
        {
            newHour = 18;
        }
        else
        {
            newHour = 0;
        }

    }

    public static T DeepClone<T>(this T obj)
    {
        using (var ms = new MemoryStream())
        {
            var formatter = new BinaryFormatter();
            formatter.Serialize(ms, obj);
            ms.Position = 0;

            return (T)formatter.Deserialize(ms);
        }
    }

    public static T[] FindComponentsInChildrenWithTag<T>(this GameObject parent, string tag, bool forceActive = false) where T : Component
    {
        if (parent == null) { throw new System.ArgumentNullException(); }
        if (string.IsNullOrEmpty(tag) == true) { throw new System.ArgumentNullException(); }
        List<T> list = new List<T>(parent.GetComponentsInChildren<T>(forceActive));
        if (list.Count == 0) { return null; }

        for (int i = list.Count - 1; i >= 0; i--)
        {
            if (list[i].CompareTag(tag) == false)
            {
                list.RemoveAt(i);
            }
        }
        return list.ToArray();
    }

    public static T FindComponentInChildWithTag<T>(this GameObject parent, string tag, bool forceActive = false) where T : Component
    {
        if (parent == null) { throw new System.ArgumentNullException(); }
        if (string.IsNullOrEmpty(tag) == true) { throw new System.ArgumentNullException(); }

        T[] list = parent.GetComponentsInChildren<T>(forceActive);
        foreach (T t in list)
        {
            if (t.CompareTag(tag) == true)
            {
                return t;
            }
        }
        return null;
    }

    public static GameObject[] FindGameObjectsInChildrenWithTag(this GameObject parent, string tag, bool forceActive = false)
    {
        if (parent == null) { throw new System.ArgumentNullException(); }
        if (string.IsNullOrEmpty(tag) == true) { throw new System.ArgumentNullException(); }

        List<GameObject> list = new List<GameObject>();
        foreach (Transform eachChild in parent.transform)
        {

            if (eachChild.gameObject.tag == tag)
            {
                list.Add(eachChild.gameObject);
            }
        }

        return list.ToArray();
    }

    public static GameObject[] FindGameObjectsInChildrenWithName(this GameObject parent, string name, bool forceActive = false)
    {
        if (parent == null) { throw new System.ArgumentNullException(); }
        if (string.IsNullOrEmpty(name) == true) { throw new System.ArgumentNullException(); }

        List<GameObject> list = new List<GameObject>();
        foreach (Transform eachChild in parent.transform)
        {
            if (eachChild.gameObject.name == name)
            {
                list.Add(eachChild.gameObject);
            }
        }

        return list.ToArray();
    }

    public static GameObject FindInactiveObjectInChildren(this GameObject parent, string name)
    {
        Transform[] trs = parent.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in trs)
        {
            if (t.name == name)
            {
                return t.gameObject;
            }
        }
        return null;
    }

    public static float GetDistance(GameObject firstGO, GameObject secondGO)
    {
        float distance = Vector2.Distance(firstGO.transform.position, secondGO.transform.position);
        return distance;
    }

    public static int GetRandomIndexByChance(float[] chances)
    {

        float[] cumulativeChances = new float[chances.Length];
        int result = -1;

        float total = 0;
        for (int i = 0; i < chances.Length; i++)
        {
            if (chances[i] == 0f)
            {
                cumulativeChances[i] = 0;
            }
            else
            {
                total += chances[i];
                cumulativeChances[i] = total;
            }

        }

        float numberChoosen = UnityEngine.Random.Range(0f, total);
        for (int i = 0; i < cumulativeChances.Length; i++)
        {
            if (i == 0)
            {
                if (numberChoosen <= cumulativeChances[i])
                {
                    result = i;
                    break;
                }
            }
            else
            {
                if (numberChoosen <= cumulativeChances[i] &&
                    numberChoosen > cumulativeChances[i - 1])
                {
                    result = i;
                    break;
                }
            }
        }

        return result;
    }

    public static float MinMaxScaling(float x, float minValue, float maxValue)
    {
        float result = (x - minValue) / (maxValue - minValue);
        return result;
    }

    public static float GetHeadAngle(float mouseAngle)
    {
        float temp = 0f;


        if (0f <= mouseAngle && mouseAngle <= 10f)
        {
            temp = mouseAngle + 90;
        }
        if (350f <= mouseAngle && mouseAngle <= 360f)
        {
            temp = mouseAngle - 270;
        }
        if (170f <= mouseAngle && mouseAngle <= 180f)
        {
            temp = mouseAngle - 90;
        }
        if (180f < mouseAngle && mouseAngle <= 190f)
        {
            temp = mouseAngle - 90;
        }


        return temp;
    }

    public static List<T> RemoveIndexesFromList<T>(List<int> indexesToRemove, List<T> listToRemove)
    {
        List<T> temp = new List<T>();
        for (int i = 0; i < listToRemove.Count; i++)
        {
            if (!indexesToRemove.Contains(i))
            {
                temp.Add(listToRemove[i]);
            }
        }
        return temp;
    }

    /// <summary>
    /// Gets the components only in immediate children of parent.
    /// </summary>
    /// <returns>The components only in children.</returns>
    /// <param name="script">MonoBehaviour Script, e.g. "this".</param>
    /// <param name="isRecursive">If set to <c>true</c> recursive search of children is performed.</param>
    /// <typeparam name="T">The 1st type parameter.</typeparam>
    public static Transform[] GetTransformsOnlyInChildren(this GameObject GO, bool isRecursive = false, bool includeInactive = false)
    {
        if (isRecursive)
        {
            if (includeInactive)
            {
                return GO.GetComponentsInChildren<Transform>(true);
            }
            else
                return GO.GetComponentsInChildren<Transform>(false);
        }
        else
        {
            Transform[] temp;
            if (includeInactive)
            {
                temp = GO.GetComponentsInChildren<Transform>(true);
            }
            else
                temp = GO.GetComponentsInChildren<Transform>(false);

            List<Transform> result = new List<Transform>();
            foreach (Transform item in temp)
            {
                if (item.parent == GO.transform)
                {
                    result.Add(item);
                }
            }

            return result.ToArray();
        }


    }


    public static List<T> FilterListByWaveNumber<T>(List<T> items, int currentWaveNumber, List<int> waveLimits, out List<T> newItems)
    {
        newItems = new List<T>();
        List<int> removedIndexes = new List<int>();
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] != null)
            {
                if (waveLimits[i] <= currentWaveNumber)
                {
                    newItems.Add(items[i]);
                }

            }
        }

        return newItems;
    }


}


