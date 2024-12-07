using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnergyModel
{
    public static string TAG_ENERGY_MEAT = "Meat";
    public static string TAG_ENERGY_COCONUT = "Coconut";
    public static string TAG_ENERGY_BANANA = "Banana";
    public static string TAG_ENERGY_PINEAPPLE = "Pineapple";
  

    public static bool IsTagEnergy(string tag)
    {
        if (tag == TAG_ENERGY_MEAT ||
            tag == TAG_ENERGY_COCONUT ||
            tag == TAG_ENERGY_BANANA ||
            tag == TAG_ENERGY_PINEAPPLE)
            return true;
        return false;
    }

    public static int GetEnergyByTag(string tag)
    {
        if (tag == TAG_ENERGY_MEAT)
            return 20;
        else if (tag == TAG_ENERGY_BANANA)
            return 5;
        else if (tag == TAG_ENERGY_COCONUT)
            return 15;
        else if (tag == TAG_ENERGY_PINEAPPLE)
            return 10;
        return 0;
    }

    public static int GetHypeTurnCountByTag(string tag)
    {
        if (tag == TAG_ENERGY_MEAT)
            return 5;
        else if (tag == TAG_ENERGY_BANANA)
            return 0;
        else if (tag == TAG_ENERGY_COCONUT)
            return 0;
        else if (tag == TAG_ENERGY_PINEAPPLE)
            return 0;
        return 0;
    }

    public static int GetBornDayByTag(string tag)
    {
        if (tag == TAG_ENERGY_MEAT)
            return 10;//10
        else if (tag == TAG_ENERGY_BANANA)
            return 0;//0
        else if (tag == TAG_ENERGY_COCONUT)
            return 14;//14
        else if (tag == TAG_ENERGY_PINEAPPLE)
            return 6;//6
        return 0;
    }

    //public static string GetDescription(string tag)
    //{
    //    if (tag == TAG_ENERGY_MEAT)
    //        return "Nothing beats the meat!";
    //    else if (tag == TAG_ENERGY_BANANA)
    //        return "Yessss! my favorite fruit is here!";
    //    else if (tag == TAG_ENERGY_COCONUT)
    //        return "Coconuts! At least it would quench some of my thirst.";
    //    else if (tag == TAG_ENERGY_PINEAPPLE)
    //        return "Nice! Finally some real sweetness!";
    //    return "Wow! New food on serve!";
    //}

    public static GameObject[] GetAllEnergyGOs()
    {

        GameObject[] enemies1 = GameObject.FindGameObjectsWithTag(TAG_ENERGY_BANANA);
        GameObject[] enemies2 = GameObject.FindGameObjectsWithTag(TAG_ENERGY_COCONUT);
        GameObject[] enemies3 = GameObject.FindGameObjectsWithTag(TAG_ENERGY_MEAT);
        GameObject[] enemies4 = GameObject.FindGameObjectsWithTag(TAG_ENERGY_PINEAPPLE);

        GameObject[] arr = enemies1.Concat(enemies2).Concat(enemies3).Concat(enemies4).ToArray();
        return arr;
    }

    public static GameObject[] GetAllVisibleEnergyGOs()
    {
        List<GameObject> result = new List<GameObject>();
        GameObject[] enemies1 = GameObject.FindGameObjectsWithTag(TAG_ENERGY_BANANA);
        GameObject[] enemies2 = GameObject.FindGameObjectsWithTag(TAG_ENERGY_COCONUT);
        GameObject[] enemies3 = GameObject.FindGameObjectsWithTag(TAG_ENERGY_MEAT);
        GameObject[] enemies4 = GameObject.FindGameObjectsWithTag(TAG_ENERGY_PINEAPPLE);

        GameObject[] tmp = enemies1.Concat(enemies2).Concat(enemies3).Concat(enemies4).ToArray();
       
        foreach (GameObject item in tmp)
        {
            if (item.GetComponent<SpriteRenderer>().isVisible)
                result.Add(item);
        }
        
        return result.ToArray();
    }

    public static GameObject[] GetAllVisibleEnergyGOs(List<string> energyTags)
    {
        List<GameObject> result = new List<GameObject>();
        foreach (string tag in energyTags)
        {
            GameObject[] tmp = GameObject.FindGameObjectsWithTag(tag);
            foreach (GameObject item in tmp)
            {
                if (item.GetComponent<SpriteRenderer>().isVisible)
                    result.Add(item);
            }
        }
      
        

        return result.ToArray();
    }

    public static string GetDisplayNameByTag(string tag)
    {
        if (tag == TAG_ENERGY_MEAT)
            return MeysamLocalization.GetLocalizaStringByKey("Meat");
        else if (tag == TAG_ENERGY_BANANA)
            return MeysamLocalization.GetLocalizaStringByKey("Banana");
        else if (tag == TAG_ENERGY_COCONUT)
            return MeysamLocalization.GetLocalizaStringByKey("Coconut");
        else if (tag == TAG_ENERGY_PINEAPPLE)
            return MeysamLocalization.GetLocalizaStringByKey("Pineapple");

        return string.Empty;
    }
}
