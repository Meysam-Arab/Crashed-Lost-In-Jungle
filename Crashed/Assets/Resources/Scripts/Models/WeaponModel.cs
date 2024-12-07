using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponModel
{
    public static string TAG_WEAPON_AXE = "Axe";
    public static string TAG_WEAPON_PICKAXE = "Pickaxe";
    public static string TAG_WEAPON_SPEAR = "Spear";


    public static bool IsTagWeapon(string tag)
    {
        if (tag == TAG_WEAPON_AXE ||
            tag == TAG_WEAPON_PICKAXE ||
            tag == TAG_WEAPON_SPEAR)
            return true;
        return false;
    }

    public static int GetBornDayByTag(string tag)
    {
        if (tag == TAG_WEAPON_AXE)
            return 2;//2
        else if (tag == TAG_WEAPON_PICKAXE)
            return 9;//9
        else if (tag == TAG_WEAPON_SPEAR)
            return 6;//6
        return 0;
    }

    public static GameObject[] GetAllVisibleWeaponGOs(List<string> weaponTags)
    {
        List<GameObject> result = new List<GameObject>();
        foreach (string tag in weaponTags)
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
        if (tag == TAG_WEAPON_AXE)
            return MeysamLocalization.GetLocalizaStringByKey("Axe");
        else if (tag == TAG_WEAPON_PICKAXE)
            return MeysamLocalization.GetLocalizaStringByKey("Pickaxe");
        else if (tag == TAG_WEAPON_SPEAR)
            return MeysamLocalization.GetLocalizaStringByKey("Spear");
      

        return string.Empty;
    }

    //public static string GetDescription(string tag)
    //{
    //    if (tag == TAG_WEAPON_AXE)
    //        return "It's good, very good! Now I can chop down trees and bushes.";
    //    else if (tag == TAG_WEAPON_PICKAXE)
    //        return "Pickaxes! Useful for destroying rocks.";
    //    else if (tag == TAG_WEAPON_SPEAR)
    //        return "Yess! Now the tables are turned! Time to do some hunting!";

    //    return "Interesting! There's some new tools around! Very useful for my survival!";
    //}

    public static string GetWeaponDescriptionError(string weaponTag)
    {
        if (weaponTag == WeaponModel.TAG_WEAPON_AXE)
           return MeysamLocalization.GetLocalizaStringByKey("With an Axe, I can cut bushes and cut down trees!");
        else if (weaponTag == WeaponModel.TAG_WEAPON_PICKAXE)
            return MeysamLocalization.GetLocalizaStringByKey("With a pickaxe, I can destroy boulders!");
        else if (weaponTag == WeaponModel.TAG_WEAPON_SPEAR)
            return MeysamLocalization.GetLocalizaStringByKey("With a Spear, I can defend myself or hunt animals!");
        else
            return string.Empty;
    }
}
