using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlockadeModel
{
    public static string TAG_BLOCKADE_BOULDER = "Boulder";
    public static string TAG_BLOCKADE_BUSH = "Bush";
    public static string TAG_BLOCKADE_PIT = "Pit";
    public static string TAG_BLOCKADE_BANANA_TREE = "BananaTree";
    public static string TAG_BLOCKADE_COCONUT_TREE = "CoconutTree";
    public static string TAG_BLOCKADE_SPIKE = "Spike";
    public static string TAG_BLOCKADE_PINEAPPLE_PLANT = "PineapplePlant";

    public static bool IsTagBlockade(string tag)
    {
        if (tag == TAG_BLOCKADE_BOULDER ||
            tag == TAG_BLOCKADE_BUSH ||
            tag == TAG_BLOCKADE_PIT ||
            tag == TAG_BLOCKADE_BANANA_TREE ||
            tag == TAG_BLOCKADE_COCONUT_TREE ||
            tag == TAG_BLOCKADE_SPIKE ||
            tag == TAG_BLOCKADE_PINEAPPLE_PLANT)
            return true;
        return false;
    }
    public static bool IsBlockadeRemovable(string blockadeTag, string weaponTag)
    {
        if (blockadeTag == TAG_BLOCKADE_BUSH && weaponTag == WeaponModel.TAG_WEAPON_AXE)
            return true;
        if (blockadeTag == TAG_BLOCKADE_BOULDER && weaponTag == WeaponModel.TAG_WEAPON_PICKAXE)
            return true;
        if (blockadeTag == TAG_BLOCKADE_BANANA_TREE && weaponTag == WeaponModel.TAG_WEAPON_AXE)
            return true;
        if (blockadeTag == TAG_BLOCKADE_COCONUT_TREE && weaponTag == WeaponModel.TAG_WEAPON_AXE)
            return true;
        if (blockadeTag == TAG_BLOCKADE_PINEAPPLE_PLANT && weaponTag == WeaponModel.TAG_WEAPON_AXE)
            return true;
        else
            return false;
    }

    public static bool IsBlockadeWalkableUpon(string blockadeTag)
    {
        if (blockadeTag == TAG_BLOCKADE_PIT)
            return true;
        else if (blockadeTag == TAG_BLOCKADE_SPIKE)
            return true;
        else
            return false;
    }

    public static string GetBlockadeDropTag(string blockadeTag)
    {
        if (blockadeTag == TAG_BLOCKADE_BANANA_TREE)
            return EnergyModel.TAG_ENERGY_BANANA;
        else if (blockadeTag == TAG_BLOCKADE_COCONUT_TREE)
            return EnergyModel.TAG_ENERGY_COCONUT;
        else if (blockadeTag == TAG_BLOCKADE_PINEAPPLE_PLANT)
            return EnergyModel.TAG_ENERGY_PINEAPPLE;
        return null;
    }


    public static int GetBornDayByTag(string tag)
    {
        if (tag == TAG_BLOCKADE_BANANA_TREE)
            return 4;//4
        else if (tag == TAG_BLOCKADE_BOULDER)
            return 8;//8
        else if (tag == TAG_BLOCKADE_BUSH)
            return 0;//0
        else if (tag == TAG_BLOCKADE_PIT)
            return 19;//19
        else if (tag == TAG_BLOCKADE_SPIKE)
            return 21;//21
        else if (tag == TAG_BLOCKADE_COCONUT_TREE)
            return 15;//15
        else if (tag == TAG_BLOCKADE_PINEAPPLE_PLANT)
            return 7;//7
        return 0;
    }

    //public static bool CanBlockadeBlockVision(string blockadeTag)
    //{
    //    if (blockadeTag == TAG_BLOCKADE_BANANA_TREE)
    //        return true;
    //    else if (blockadeTag == TAG_BLOCKADE_BOULDER)
    //        return true;
    //    else if (blockadeTag == TAG_BLOCKADE_BUSH)
    //        return true;
    //    else if (blockadeTag == TAG_BLOCKADE_COCONUT_TREE)
    //        return true;
    //    else if (blockadeTag == TAG_BLOCKADE_PINEAPPLE_PLANT)
    //        return true;
    //    else
    //        return false;
    //}

    public static bool IsTagAllowedToMoveOver(string blockadeTag, string enemyTag)
    {
        if ((blockadeTag == TAG_BLOCKADE_BOULDER || blockadeTag == TAG_BLOCKADE_SPIKE) && enemyTag == EnemyModel.TAG_ENEMY_GORILLA)
            return true;
        else if ((blockadeTag == TAG_BLOCKADE_BUSH || blockadeTag == TAG_BLOCKADE_SPIKE) &&
            (enemyTag == EnemyModel.TAG_ENEMY_JAGUAR ||
            enemyTag == EnemyModel.TAG_ENEMY_LION ||
            enemyTag == EnemyModel.TAG_ENEMY_LIONESS ||
            enemyTag == EnemyModel.TAG_ENEMY_TIGER))
            return true;
        else if(blockadeTag == TAG_BLOCKADE_SPIKE &&
            (enemyTag == EnemyModel.TAG_ENEMY_CANNIBAL ||
            enemyTag == EnemyModel.TAG_ENEMY_BEAR ||
            enemyTag == EnemyModel.TAG_ENEMY_MONKEY))
            return true;
        else
            return false;
    }

  

    public static string GetDisplayNameByTag(string tag)
    {
        if (tag == TAG_BLOCKADE_BANANA_TREE)
            return MeysamLocalization.GetLocalizaStringByKey("BananaTree");
        else if (tag == TAG_BLOCKADE_BOULDER)
            return MeysamLocalization.GetLocalizaStringByKey("Boulder");
        else if (tag == TAG_BLOCKADE_BUSH)
            return MeysamLocalization.GetLocalizaStringByKey("Bush");
        else if (tag == TAG_BLOCKADE_COCONUT_TREE)
            return MeysamLocalization.GetLocalizaStringByKey("CoconutTree");
        else if (tag == TAG_BLOCKADE_PINEAPPLE_PLANT)
            return MeysamLocalization.GetLocalizaStringByKey("PineapplePlant");
        else if (tag == TAG_BLOCKADE_PIT)
            return MeysamLocalization.GetLocalizaStringByKey("Pit");
        else if (tag == TAG_BLOCKADE_SPIKE)
            return MeysamLocalization.GetLocalizaStringByKey("Spike");


        return string.Empty;
    }

   
}
