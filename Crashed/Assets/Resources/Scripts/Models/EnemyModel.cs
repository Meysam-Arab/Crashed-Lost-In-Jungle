using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyModel
{
    public static string TAG_ENEMY_JAGUAR = "Jaguar";
    public static string TAG_ENEMY_LION = "Lion";
    public static string TAG_ENEMY_TIGER = "Tiger";
    public static string TAG_ENEMY_BEAR = "Bear";
    public static string TAG_ENEMY_GORILLA = "Gorilla";
    public static string TAG_ENEMY_LIONESS = "Lioness";
    public static string TAG_ENEMY_CANNIBAL = "Cannibal";
    public static string TAG_ENEMY_MONKEY = "Monkey";

  
    public int moveTileCounts;
    public float moveProbability;
    public float leapProbability;

    public void Initialize(string tag)
    {
        

        if (tag == TAG_ENEMY_JAGUAR)
        {
            moveTileCounts = 2;
            moveProbability = 0.5f;
            leapProbability = 0.5f;
        }
        else if(tag == TAG_ENEMY_LION)
        {
            moveTileCounts = 1;
            moveProbability = 0.5f;
            leapProbability = 0.5f;
        }
        else if (tag == TAG_ENEMY_TIGER)
        {
            moveTileCounts = 1;
            moveProbability = 0.5f;
            leapProbability = 0.5f;
        }
        else if (tag == TAG_ENEMY_BEAR)
        {
            moveTileCounts = 1;
            moveProbability = 0.5f;
            leapProbability = 0.5f;
        }
        else if (tag == TAG_ENEMY_LIONESS)
        {
            moveTileCounts = 1;
            moveProbability = 0.5f;
            leapProbability = 0.5f;
        }
        else if (tag == TAG_ENEMY_GORILLA)
        {
            moveTileCounts = 1;
            moveProbability = 0.5f;
            leapProbability = 0.5f;
        }
        else if (tag == TAG_ENEMY_CANNIBAL)
        {
            moveTileCounts = 1;
            moveProbability = 0.5f;
            leapProbability = 0.5f;
        }
        else if (tag == TAG_ENEMY_MONKEY)
        {
            moveTileCounts = 1;
            moveProbability = 0.5f;
            leapProbability = 0.5f;
        }
    }

    public static bool IsTagEnemy(string tag)
    {
        if (tag == TAG_ENEMY_JAGUAR ||
            tag == TAG_ENEMY_LION ||
            tag == TAG_ENEMY_TIGER ||
            tag == TAG_ENEMY_BEAR ||
            tag == TAG_ENEMY_GORILLA ||
            tag == TAG_ENEMY_LIONESS ||
            tag == TAG_ENEMY_CANNIBAL ||
            tag == TAG_ENEMY_MONKEY)
            return true;
        return false;
    }


    public static bool IsEnemyKillable(string enemyTag, string weaponTag)
    {
        if ((enemyTag == TAG_ENEMY_JAGUAR ||
            enemyTag == TAG_ENEMY_LION ||
            enemyTag == TAG_ENEMY_TIGER ||
            enemyTag == TAG_ENEMY_BEAR ||
            enemyTag == TAG_ENEMY_GORILLA ||
            enemyTag == TAG_ENEMY_LIONESS ||
            enemyTag == TAG_ENEMY_CANNIBAL ||
            enemyTag == TAG_ENEMY_MONKEY) && weaponTag == WeaponModel.TAG_WEAPON_SPEAR)
            return true;
        return false;
    }

    public static string GetEnemyDropTag(string enemyTag)
    {
        if (enemyTag == TAG_ENEMY_JAGUAR ||
            enemyTag == TAG_ENEMY_LION ||
            enemyTag == TAG_ENEMY_TIGER ||
            enemyTag == TAG_ENEMY_BEAR ||
            enemyTag == TAG_ENEMY_GORILLA ||
            enemyTag == TAG_ENEMY_LIONESS ||
            enemyTag == TAG_ENEMY_MONKEY)
            return EnergyModel.TAG_ENERGY_MEAT;
        else if (enemyTag == TAG_ENEMY_CANNIBAL)
        {
            List<string> itemsToDrop = new List<string>();
            itemsToDrop.Add(EnergyModel.TAG_ENERGY_BANANA);
            itemsToDrop.Add(EnergyModel.TAG_ENERGY_COCONUT);
            itemsToDrop.Add(EnergyModel.TAG_ENERGY_MEAT);
            itemsToDrop.Add(EnergyModel.TAG_ENERGY_PINEAPPLE);
            itemsToDrop.Add(WeaponModel.TAG_WEAPON_AXE);
            itemsToDrop.Add(WeaponModel.TAG_WEAPON_PICKAXE);
            itemsToDrop.Add(WeaponModel.TAG_WEAPON_SPEAR);

            int randomIndex = Random.Range(0, itemsToDrop.Count);
            return itemsToDrop[randomIndex];
        }



        return null;
    }

    public static int GetBornDayByTag(string tag)
    {
        if (tag == TAG_ENEMY_BEAR)
            return 5;//5
        else if (tag == TAG_ENEMY_CANNIBAL)
            return 23;//23
        else if (tag == TAG_ENEMY_GORILLA)
            return 17;//17
        else if (tag == TAG_ENEMY_JAGUAR)
            return 20;//20
        else if (tag == TAG_ENEMY_LION)
            return 9;//9
        else if (tag == TAG_ENEMY_LIONESS)
            return 7;//7
        else if (tag == TAG_ENEMY_TIGER)
            return 12;//12
        else if (tag == TAG_ENEMY_MONKEY)
            return 3;//3
        return 0;
    }

    public static GameObject[] GetAllEnemyNPCGOs()
    {

        GameObject[] enemies1 = GameObject.FindGameObjectsWithTag(TAG_ENEMY_BEAR);
        GameObject[] enemies2 = GameObject.FindGameObjectsWithTag(TAG_ENEMY_CANNIBAL);
        GameObject[] enemies3 = GameObject.FindGameObjectsWithTag(TAG_ENEMY_GORILLA);
        GameObject[] enemies4 = GameObject.FindGameObjectsWithTag(TAG_ENEMY_JAGUAR);
        GameObject[] enemies5 = GameObject.FindGameObjectsWithTag(TAG_ENEMY_LION);
        GameObject[] enemies6 = GameObject.FindGameObjectsWithTag(TAG_ENEMY_LIONESS);
        GameObject[] enemies7 = GameObject.FindGameObjectsWithTag(TAG_ENEMY_TIGER);
        GameObject[] enemies8 = GameObject.FindGameObjectsWithTag(TAG_ENEMY_MONKEY);

        GameObject[] arr = enemies1.Concat(enemies2).Concat(enemies3).Concat(enemies4).Concat(enemies5).Concat(enemies6).Concat(enemies7).Concat(enemies8).ToArray();
        return arr;
    }

    //public static string GetDescription(string tag)
    //{
    //    if (tag == TAG_ENEMY_BEAR)
    //        return "Ohhh! It's getting harder and harder!";
    //    else if (tag == TAG_ENEMY_CANNIBAL)
    //        return "Cannibals! Let's be careful not to be on their menu tonight!";
    //    else if (tag == TAG_ENEMY_GORILLA)
    //        return "Damn it! Darwin's favorites are here!";
    //    else if (tag == TAG_ENEMY_JAGUAR)
    //        return "Ohhh! I must be faster to survive.";
    //    else if (tag == TAG_ENEMY_LION)
    //        return "Ohhh! It's getting harder and harder!";
    //    else if (tag == TAG_ENEMY_LIONESS)
    //        return "Ohhh! It's getting harder and harder!";
    //    else if (tag == TAG_ENEMY_TIGER)
    //        return "Ohhh! It's getting harder and harder!";
    //    else if (tag == TAG_ENEMY_MONKEY)
    //        return "Oh no! Monkeys are here! I better kill them sooner or They are gonna eat all the fruits!";
    //    return "Ohhh! It's getting harder and harder!";
    //}

    public static bool IsEnemyKiller(string enemyTag)
    {
        if (enemyTag == TAG_ENEMY_MONKEY)
            return false;
        return true;
    }

    public static List<string> GetAllowedEnergyTagsToEat(string enemyTag)
    {
        List<string> result = new List<string>();
        if (enemyTag == TAG_ENEMY_MONKEY)
        {
            result.Add(EnergyModel.TAG_ENERGY_BANANA);
            result.Add(EnergyModel.TAG_ENERGY_COCONUT);
            result.Add(EnergyModel.TAG_ENERGY_PINEAPPLE);
        }
        else if(enemyTag == TAG_ENEMY_BEAR ||
            enemyTag == TAG_ENEMY_JAGUAR ||
            enemyTag == TAG_ENEMY_GORILLA ||
            enemyTag == TAG_ENEMY_LION ||
            enemyTag == TAG_ENEMY_LIONESS ||
            enemyTag == TAG_ENEMY_TIGER)
        {
            result.Add(EnergyModel.TAG_ENERGY_MEAT);
        }
        else if(enemyTag == TAG_ENEMY_CANNIBAL)
        {
            result.Add(EnergyModel.TAG_ENERGY_BANANA);
            result.Add(EnergyModel.TAG_ENERGY_COCONUT);
            result.Add(EnergyModel.TAG_ENERGY_PINEAPPLE);
            result.Add(EnergyModel.TAG_ENERGY_MEAT);
        }
        return result;
    }
}
